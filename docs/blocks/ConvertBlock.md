# Convert Block

## Description
Block shall convert images between supported formats using ImageSharp.
Supports re-encoding, metadata preservation, alpha handling, and configurable encoder options.

---

## Configuration Parameters

### TargetFormat
Specifies desired output format. Supported values:
- Bmp
- Gif
- Jpeg
- Png
- Tiff
- Tga
- WebP
- Qoi
- (Pbm, Unknown are present in enum but may be unimplemented)

### AlwaysEncode
Boolean.
- false = passthrough unless encoder options change or format changes
- true = always re-encode

### EncodingOptions
Format-specific encoder configuration:
- JPEG: Quality
- PNG: CompressionLevel
- WEBP: Quality, Lossless
- TIFF: Compression
- BMP: BitsPerPixel
- GIF: UseDithering, ColorPaletteSize
- TGA: Compress
- QOI: IncludeAlpha
Displayed only for relevant TargetFormat.

---

## Acceptance Criteria
- Produces a valid output file in TargetFormat.
- Encoder parameters applied correctly.
- Metadata preserved only when applicable (logic inside Execute handles deep cloning/modification).
- Passthrough occurs only when source and target formats match and AlwaysEncode = false.
- Transparency preserved for formats with alpha support; flattened for non-alpha formats.

---

## UI Behaviour
- TargetFormat dropdown displays supported formats.
- EncodingOptions shows only parameters relevant to selected TargetFormat.
- JPEG  exposes Quality list.
- PNG exposes CompressionLevel list.
- WEBP exposes  Quality list and Lossless true/false.
- BMP exposes BitsPerPixel value.
- GIF exposes UseDithering true/false and ColorPaletteSize value.
- TIFF exposes Compression list.
- TGA exposes Compress true/false.
- QOI exposes IncludeAlpha true/false

---

## Operational Behaviour

### Format Detection
Input format auto-detected using:
```csharp
var info = Image.Identify(stream);
```

### Conversion Rules
- Re-encode when:
  - TargetFormat differs from source format
  - EncodingOptions provided
  - AlwaysEncode = true
- Passthrough allowed only when:
  - Formats match
  - No EncodingOptions set
  - AlwaysEncode = false

### Metadata
Metadata is passed via `WorkItem` metadata dictionary, setting "Format" and "EncodingOptions".
The actual image conversion happens later (possibly in SaveBlock or during processing if forced).
**Note**: The current `ConvertBlock` implementation updates metadata but returns a `WorkItem` with the *original* image if not explicitly re-encoded immediately?
Wait, the code in `Execute` creates a new `WorkItem` with the *same* image but updated metadata. This suggests the actual pixel conversion might be deferred or handled by `SaveBlock` using the metadata instructions.
The `Execute` method sets `Format` and `EncodingOptions` in metadata.

### Transparency
- For non-alpha formats (JPEG, BMP), transparency is flattened to white (handled by encoder).
- Alpha preserved for PNG, TGA, TIFF, WebP, QOI.

---

## Technical Notes
- ImageSharp supports all listed formats.
- ICC profiles preserved unless explicitly removed.
- Animated GIFs: only first frame processed (Image is single frame in WorkItem).
- Large batch operations may require memory warnings and sequential processing.
