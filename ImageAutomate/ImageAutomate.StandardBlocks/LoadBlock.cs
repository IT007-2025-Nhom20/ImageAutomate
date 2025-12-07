using ImageAutomate.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageAutomate.StandardBlocks;

internal class LoadBlock : IBlock
{
    #region Fields
    private readonly Socket _outputSocket = new("Load.out", "Image.out");
    private readonly IReadOnlyList<Socket> _inputs;
    private readonly IReadOnlyList<Socket> _outputs;

    private static readonly HttpClient HttpClient = new HttpClient();

    private string _sourcePath = string.Empty;
    private bool _loadFromUrl = false;
    private string _url = string.Empty;
    private bool _autoOrient = false;

    private bool _alwaysEncoder = false;
    private bool disposedValue = false;

    private int _width = 200;
    private int _height = 100;

    private string _title = "Load";
    private string _content = "Load image";
    #endregion

    #region InotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region Constructor
    public LoadBlock()
    {
        _inputs = Array.Empty<Socket>();
        _outputs = new[] { _outputSocket};
    }
    #endregion

    #region Basic Properties
    public string Name => "Load";

    public string Title 
    { 
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }
    public string Content 
    { 
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
    }

    [Category("Layout")]
    [Description("Width of the black node")]
    public int Width 
    { 
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
    }

