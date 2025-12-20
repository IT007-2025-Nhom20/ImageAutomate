namespace ImageAutomate.Execution;

/// <summary>
/// Represents the execution state of a block in the pipeline.
/// </summary>
internal enum BlockExecutionState
{
    /// <summary>
    /// Block is ready to execute (all dependencies satisfied).
    /// </summary>
    Ready,

    /// <summary>
    /// Block is currently executing on a worker thread.
    /// </summary>
    Running,

    /// <summary>
    /// Block has completed execution successfully.
    /// </summary>
    Completed,

    /// <summary>
    /// Block execution threw an exception.
    /// </summary>
    Failed,

    /// <summary>
    /// Block is downstream of a failed block and will be skipped.
    /// </summary>
    Poisoned,

    /// <summary>
    /// Block execution was cancelled by the user.
    /// </summary>
    Cancelled
}
