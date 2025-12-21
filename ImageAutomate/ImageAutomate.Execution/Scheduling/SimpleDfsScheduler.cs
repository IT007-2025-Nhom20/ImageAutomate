using ImageAutomate.Core;

namespace ImageAutomate.Execution.Scheduling;

/// <summary>
/// Greedy completion pressure scheduler - executes the "hungriest" block first.
/// Simple implementation - production-ready, minimal overhead.
/// </summary>
/// <remarks>
/// Strategy: "Always pick the block that will free the most memory right now"
/// 
/// Priority = -CompletionPressure
/// where CompletionPressure = Σ(WarehouseSize / RemainingConsumers) for all predecessors
/// 
/// No depth calculation needed - greedy strategy naturally follows deep paths.
/// Simple, fast (O(In-Degree) per enqueue), and optimal for memory pressure.
/// </remarks>
internal sealed class SimpleDfsScheduler : IScheduler
{
    // Scheduler queue; Critical section
    private readonly PriorityQueue<IBlock, float> _queue = new();
    private readonly HashSet<IBlock> _enqueuedBlocks = [];
    private readonly Lock _lock = new();

    public bool IsEmpty
    {
        get
        {
            lock (_lock)
            {
                return _queue.Count == 0;
            }
        }
    }

    /// <inheritdoc />
    public bool TryEnqueue(IBlock block, ExecutionContext context)
    {
        // Don't enqueue blocked blocks
        if (context.IsBlocked(block))
            return false;

        lock (_lock)
        {
            // Prevent duplicate enqueueing
            if (!_enqueuedBlocks.Add(block))
                return false;

            // Calculate priority at enqueue time for accurate scheduling
            float priority = CalculatePriority(block, context);
            _queue.Enqueue(block, priority);
            return true;
        }
    }

    /// <inheritdoc />
    public IBlock? TryDequeue(ExecutionContext context)
    {
        lock (_lock)
        {
            while (_queue.Count > 0)
            {
                var block = _queue.Dequeue();
                _enqueuedBlocks.Remove(block);

                // Skip blocked blocks that may have been blocked after enqueueing
                if (context.IsBlocked(block))
                    continue;

                return block;
            }

            return null;
        }
    }

    /// <inheritdoc />
    public void SignalCompletion(IBlock completedBlock, ExecutionContext context)
    {
        // Find all downstream blocks using precomputed adjacency
        if (!context.DownstreamBlocks.TryGetValue(completedBlock, out var downstreamBlocks))
            return;

        foreach (var downstreamBlock in downstreamBlocks)
        {
            // Get or create barrier (lazy initialization)
            // Use active in-degree to handle cases where some upstream sources are exhausted
            var lazyBarrier = context.Barriers.GetOrAdd(
                downstreamBlock,
                _ => new Lazy<DependencyBarrier>(
                    () => new DependencyBarrier(downstreamBlock, context.GetActiveInDegree(downstreamBlock))));

            var barrier = lazyBarrier.Value;

            // Signal and check if ready
            if (barrier.Signal())
            {
                // All dependencies satisfied - transition to Ready
                context.BlockStates[downstreamBlock] = BlockExecutionState.Ready;

                // Enqueue for execution (if not blocked)
                TryEnqueue(downstreamBlock, context);
            }
        }
    }

    /// <inheritdoc />
    public void HandleBlockedBlock(IBlock blockedBlock, ExecutionContext context)
    {
        // Decrement warehouse counters for upstream blocks (cleanup)
        if (context.UpstreamBlocks.TryGetValue(blockedBlock, out var upstreamBlocks))
        {
            foreach (var upstreamBlock in upstreamBlocks)
            {
                if (context.Warehouses.TryGetValue(upstreamBlock, out var lazyWarehouse))
                {
                    lazyWarehouse.Value.DecrementConsumerCount();
                }
            }
        }

        // Signal downstream (so they can also be skipped when ready)
        SignalCompletion(blockedBlock, context);

        // Count as completed (skipped)
        context.IncrementProcessedShipments();
    }

    /// <inheritdoc />
    public void PrepareNextShipmentCycle(ExecutionContext context)
    {
        lock (context.ActiveSourcesLock)
        {
            foreach (var source in context.ActiveSources)
            {
                if (!context.IsBlocked(source))
                {
                    TryEnqueue(source, context);
                }
            }
        }
    }

    /// <summary>
    /// Calculates greedy priority based purely on completion pressure.
    /// </summary>
    /// <remarks>
    /// Priority = -CompletionPressure (negative for min-heap)
    /// </remarks>
    private float CalculatePriority(IBlock block, ExecutionContext context)
        => -CalculateCompletionPressure(block, context);

    /// <summary>
    /// Calculates Completion Pressure priority for a block.
    /// </summary>
    /// <remarks>
    /// Priority = Σ(WarehouseSize / RemainingConsumers) for all predecessors.
    /// Higher values indicate more memory pressure (should execute sooner).
    /// </remarks>
    private float CalculateCompletionPressure(IBlock block, ExecutionContext context)
    {
        float totalPressure = 0;

        // Use precomputed upstream blocks instead of LINQ
        if (!context.UpstreamBlocks.TryGetValue(block, out var predecessors))
            return totalPressure;

        foreach (var predecessor in predecessors)
        {
            if (context.Warehouses.TryGetValue(predecessor, out var lazyWarehouse))
            {
                var warehouse = lazyWarehouse.Value;
                float warehouseSize = warehouse.TotalSizeMp;
                int remainingConsumers = warehouse.RemainingConsumers;

                if (remainingConsumers > 0)
                {
                    totalPressure += warehouseSize / remainingConsumers;
                }
            }
        }

        return totalPressure;
    }
}
