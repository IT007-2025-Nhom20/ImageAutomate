# ImageAutomate WebP Format Extension

A proper ImageSharp format extension for WebP that integrates with ImageAutomate's format registry system.

## Overview

This extension implements the necessary contracts to make WebP available to `ConvertBlock` and `LoadBlock` through a central format registry. It follows ImageSharp's extension protocol and provides a bridge to ImageAutomate's block system.

## Architecture

### Components

1. **WebPFormat** - Implements `IImageFormat` from ImageSharp
   - Singleton instance providing format metadata
   - File extensions: `.webp`
   - MIME type: `image/webp`

2. **WebPEncoder** - Implements `IImageEncoder` from ImageSharp
   - Wraps ImageSharp's built-in `WebpEncoder`
   - Integrates with `WebPOptions` for configuration
   - Supports both lossy and lossless compression

3. **WebPDecoder** - Implements `IImageDecoder` from ImageSharp
   - Wraps ImageSharp's built-in `WebpDecoder`
   - Handles WebP image decoding

4. **WebPOptions** - Configuration class for ImageAutomate
   - Lossless/Lossy mode selection
   - Quality control (0-100)
   - Encoding method (Fastest/Default/BestQuality)
   - Near-lossless quality
   - Alpha compression control
   - Implements `INotifyPropertyChanged` for UI binding

5. **WebPFormatRegistration** - Registration utility
   - Registers format with ImageSharp
   - Registers with ImageAutomate's format registry
   - Provides factory methods for encoder/decoder creation

6. **CoreImitation** - Simulated format registry interface
   - Defines `IImageFormatRegistry` interface
   - Provides dummy implementation for type checking
   - Located in `ImageAutomate.Core` namespace (illegally, as requested)

## Format Registry System

The format registry provides a central mapping of:
- Format Name → IImageFormat
- Format Name → Encoder Factory (with options)
- Format Name → Decoder Factory

This allows blocks like `ConvertBlock` and `LoadBlock` to:
1. Discover available formats
2. Get appropriate encoders with options
3. Get appropriate decoders
4. Map file extensions to formats

## Usage

### Registration

```csharp
using ImageAutomate.WebPExtension;
using ImageAutomate.Core;

// Create or get the format registry
var registry = new ImageFormatRegistry();

// Register WebP format
WebPFormatRegistration.RegisterWebPFormat(registry);
```

### Creating Encoders

```csharp
// With default options
var encoder = WebPFormatRegistration.CreateEncoder();

// With custom options
var options = new WebPOptions
{
    Lossless = false,
    Quality = 85f,
    Method = WebPEncodingMethod.BestQuality
};
var encoder = WebPFormatRegistration.CreateEncoder(options);

// Use the encoder
using var image = Image.Load("input.png");
using var output = File.Create("output.webp");
encoder.Encode(image, output, CancellationToken.None);
```

### Creating Decoders

```csharp
var decoder = WebPFormatRegistration.CreateDecoder();

using var input = File.OpenRead("input.webp");
using var image = decoder.Decode(DecoderOptions.Default, input, CancellationToken.None);
```

### Using with Format Registry

```csharp
// Get encoder from registry
var encoder = registry.GetEncoder("WebP", new WebPOptions { Quality = 90f });

// Get decoder from registry
var decoder = registry.GetDecoder("WebP");

// Check if format is registered
bool isAvailable = registry.IsFormatRegistered("WebP");
```

## Integration with ConvertBlock

Once `ConvertBlock` is updated to use the format registry, it will:

1. Query the registry for available formats
2. Get the appropriate encoder based on target format
3. Apply user-configured options (WebPOptions)
4. Encode the image with the configured encoder

Example future integration:
```csharp
// In ConvertBlock.Execute()
var formatName = TargetFormat.ToString();
var encoder = _formatRegistry.GetEncoder(formatName, _webpOptions);
if (encoder != null)
{
    workItem.Image.Save(stream, encoder);
}
```

## Integration with LoadBlock

`LoadBlock` can use the registry to:

1. Detect format from file extension
2. Get appropriate decoder
3. Decode the image

Example future integration:
```csharp
// In LoadBlock.Execute()
var extension = Path.GetExtension(filePath).TrimStart('.');
var decoder = _formatRegistry.GetDecoder(extension);
if (decoder != null)
{
    using var stream = File.OpenRead(filePath);
    var image = decoder.Decode(DecoderOptions.Default, stream, cancellationToken);
}
```

## WebP Options Reference

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| Lossless | bool | false | Enable lossless compression |
| Quality | float | 75.0 | Quality factor (0-100, lossy mode only) |
| FileFormat | enum | Lossy | Derived from Lossless setting |
| Method | enum | Default | Encoding speed/quality tradeoff |
| NearLossless | int | 100 | Near-lossless quality (0-100, lossless mode only) |
| UseAlphaCompression | bool | true | Enable alpha channel compression |

### Encoding Methods
- **Fastest** (0) - Fast encoding, larger files
- **Default** (4) - Balanced speed and size
- **BestQuality** (6) - Slow encoding, smaller files

## Building

```bash
dotnet build ImageAutomate.WebPExtension.csproj
```

## Dependencies

- .NET 9.0
- SixLabors.ImageSharp 3.1.12
- ImageAutomate.Core (project reference)

## Notes

- **CoreImitation.cs** contains a simulated registry interface to avoid modifying Core
- The actual Core project would need to implement `IImageFormatRegistry` for full integration
- ImageSharp already has WebP support; this extension provides integration with ImageAutomate's architecture
- The format registry pattern allows any format to be added without modifying existing blocks
