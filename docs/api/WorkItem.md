# Work Items

The `IWorkItem` family of interfaces defines the units of data that flow through the ImageAutomate pipeline.

## Core Interfaces

### `IBasicWorkItem`
The base interface for all work items.

```csharp
public interface IBasicWorkItem : IDisposable, ICloneable
{
    Guid Id { get; }
    IImmutableDictionary<string, object> Metadata { get; }
}
```

*   **`Id`**: Unique identifier for the work item.
*   **`Metadata`**: Immutable dictionary for storing additional information (e.g., file paths, format info).
*   **`IDisposable`**: Ensures resources (like images) are released.
*   **`ICloneable`**: Supports deep copying for branching paths in the graph.

### `IWorkItem`
Represents a single image work item.

```csharp
public interface IWorkItem : IBasicWorkItem
{
    Image Image { get; }
    float SizeMP { get; }
}
```

*   **`Image`**: The `SixLabors.ImageSharp.Image` object.
*   **`SizeMP`**: The size of the image in megapixels (Width * Height / 1,000,000).

### `IBatchWorkItem`
Represents a collection of images processed as a single unit.

```csharp
public interface IBatchWorkItem : IBasicWorkItem
{
    IReadOnlyList<Image> Images { get; }
    float TotalSizeMP { get; }
}
```

*   **`Images`**: A list of `Image` objects.
*   **`TotalSizeMP`**: The cumulative size of all images in the batch.

## Implementations

### `WorkItem`
Standard implementation of `IWorkItem`.
*   Constructor: `WorkItem(Image image, IImmutableDictionary<string, object>? metadata = null)`
*   Automatically calculates `SizeMP`.
*   Deep clones the image when `Clone()` is called.

### `BatchWorkItem`
Standard implementation of `IBatchWorkItem`.
*   Constructor: `BatchWorkItem(IEnumerable<Image> images, IImmutableDictionary<string, object>? metadata = null)`
*   Automatically calculates `TotalSizeMP`.
*   Deep clones all images when `Clone()` is called.
