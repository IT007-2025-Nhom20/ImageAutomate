# ExecutionContext

`ExecutionContext` manages the runtime state of a pipeline execution. It encapsulates the topology analysis, state tracking, and synchronization primitives used by the Execution Engine.

> **Note**: This class is `internal` to the `ImageAutomate.Execution` assembly and is not intended for direct public use. However, understanding its role is crucial for understanding the engine's internal mechanics.

## Responsibilities

*   **Topology Analysis**: Precomputes In-Degree, Out-Degree, and Adjacency lists (Upstream/Downstream).
*   **State Tracking**: Tracks the `BlockExecutionState` (Pending, Ready, Running, Completed) of each block.
*   **Synchronization**: Manages `Warehouse` (Data) and `DependencyBarrier` (Control) instances.
*   **Cycle Management**: Handles "Active Sources" and resets state for shipment-based execution.

## Key Properties

*   **`Graph`**: The `PipelineGraph` being executed.
*   **`ActiveBlockCount`**: The number of currently executing blocks.
*   **`ProcessedShipmentCount`**: A metric for tracking progress across multiple shipment cycles.
*   **`HasActiveSources`**: Indicates if any source block is still capable of producing data.

## Lifecycle Management

1.  **Initialization**: Computes topology and initializes degrees.
2.  **Shipment Cycle**:
    *   `InitializeActiveConnections()`: Determines which connections are active based on available sources.
    *   `ResetForNextShipment()`: Clears Warehouses/Barriers and resets block states for the next batch of data.
