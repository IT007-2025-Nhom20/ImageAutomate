# WorkItem

`WorkItem` is the fundamental unit of data in the ImageAutomate pipeline. It encapsulates the data being processed (images) along with metadata.

## Interfaces

### `IBasicWorkItem`
The base interface for all work items.

```csharp
public interface IBasicWorkItem : IDisposable, ICloneable
{
    Guid Id { get; }
    IImmutableDictionary<string, object> Metadata { get; }
}
```

*   **`Id`**: A unique identifier for the work item.
*   **`Metadata`**: A key-value store for additional information.
*   **`IDisposable`**: Ensures resources (images) can be released.
*   **`ICloneable`**: Supports deep cloning of the work item.

### `IWorkItem`
Represents a work item containing a single image.

```csharp
public interface IWorkItem : IBasicWorkItem
{
    Image Image { get; }
    float SizeMP { get; }
}
```

*   **`Image`**: The `SixLabors.ImageSharp.Image` object associated with this item.
*   **`SizeMP`**: The size of the image in megapixels (Width * Height / 1,000,000).

### `IBatchWorkItem`
Represents a work item containing a collection of images.

```csharp
public interface IBatchWorkItem : IBasicWorkItem
{
    IReadOnlyList<Image> Images { get; }
    float TotalSizeMP { get; }
}
```

*   **`Images`**: A read-only list of images.
*   **`TotalSizeMP`**: The cumulative size of all images in megapixels.

## Implementations

### `WorkItem`
The standard implementation of `IWorkItem`.

```csharp
public sealed class WorkItem(Image image, IImmutableDictionary<string, object>? metadata = null) : IWorkItem
```

*   **Construction**: Automatically calculates `SizeMP` upon initialization.
*   **Immutability**: The metadata is immutable.
*   **Lifecycle**: When `Dispose()` is called, the `Image` is disposed.
*   **Cloning**: `Clone()` creates a deep copy of the image and the work item.

### `BatchWorkItem`
The standard implementation of `IBatchWorkItem`.

```csharp
public sealed class BatchWorkItem(IEnumerable<Image> images, IImmutableDictionary<string, object>? metadata = null) : IBatchWorkItem
```

*   **Construction**: Automatically calculates `TotalSizeMP` upon initialization.
*   **Lifecycle**: When `Dispose()` is called, all images in the `Images` list are disposed.
*   **Cloning**: `Clone()` creates deep copies of all images in the list.
