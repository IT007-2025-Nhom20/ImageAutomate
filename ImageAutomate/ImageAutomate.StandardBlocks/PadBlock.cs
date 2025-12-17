using ImageAutomate.Core;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAutomate.StandardBlocks;

public class PadBlock
{
    #region Fields

    private readonly IReadOnlyList<Socket> _inputs = [new("Pad.In", "Image.In")];
    private readonly IReadOnlyList<Socket> _outputs = [new("Pad.Out", "Image.Out")];

    private bool _disposed;

    private int _nodeWidth = 200;
    private int _nodeHeight = 100;

    private int _padWidth;
    private int _padHeight;
    #endregion

    #region IBlock basic

    public string Name => "Pad";

    public string Title => "Pad";

    public string Content => $"Pad width: {PadWidth}\nPadHeight: {PadHeight}";

    #endregion

    #region Layout

    [Category("Layout")]
    [Description("Width of the block node")]
    public int Width
    {
        get => _nodeWidth;
        set
        {
            if (_nodeWidth != value)
            {
                _nodeWidth = value;
                OnPropertyChanged(nameof(Width));
            }
        }
    }

    [Category("Layout")]
    [Description("Height of the block node")]
    public int Height
    {
        get => _nodeHeight;
        set
        {
            if (_nodeHeight != value)
            {
                _nodeHeight = value;
                OnPropertyChanged(nameof(Height));
            }
        }
    }

    #endregion

    #region Sockets

    public IReadOnlyList<Socket> Inputs => _inputs;
    public IReadOnlyList<Socket> Outputs => _outputs;

    #endregion

    #region Configuration

    [Category("Configuration")]
    [Description("The new height")]
    public int PadWidth
    {
        get => _padWidth;
        set
        {
            if (_padWidth != value)
            {
                _padWidth = value;
                OnPropertyChanged(nameof(_padWidth));
            }
        }
    }

    [Category("Configuration")]
    [Description("The new height")]
    public int PadHeight
    {
        get => _padHeight;
        set
        {
            if (_padHeight != value)
            {
                _padHeight = value;
                OnPropertyChanged(nameof(_padHeight));
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
        return Execute(inputs.ToDictionary(kvp => kvp.Key.Id, kvp => kvp.Value));
    }

    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        if (!inputs.TryGetValue(_inputs[0].Id, out var inItems))
            throw new ArgumentException($"Input items not found for the expected input socket {_inputs[0].Id}.", nameof(inputs));

        var outputItems = new List<IBasicWorkItem>();

        foreach (var sourceItem in inItems.OfType<WorkItem>())
        {
            sourceItem.Image.Mutate(x => x.Pad(PadWidth, PadHeight));
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
