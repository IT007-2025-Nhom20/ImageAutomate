using ImageAutomate.Core;

namespace ImageAutomate.Execution.Scheduling;

/// <summary>
/// Interface for pipeline block schedulers.
/// </summary>
/// <remarks>
/// Schedulers are responsible for:
/// <list type="bullet">
/// <item>Tracking block readiness via dependency barriers</item>
/// <item>Prioritizing block execution order</item>
/// <item>Handling blocked block propagation</item>
/// <item>Managing shipment cycle transitions</item>
/// </list>
/// </remarks>
internal interface IScheduler
{
    /// <summary>
    /// Gets whether the scheduler queue is empty.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Enqueues a block for execution if it's ready.
    /// </summary>
    /// <param name="block">The block to enqueue.</param>
    /// <param name="context">The execution context.</param>
    /// <returns>True if the block was enqueued; false if blocked or already queued.</returns>
    bool TryEnqueue(IBlock block, ExecutionContext context);

    /// <summary>
    /// Attempts to dequeue the highest priority block that is ready for execution.
    /// </summary>
    /// <param name="context">The execution context.</param>
    /// <returns>The next block to execute, or null if no ready blocks available.</returns>
    IBlock? TryDequeue(ExecutionContext context);

    /// <summary>
    /// Signals that a block has completed execution, updating downstream barriers.
    /// </summary>
    /// <param name="completedBlock">The block that completed.</param>
    /// <param name="context">The execution context.</param>
    /// <remarks>
    /// This method handles:
    /// <list type="bullet">
    /// <item>Signaling dependency barriers for downstream blocks</item>
    /// <item>Enqueueing blocks whose dependencies are satisfied</item>
    /// </list>
    /// </remarks>
    void SignalCompletion(IBlock completedBlock, ExecutionContext context);

    /// <summary>
    /// Notifies the scheduler that a block is blocked and cannot execute.
    /// </summary>
    /// <param name="blockedBlock">The block that is blocked.</param>
    /// <param name="context">The execution context.</param>
    /// <remarks>
    /// Blocked blocks should signal their barriers (to unblock waiting downstream)
    /// and clean up upstream warehouses without executing.
    /// </remarks>
    void HandleBlockedBlock(IBlock blockedBlock, ExecutionContext context);

    /// <summary>
    /// Prepares the scheduler for the next shipment cycle.
    /// </summary>
    /// <param name="context">The execution context.</param>
    /// <remarks>
    /// Re-enqueues all active sources that are not blocked.
    /// Called when a shipment cycle completes but sources still have data.
    /// </remarks>
    void PrepareNextShipmentCycle(ExecutionContext context);
}
