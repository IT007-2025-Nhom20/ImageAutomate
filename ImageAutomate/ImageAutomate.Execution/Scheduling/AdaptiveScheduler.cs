using ImageAutomate.Core;

namespace ImageAutomate.Execution.Scheduling;

/// <summary>
/// Adaptive scheduler with live cost profiling and critical path analysis.
/// Experimental. Not yet implemented.
/// </summary>
/// <remarks>
/// Reserved for future implementation. All methods throw NotImplementedException.
/// </remarks>
internal sealed class AdaptiveScheduler : IScheduler
{
    private const string NotImplementedMessage =
        "Adaptive Mode is not yet implemented. Use ExecutionMode.SimpleDfs instead.";

    #region Reserved for Adaptive Mode Implementation

    #endregion

    public bool IsEmpty => throw new NotImplementedException(NotImplementedMessage);

    public bool TryEnqueue(IBlock block, ExecutionContext context)
        => throw new NotImplementedException(NotImplementedMessage);

    public IBlock? TryDequeue(ExecutionContext context)
        => throw new NotImplementedException(NotImplementedMessage);

    public void SignalCompletion(IBlock completedBlock, ExecutionContext context)
        => throw new NotImplementedException(NotImplementedMessage);

    public void HandleBlockedBlock(IBlock blockedBlock, ExecutionContext context)
        => throw new NotImplementedException(NotImplementedMessage);

    public void PrepareNextShipmentCycle(ExecutionContext context)
        => throw new NotImplementedException(NotImplementedMessage);
}
