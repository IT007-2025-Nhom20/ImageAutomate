using ImageAutomate.Core;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Normalization;
using System.ComponentModel;

namespace ImageAutomate.StandardBlocks;

public class HistogramEqualizationBlock : IBlock
{
    #region Fields

    private readonly IReadOnlyList<Socket> _inputs = [new("HistEq.In", "Image.In")];
    private readonly IReadOnlyList<Socket> _outputs = [new("HistEq.Out", "Image.Out")];

    private bool _disposed;

    // Configuration fields
    private HistogramEqualizationMethod _method = HistogramEqualizationMethod.Global;
    private bool _clipHistogram = true;
    private int _clipLimit = 350;
    private int _luminanceLevels = 256;
    private int _numberOfTiles = 8;
    private bool _syncChannels = true;

    // Layout fields
    private double _x;
    private double _y;
    private int _width;
    private int _height;
    private string _title = "Histogram Equalization";

    #endregion

    public HistogramEqualizationBlock()
        : this(250, 200)
    {
    }

    public HistogramEqualizationBlock(int width, int height)
    {
        _width = width;
        _height = height;
    }

    #region IBlock basic

    [Browsable(false)]
    public string Name => "HistogramEqualization";

    [Category("Title")]
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

    [Browsable(false)]
    public string Content => $"Method: {Method}";

    #endregion

    #region Layout Properties

    [Category("Layout")]
    public double X
    {
        get => _x;
        set
        {
            if (Math.Abs(_x - value) > double.Epsilon)
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
    }

    [Category("Layout")]
    public double Y
    {
        get => _y;
        set
        {
            if (Math.Abs(_y - value) > double.Epsilon)
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
    }

    [Category("Layout")]
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

    #region Sockets

    [Browsable(false)]
    public IReadOnlyList<Socket> Inputs => _inputs;
    [Browsable(false)]
    public IReadOnlyList<Socket> Outputs => _outputs;

    #endregion

    #region Configuration

    [Category("Configuration")]
    [Description("Global: Apply to whole image. Adaptive: Apply locally to tiles (better detail).")]
    public HistogramEqualizationMethod Method
    {
        get => _method;
        set
        {
            if (_method != value)
            {
                _method = value;
                OnPropertyChanged(nameof(Method));
                OnPropertyChanged(nameof(Content));
            }
        }
    }

    [Category("Configuration")]
    [Description("If true, limits the contrast amplification to avoid noise (Used in Adaptive mode).")]
    public bool ClipHistogram
    {
        get => _clipHistogram;
        set
        {
            if (_clipHistogram != value)
            {
                _clipHistogram = value;
                OnPropertyChanged(nameof(ClipHistogram));
            }
        }
    }

    [Category("Configuration")]
    [Description("The contrast limit value for clipping. Higher values allow more contrast but more noise.")]
    public int ClipLimit
    {
        get => _clipLimit;
        set
        {
            if (_clipLimit != value)
            {
                _clipLimit = value;
                OnPropertyChanged(nameof(ClipLimit));
            }
        }
    }

    [Category("Configuration")]
    [Description("The number of luminance levels (bins). Standard is 256 for 8-bit images.")]
    public int LuminanceLevels
    {
        get => _luminanceLevels;
        set
        {
            if (_luminanceLevels != value)
            {
                _luminanceLevels = value;
                OnPropertyChanged(nameof(LuminanceLevels));
            }
        }
    }

    [Category("Configuration")]
    [Description("The number of tiles (grid size) for Adaptive mode (e.g., 8 means 8x8 grid).")]
    public int NumberOfTiles
    {
        get => _numberOfTiles;
        set
        {
            if (_numberOfTiles != value)
            {
                _numberOfTiles = value;
                OnPropertyChanged(nameof(NumberOfTiles));
            }
        }
    }

    [Category("Configuration")]
    [Description("Whether to synchronize equalization across color channels.")]
    public bool SyncChannels
    {
        get => _syncChannels;
        set
        {
            if (_syncChannels != value)
            {
                _syncChannels = value;
                OnPropertyChanged(nameof(SyncChannels));
            }
        }
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

    #region Execute

    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        return Execute(inputs, CancellationToken.None);
    }

    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs, CancellationToken cancellationToken)
    {
        return Execute(inputs.ToDictionary(kvp => kvp.Key.Id, kvp => kvp.Value), cancellationToken);
    }

    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        return Execute(inputs, CancellationToken.None);
    }

    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(inputs, nameof(inputs));
        if (!inputs.TryGetValue(_inputs[0].Id, out var inItems))
            throw new ArgumentException($"Input items not found for the expected input socket {_inputs[0].Id}.", nameof(inputs));

        var outputItems = new List<IBasicWorkItem>();

        // Create options once
        var options = new HistogramEqualizationOptions
        {
            Method = this.Method,
            ClipHistogram = this.ClipHistogram,
            ClipLimit = this.ClipLimit,
            LuminanceLevels = this.LuminanceLevels,
            NumberOfTiles = this.NumberOfTiles,
            SyncChannels = this.SyncChannels
        };

        foreach (var sourceItem in inItems.OfType<WorkItem>())
        {
            cancellationToken.ThrowIfCancellationRequested();

            sourceItem.Image.Mutate(x => x.HistogramEqualization(options));

            outputItems.Add(sourceItem);
        }

        return new Dictionary<Socket, IReadOnlyList<IBasicWorkItem>>
            {
                { _outputs[0], outputItems }
            };
    }

    #endregion

    #region IDisposable

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}