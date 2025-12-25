# ExecutionContext

`ExecutionContext` is an internal class that maintains the runtime state of a pipeline execution. While not a public API, it is central to the engine's operation.

## Responsibilities

*   **Topology Analysis**: Precomputes in-degrees, out-degrees, and adjacency lists for efficient graph traversal.
*   **State Management**: Tracks the execution state (`Pending`, `Running`, `Completed`, `Failed`) of each block.
*   **Synchronization**: Manages `Warehouse` (data buffers) and `DependencyBarrier` (synchronization primitives) instances.
*   **Progress Tracking**: Counters for active blocks and processed shipments.
*   **Source Management**: Tracks active source blocks to handle graph termination conditions.

## Key Properties

*   **`Graph`**: The `PipelineGraph` being executed.
*   **`Configuration`**: The `ExecutorConfiguration` in use.
*   **`Scheduler`**: The `IScheduler` strategy.
*   **`CancellationToken`**: Token for cancellation.
*   **`ActiveBlockCount`**: Current number of running blocks.
*   **`ProcessedShipmentCount`**: Total number of completed execution units.

## Methods (Internal)

*   `GetOrCreateWarehouse(IBlock block)`
*   `GetOrCreateBarrier(IBlock block, int dependencyCount)`
*   `GetBlockState(IBlock block)` / `SetBlockState(...)`
*   `RecordException(Exception exception)`
*   `MarkSourceActive(IBlock source)` / `MarkSourceInactive(...)`
*   `ResetForNextShipment()`: Clears transient state for the next processing cycle.
