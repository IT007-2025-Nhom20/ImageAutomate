using ImageAutomate.Core;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAutomate.StandardBlocks;

/// <summary>
/// A block that adjusts the brightness of an image.
/// </summary>
public class BrightnessBlock : IBlock
{
    #region Fields

    private readonly IReadOnlyList<Socket> _inputs = [new("Brightness.In", "Image.In")];
    private readonly IReadOnlyList<Socket> _outputs = [new("Brightness.Out", "Image.Out")];

    private bool _disposed;

    private float _bright = 1.0f;

    #endregion

    #region IBlock basic

    /// <inheritdoc />
    public string Name => "Brightness";

    /// <inheritdoc />
    public string Title => "Brightness";

    /// <inheritdoc />
    public string Content => $"Brightness: {Bright}";

    #endregion

    #region Sockets

    /// <inheritdoc />
    public IReadOnlyList<Socket> Inputs => _inputs;
    /// <inheritdoc />
    public IReadOnlyList<Socket> Outputs => _outputs;

    #endregion

    #region Configuration

    /// <summary>
    /// Gets or sets the brightness factor.
    /// 1.0 = no change, &lt; 1.0 = darker, &gt; 1.0 = brighter.
    /// </summary>
    [Category("Configuration")]
    [Description("Brightness factor. 1.0 = no change, <1.0 = darker, >1.0 = brighter.")]   
    public float Bright
    {
        get => _bright;
        set
        {
            var clamped = Math.Clamp(value, 0.0f, 3.0f);
            if (Math.Abs(_bright - clamped) > float.Epsilon)
            {
                _bright = clamped;
                OnPropertyChanged(nameof(Bright));
            }
        }
    }

    #endregion

    #region INotifyPropertyChanged

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

    #region Execute

    /// <inheritdoc />
    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        return Execute(inputs, CancellationToken.None);
    }

    /// <inheritdoc />
    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<Socket, IReadOnlyList<IBasicWorkItem>> inputs, CancellationToken cancellationToken)
    {
        return Execute(inputs.ToDictionary(kvp => kvp.Key.Id, kvp => kvp.Value), cancellationToken);
    }

    /// <inheritdoc />
    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs)
    {
        return Execute(inputs, CancellationToken.None);
    }

    /// <inheritdoc />
    public IReadOnlyDictionary<Socket, IReadOnlyList<IBasicWorkItem>> Execute(
        IDictionary<string, IReadOnlyList<IBasicWorkItem>> inputs, CancellationToken cancellationToken)
    {
        if (!inputs.TryGetValue(_inputs[0].Id, out var inItems))
            throw new ArgumentException($"Input items not found for the expected input socket {_inputs[0].Id}.", nameof(inputs));

        var outputItems = new List<IBasicWorkItem>();

        foreach (var sourceItem in inItems.OfType<WorkItem>())
        {
            cancellationToken.ThrowIfCancellationRequested();
            sourceItem.Image.Mutate(x => x.Brightness(Bright));
            outputItems.Add(sourceItem);
        }

        return new Dictionary<Socket, IReadOnlyList<IBasicWorkItem>> { { _outputs[0], outputItems } };
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
