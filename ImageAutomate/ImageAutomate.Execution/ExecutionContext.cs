using System.Collections.Concurrent;
using ImageAutomate.Core;
using ImageAutomate.Execution.Scheduling;

namespace ImageAutomate.Execution;

/// <summary>
/// The runtime state of a pipeline execution.
/// </summary>
internal sealed class ExecutionContext
{
    /// <summary>
    /// The pipeline graph being executed.
    /// </summary>
    public PipelineGraph Graph { get; }

    /// <summary>
    /// The executor configuration.
    /// </summary>
    public ExecutorConfiguration Configuration { get; }

    /// <summary>
    /// The scheduler instance for this execution.
    /// </summary>
    public IScheduler Scheduler { get; }

    /// <summary>
    /// Cancellation token for cooperative cancellation.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Warehouses indexed by producer block.
    /// </summary>
    /// <remarks>
    /// Warehouses are lazily initialized. Initialization defers to the first access.
    /// </remarks>
    public ConcurrentDictionary<IBlock, Lazy<Warehouse>> Warehouses { get; } = new();

    /// <summary>
    /// Dependency barriers indexed by consumer block.
    /// </summary>
    /// <remarks>
    /// Barriers are lazily initialized. Initialization defers to the first access.
    /// </remarks>
    public ConcurrentDictionary<IBlock, Lazy<DependencyBarrier>> Barriers { get; } = new();

    /// <summary>
    /// In-degree (number of incoming connections) for each block.
    /// </summary>
    public Dictionary<IBlock, int> InDegree { get; } = [];

    /// <summary>
    /// Out-degree (number of outgoing connections) for each block.
    /// </summary>
    public Dictionary<IBlock, int> OutDegree { get; } = [];

    /// <summary>
    /// Current execution state of each block.
    /// </summary>
    public ConcurrentDictionary<IBlock, BlockExecutionState> BlockStates { get; } = new();

    /// <summary>
    /// Exceptions encountered during execution.
    /// </summary>
    public ConcurrentBag<Exception> Exceptions { get; } = [];

    /// <summary>
    /// Number of blocks currently executing.
    /// </summary>
    public int ActiveBlockCount => _activeBlockCount;

    /// <summary>
    /// Number of shipments processed (for progress tracking).
    /// A shipment is one execution of a block (may be multiple for IShipmentSource blocks).
    /// </summary>
    public int ProcessedShipmentCount => _processedShipmentCount;

    /// <summary>
    /// Timestamp of last progress.
    /// </summary>
    public DateTime LastProgress
    {
        get => new(Interlocked.Read(ref _lastProgressTicks));
        set => Interlocked.Exchange(ref _lastProgressTicks, value.Ticks);
    }

    private int _activeBlockCount;
    private int _processedShipmentCount;
    private long _lastProgressTicks;

    public ExecutionContext(
        PipelineGraph graph,
        ExecutorConfiguration configuration,
        IScheduler scheduler,
        CancellationToken cancellationToken)
    {
        Graph = graph ?? throw new ArgumentNullException(nameof(graph));
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        CancellationToken = cancellationToken;

        _activeBlockCount = 0;
        _processedShipmentCount = 0;
        _lastProgressTicks = DateTime.UtcNow.Ticks;

        InitializeDegrees();
    }

    /// <summary>
    /// Initializes in-degree and out-degree maps from the graph.
    /// </summary>
    private void InitializeDegrees()
    {
        // Initialize all blocks with degree 0 and Pending state
        foreach (var block in Graph.Blocks)
        {
            InDegree[block] = 0;
            OutDegree[block] = 0;
            BlockStates[block] = BlockExecutionState.Pending;
        }

        // Count degrees from connections
        foreach (var connection in Graph.Connections)
        {
            InDegree[connection.Target]++;
            OutDegree[connection.Source]++;
        }
    }

    /// <summary>
    /// Increments the active block count.
    /// </summary>
    public void IncrementActiveBlocks()
    {
        Interlocked.Increment(ref _activeBlockCount);
    }

    /// <summary>
    /// Decrements the active block count.
    /// </summary>
    public void DecrementActiveBlocks()
    {
        Interlocked.Decrement(ref _activeBlockCount);
    }

    /// <summary>
    /// Increments the processed shipment count and updates last progress timestamp.
    /// </summary>
    public void IncrementProcessedShipments()
    {
        Interlocked.Increment(ref _processedShipmentCount);
        LastProgress = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks a block as poisoned.
    /// </summary>
    public void MarkPoisoned(IBlock block)
    {
        BlockStates[block] = BlockExecutionState.Poisoned;
    }

    /// <summary>
    /// Checks if a block is poisoned.
    /// </summary>
    public bool IsPoisoned(IBlock block)
    {
        return BlockStates.TryGetValue(block, out var state) && state == BlockExecutionState.Poisoned;
    }
}
