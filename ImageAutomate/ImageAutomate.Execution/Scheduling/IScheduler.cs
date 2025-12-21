using ImageAutomate.Core;

namespace ImageAutomate.Execution.Scheduling;

/// <summary>
/// Interface for pipeline block schedulers.
/// </summary>
internal interface IScheduler
{
    /// <summary>
    /// Enqueues a block for execution.
    /// </summary>
    /// <param name="block">The block to enqueue.</param>
    /// <param name="context">The execution context.</param>
    void Enqueue(IBlock block, ExecutionContext context);

    /// <summary>
    /// Attempts to dequeue the highest priority block.
    /// </summary>
    /// <param name="context">The execution context.</param>
    /// <returns>The next block to execute, or null if the queue is empty.</returns>
    IBlock? TryDequeue(ExecutionContext context);

    /// <summary>
    /// Gets whether the scheduler queue is empty.
    /// </summary>
    bool IsEmpty { get; }
}
