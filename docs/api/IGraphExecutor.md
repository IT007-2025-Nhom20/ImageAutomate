# IGraphExecutor

The `IGraphExecutor` interface defines the contract for executing a `PipelineGraph`.

## Definition

```csharp
public interface IGraphExecutor
{
    void Execute(PipelineGraph graph);
    Task ExecuteAsync(
        PipelineGraph graph,
        ExecutorConfiguration? configuration = null,
        CancellationToken cancellationToken = default);
}
```

## Methods

### `Execute(PipelineGraph graph)`
Executes the provided graph synchronously. This method blocks the calling thread until execution completes.

### `ExecuteAsync(...)`
Executes the graph asynchronously.

*   **Parameters**:
    *   `graph`: The `PipelineGraph` to execute.
    *   `configuration`: Optional `ExecutorConfiguration` to tune execution parameters (e.g., parallelism, shipment size).
    *   `cancellationToken`: Token to request cooperative cancellation of the pipeline.
*   **Returns**: A `Task` representing the asynchronous operation.