    [Category("Layout")]
    [Description("Width of the black node")]
    public int Height 
    { 
        get => _height;
        set 
        {
            if (_height != value)
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }    
        }
    }
    #endregion

    #region Configuration Propertise
    [Category("Configuration")]
    [Description("File system path to the input image ")]
    public string SourcePath
    {
        get => _sourcePath;
        set
        {
            if (_sourcePath != value)
            {
                _sourcePath = value;
                OnPropertyChanged(nameof(SourcePath));
            }    
        }
    }

    [Category("Cofniguration")]
    [Description("If true, load the image from the specified URL instead of a local file")]
    public bool LoadFromUrl
    {
        get => _loadFromUrl;
        set
        {
            if (_loadFromUrl != value)
            {
                _loadFromUrl = value;
                OnPropertyChanged(nameof(LoadFromUrl));
            }    
        }
    }

    [Category("Configuration")]
    [Description("Remote URL of the image to load (used when LoadFromUrl = true)")]
    public string Url
    {
        get => _url;
        set
        {
            if (_url != value)
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }
    }

    [Category("Confiuration")]
    [Description("If true, applies EXIF orientation correction automatically")]
    public bool AutoOrient
    {
        get => _autoOrient;
        set
        {
            if (_autoOrient != value)
            {
                _autoOrient = value;
                OnPropertyChanged(nameof(AutoOrient));
            }
        }
    }
    #endregion

    #region Socket
    public IReadOnlyList<Socket> Inputs => _inputs;

    public IReadOnlyList<Socket> Outputs => _outputs;
    #endregion

    #region Execute
    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        // This block ignores upstream data; it is an entry point.
        var loaded = LoadSingleWorkItem();

        var list = loaded is null ? Array.Empty<IBasicWorkItem>() : new[] { loaded };

        var readOnly = new ReadOnlyCollection<IBasicWorkItem>(list);

        return new Dictionary<Socket, IReadOnlyList<IBasicWorkItem>>
            {
                { _outputSocket, readOnly }
            };
    }

    public IReadOnlyDictionary<string, IReadOnlyList<IBasicWorkItem>> Execute(IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        var loaded = LoadSingleWorkItem();

        var list = loaded is null ? Array.Empty<IBasicWorkItem>() : new[] { loaded };

        var readOnly = new ReadOnlyCollection<IBasicWorkItem>(list);

        return new Dictionary<string, IReadOnlyList<IBasicWorkItem>>
            {
                { _outputSocket.Id, readOnly }
            };
    }
    private IBasicWorkItem? LoadSingleWorkItem()
    {
        if (LoadFromUrl)
        {
            if (string.IsNullOrWhiteSpace(Url))
                throw new InvalidOperationException("LoadBlock: Url is required when LoadFromUrl = true.");

            return LoadFromUrlInternal(Url);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(SourcePath))
                throw new InvalidOperationException("LoadBlock: SourcePath is required when LoadFromUrl = false.");

            return LoadFromFileInternal(SourcePath);
        }
    }
    private IBasicWorkItem LoadFromFileInternal(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"LoadBlock: File not found at path '{path}'.", path);

        try
        {
            // 1. Mở stream và load ảnh
            using var fs = File.OpenRead(path);
            using var image = Image.Load<Rgba32>(fs);

            // 2. Lấy định dạng đã decode
            var format = image.Metadata.DecodedImageFormat;
            if (format is null)
                throw new InvalidOperationException($"LoadBlock: Unsupported or unknown image format for file '{path}'.");

            // 3. AutoOrient nếu bật

            if (AutoOrient)
            {
                image.Mutate(x => x.AutoOrient());
            }

            using var ms = new MemoryStream();
            image.Save(ms, format);
            var bytes = ms.ToArray();

            var metadata = new Dictionary<string, object>
            {
                ["ImageData"] = bytes,
                ["Format"] = MapFormat(format),
                ["SourcePath"] = path,
                ["SourceType"] = "File",
                ["AutoOriented"] = AutoOrient,
                ["Width"] = image.Width,
                ["Height"] = image.Height,
                ["LoadedAtUtc"] = DateTime.UtcNow
            };

            return new LoadBlockWorkItem(metadata);
        }
        catch (Exception ex) when (ex is IOException
                                   || ex is UnauthorizedAccessException
                                   || ex is SixLabors.ImageSharp.UnknownImageFormatException
                                   || ex is SixLabors.ImageSharp.InvalidImageContentException)
        {
            throw new InvalidOperationException(
                $"LoadBlock: Failed to load image from file '{path}': {ex.Message}", ex);
        }
    }

    private IBasicWorkItem LoadFromUrlInternal(string url)
    {
        try
        {
            // 1. HTTP GET
            var response = HttpClient.GetAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"LoadBlock: Failed to access URL '{url}'. Status code: {(int)response.StatusCode} {response.StatusCode}");
            }

            // 2. Validate content type = image/*
            var contentType = response.Content.Headers.ContentType;
            if (!IsImageContentType(contentType))
            {
                throw new InvalidOperationException(
                    $"LoadBlock: URL '{url}' does not contain an image resource. Content-Type: '{contentType?.MediaType}'.");
            }

            // 3. Đọc toàn bộ bytes về bộ nhớ
            var bytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();

            // 4. Load ảnh từ byte[] (ImageSharp 3.x)
            using var image = Image.Load<Rgba32>(bytes);

            // 5. Lấy định dạng đã decode
            var format = image.Metadata.DecodedImageFormat;
            if (format is null)
                throw new InvalidOperationException($"LoadBlock: Unsupported or unknown image format at URL '{url}'.");
            if (AutoOrient)
            {
                image.Mutate(x => x.AutoOrient());
            }

            using var ms = new MemoryStream();
            image.Save(ms, format);
            var outbytes = ms.ToArray();

            var metadata = new Dictionary<string, object>
            {
                ["ImageData"] = outbytes,
                ["Format"] = MapFormat(format),
                ["SourceUrl"] = url,
                ["SourceType"] = "Url",
                ["AutoOriented"] = AutoOrient,
                ["Width"] = image.Width,
                ["Height"] = image.Height,
                ["LoadedAtUtc"] = DateTime.UtcNow
            };

            return new LoadBlockWorkItem(metadata);
        }
        catch (Exception ex) when (ex is HttpRequestException
                                   || ex is TaskCanceledException
                                   || ex is SixLabors.ImageSharp.UnknownImageFormatException
                                   || ex is SixLabors.ImageSharp.InvalidImageContentException)
        {
            throw new InvalidOperationException(
                $"LoadBlock: Failed to load image from URL '{url}': {ex.Message}", ex);
        }
    }
    private static bool IsImageContentType(MediaTypeHeaderValue? contentType)
    {
        if (contentType?.MediaType is null)
            return false;

        return contentType.MediaType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
    }

    private static ImageFormat MapFormat(IImageFormat format)
    {
        var name = format.Name?.ToLowerInvariant() ?? string.Empty;

        return name switch
        {
            "jpeg" or "jpg" => ImageFormat.Jpeg,
            "png" => ImageFormat.Png,
            "gif" => ImageFormat.Gif,
            "bmp" => ImageFormat.Bmp,
            "pbm" => ImageFormat.Pbm,
            "tiff" => ImageFormat.Tiff,
            "tga" => ImageFormat.Tga,
            "webp" => ImageFormat.WebP,
            "qoi" => ImageFormat.Qoi,
            _ => ImageFormat.Png
        };
    }
    #endregion

    #region IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~LoadBlock()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    #region Nested WorkItem

    private sealed class LoadBlockWorkItem : IBasicWorkItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public IDictionary<string, object> Metadata { get; }

        public LoadBlockWorkItem(IDictionary<string, object> metadata)
        {
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }
    }

    #endregion
}
