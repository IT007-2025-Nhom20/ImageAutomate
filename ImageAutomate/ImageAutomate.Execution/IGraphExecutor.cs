using ImageAutomate.Core;

namespace ImageAutomate.Execution;

/// <summary>
/// Interface for pipeline graph executors.
/// </summary>
public interface IGraphExecutor
{
    /// <summary>
    /// Executes the pipeline graph synchronously.
    /// </summary>
    /// <param name="graph">The pipeline graph to execute.</param>
    void Execute(PipelineGraph graph);

    /// <summary>
    /// Executes the pipeline graph asynchronously with optional configuration.
    /// </summary>
    /// <param name="graph">The pipeline graph to execute.</param>
    /// <param name="configuration">Execution configuration (optional).</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    Task ExecuteAsync(
        PipelineGraph graph,
        ExecutorConfiguration? configuration = null,
        CancellationToken cancellationToken = default);
}