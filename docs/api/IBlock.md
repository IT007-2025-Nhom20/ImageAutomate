# IBlock Interface

The `IBlock` interface defines the contract for all graph nodes (blocks) within the ImageAutomate dataflow system. It abstracts the underlying implementation of image manipulation operations, allowing the `PipelineGraph` and `GraphRenderPanel` to handle various block types uniformly.

## Definition

```csharp
public interface IBlock : INotifyPropertyChanged, IDisposable
{
    string Name { get; }
    string Title { get; }
    string Content { get; }

    IReadOnlyList<Socket> Inputs { get; }
    IReadOnlyList<Socket> Outputs { get; }

    IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs);
    IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs, CancellationToken cancellationToken);
    IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs);
    IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs, CancellationToken cancellationToken);
}
```

## Properties

### `Name`
*   **Type**: `string`
*   **Description**: The internal or immutable name of the block type (e.g., "ConvertBlock").
*   **Usage**: Used for identification.

### `Title`
*   **Type**: `string`
*   **Description**: The display header of the block.
*   **Usage**: Displayed as the main label in the block header on the graph.

### `Content`
*   **Type**: `string`
*   **Description**: The display content of the block.
*   **Usage**: Rendered in the body of the block node to give users quick insight into the block's settings.

### `Inputs` / `Outputs`
*   **Type**: `IReadOnlyList<Socket>`
*   **Description**: Collections of input and output sockets.
*   **Usage**: Defines the connectivity points for the block.

## Methods

### `Execute`

The `Execute` method has overloads to support synchronous and asynchronous execution patterns (via cancellation tokens), as well as socket-based or string-based input keys.

#### Socket-Keyed Execute
```csharp
IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
    IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs,
    CancellationToken cancellationToken = default)
```
*   **Description**: The primary execution method that accepts Socket-keyed inputs.
*   **Parameters**:
    *   `inputs`: Dictionary mapping input sockets to their work items.
    *   `cancellationToken`: Token to observe for cancellation requests.
*   **Returns**: Dictionary mapping output sockets to their work items.

#### String-Keyed Execute (Convenience Overload)
```csharp
IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
    IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs,
    CancellationToken cancellationToken = default)
```
*   **Description**: Convenience overload that accepts string socket IDs instead of Socket objects.
*   **Parameters**:
    *   `inputs`: Dictionary mapping socket IDs to their work items.
    *   `cancellationToken`: Token to observe for cancellation requests.
*   **Returns**: Dictionary mapping output sockets to their work items.
