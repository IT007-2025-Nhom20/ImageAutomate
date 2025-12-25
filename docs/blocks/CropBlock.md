# Crop Block

## Description
Block shall crop images to a specified region.
Supports multiple cropping modes including explicit rectangle, centered cropping, and anchor-based cropping.

---

## Configuration Parameters

### `CropMode`
Specifies how the crop region is determined.
- **Rectangle**: Uses explicit `X`, `Y` coordinates and `CropWidth`, `CropHeight`.
- **Center**: Centers the crop region of size `CropWidth` x `CropHeight` within the image.
- **Anchor**: Aligns the crop region of size `CropWidth` x `CropHeight` relative to an `AnchorPosition`.

### `X`, `Y`
- Only used in **Rectangle** mode.
- Top-left coordinates of the crop rectangle.

### `CropWidth`, `CropHeight`
- The dimensions of the resulting cropped image.
- Must be positive integers.

### `AnchorPosition`
- Only used in **Anchor** mode.
- Positions the crop rectangle relative to the source image.
- Options: `TopLeft`, `Top`, `TopRight`, `Left`, `Center`, `Right`, `BottomLeft`, `Bottom`, `BottomRight`.

---

## Acceptance Criteria
- Output image has dimensions `CropWidth` x `CropHeight`.
- Crop region is correctly positioned according to `CropMode` and parameters.
- Throws error if crop region exceeds source image bounds.

---

## UI Behaviour
- **CropMode** dropdown selects the mode.
- **X, Y** visible only when `CropMode` is `Rectangle`.
- **AnchorPosition** visible only when `CropMode` is `Anchor`.
- **CropWidth, CropHeight** always visible.

---

## Operational Behaviour

### Bounds Checking
- **Rectangle Mode**: Throws if `X + CropWidth > SourceWidth` or `Y + CropHeight > SourceHeight`.
- **Center/Anchor Mode**: Throws if `CropWidth > SourceWidth` or `CropHeight > SourceHeight`.

### Execution
- Applies `Image.Mutate(x => x.Crop(rectangle))` using the calculated rectangle.
