/**
 * GraphRenderPanel.cs
 * 
 * Panel-based control for rendering pipeline graph
 */

using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Layout.Layered;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using GeomEdge = Microsoft.Msagl.Core.Layout.Edge;
using GeomNode = Microsoft.Msagl.Core.Layout.Node;
using MsaglPoint = Microsoft.Msagl.Core.Geometry.Point;

namespace ConvertBlockPoC;

public class GraphRenderPanel : Panel
{
    #region Exposed Properties

    [Category("Node Appearance")]
    [Description("Width of each node in the graph")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double NodeWidth
    {
        get => _nodeWidth;
        set
        {
            if (Math.Abs(_nodeWidth - value) > double.Epsilon)
            {
                _nodeWidth = value;
                if (_pGraph != null)
                    _pGraph.NodeWidth = value;
                Invalidate();
            }
        }
    }
    private double _nodeWidth = 200;

    [Category("Node Appearance")]
    [Description("Height of each node in the graph")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double NodeHeight
    {
        get => _nodeHeight;
        set
        {
            if (Math.Abs(_nodeHeight - value) > double.Epsilon)
            {
                _nodeHeight = value;
                if (_pGraph != null)
                    _pGraph.NodeHeight = value;
                Invalidate();
            }
        }
    }
    private double _nodeHeight = 100;

    [Category("Node Appearance")]
    [Description("Outline color for the selected block")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color SelectedBlockOutlineColor
    {
        get => _selectedBlockOutlineColor;
        set
        {
            _selectedBlockOutlineColor = value;
            Invalidate();
        }
    }
    private Color _selectedBlockOutlineColor = Color.Red;

    [Category("Node Appearance")]
    [Description("Connection socket size")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double SocketRadius
    {
        get => _socketRadius;
        set
        {
            _socketRadius = value;
            Invalidate();
        }
    }
    private double _socketRadius = 6;

    [Category("Graph Layout")]
    [Description("Spacing between columns (layers) in the graph")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double ColumnSpacing
    {
        get => _columnSpacing;
        set
        {
            _columnSpacing = value;
            Invalidate();
        }
    }
    private double _columnSpacing = 250;

    [Category("Graph Appearance")]
    [Description("Node render scale factor")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float RenderScale
    {
        get => _renderScale;
        set
        {
            _renderScale = value;
            Invalidate();
        }
    }
    private float _renderScale = 1.0f;

    #endregion

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PipelineGraph PGraph => _pGraph;

    private readonly PipelineGraph _pGraph;
    private PointF _panOffset = new(0, 0);
    private Point _lastMousePos;
    private bool _isPanning;

    public GraphRenderPanel()
    {
        DoubleBuffered = true;
        BackColor = Color.White;

        // Initialize PGraph after property defaults are set
        _pGraph = new PipelineGraph(_nodeWidth, _nodeHeight);

        Resize += (_, _) => Invalidate();
        MouseDown += OnMouseDownPan;
        MouseMove += OnMouseMovePan;
        MouseUp += OnMouseUpPan;
        MouseWheel += OnMouseWheelPan;
    }

    public void Initialize(ConvertBlock block)
    {
        _pGraph.CenterNode = _pGraph.AddNode(block);
        ComputeLayoutAndRender();
    }

    public void SetCenterBlock(ConvertBlock block)
    {
        var node = _pGraph.GetNode(block) ?? _pGraph.AddNode(block);
        _pGraph.CenterNode = node;
        ComputeLayoutAndRender();
    }

    public void AddBlockAndConnect(ConvertBlock block, ConvertBlock? connectTo = null)
    {
        var newNode = _pGraph.AddNode(block);

        if (connectTo != null)
        {
            var targetNode = _pGraph.GetNode(connectTo);
            if (targetNode != null)
                _pGraph.AddEdge(targetNode, newNode);
        }

        ComputeLayoutAndRender();
    }

    public void AddSuccessor(ConvertBlock block)
    {
        var newNode = _pGraph.AddNode(block);

        if (_pGraph.CenterNode != null)
            _pGraph.AddEdge(_pGraph.CenterNode, newNode);

        ComputeLayoutAndRender();
    }

    public void AddPredecessor(ConvertBlock block)
    {
        var newNode = _pGraph.AddNode(block);

        if (_pGraph.CenterNode != null)
            _pGraph.AddEdge(newNode, _pGraph.CenterNode);

        ComputeLayoutAndRender();
    }

    private void OnMouseDownPan(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _isPanning = true;
            _lastMousePos = e.Location;
            Cursor = Cursors.Hand;
        }
    }

    private void OnMouseUpPan(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _isPanning = false;
            Cursor = Cursors.Default;
        }
    }

    private void OnMouseMovePan(object? sender, MouseEventArgs e)
    {
        if (!_isPanning) return;

        float dx = e.X - _lastMousePos.X;
        float dy = e.Y - _lastMousePos.Y;

        ClampPanToBounds(ref dx, ref dy);

        _panOffset.X += dx;
        _panOffset.Y += dy;

        _lastMousePos = e.Location;
        Invalidate();
    }

    private void OnMouseWheelPan(object? sender, MouseEventArgs e)
    {
        float dx = 0;
        float dy = e.Delta / 4f;

        ClampPanToBounds(ref dx, ref dy);

        _panOffset.X += dx;
        _panOffset.Y += dy;
        Invalidate();
    }

    private void ClampPanToBounds(ref float dx, ref float dy, float margin = 20)
    {
        var graph = _pGraph.GeomGraph;
        var bounds = graph.BoundingBox;

        // AABB corners in graph space
        float graphLeft = (float)bounds.Left * _renderScale;
        float graphRight = (float)bounds.Right * _renderScale;
        float graphTop = (float)bounds.Top * _renderScale;
        float graphBottom = (float)bounds.Bottom * _renderScale;

        // Current screen positions of graph edges
        float centerX = Width / 2f;
        float centerY = Height / 2f;

        float screenLeft = centerX + _panOffset.X + graphLeft;
        float screenRight = centerX + _panOffset.X + graphRight;
        float screenTop = centerY + _panOffset.Y - graphTop;
        float screenBottom = centerY + _panOffset.Y - graphBottom;

        // Proposed new positions
        float newScreenLeft = screenLeft + dx;
        float newScreenRight = screenRight + dx;
        float newScreenTop = screenTop + dy;
        float newScreenBottom = screenBottom + dy;

        // Clamp horizontal
        if (newScreenLeft > Width - margin)
            dx = (Width - margin) - screenLeft;
        else if (newScreenRight < margin)
            dx = margin - screenRight;

        // Clamp vertical
        if (newScreenTop > Height - margin)
            dy = (Height - margin) - screenTop;
        else if (newScreenBottom < margin)
            dy = margin - screenBottom;
    }

    private void ComputeLayoutAndRender()
    {
        var graph = _pGraph.GeomGraph;

        var settings = new SugiyamaLayoutSettings
        {
            Transformation = PlaneTransformation.Rotation(Math.PI / 2),
            LayerSeparation = _columnSpacing,
            NodeSeparation = 30,
            EdgeRoutingSettings = { EdgeRoutingMode = Microsoft.Msagl.Core.Routing.EdgeRoutingMode.Spline },
            RandomSeedForOrdering = 0
        };

        var layout = new LayeredLayout(graph, settings);
        layout.Run();

        graph.UpdateBoundingBox();
        CenterGraphAtOrigin();
        Invalidate();
    }

    private void CenterGraphAtOrigin()
    {
        var graph = _pGraph.GeomGraph;
        var bounds = graph.BoundingBox;
        var centerOffset = new MsaglPoint(-bounds.Center.X, -bounds.Center.Y);

        foreach (var node in graph.Nodes)
            node.BoundaryCurve.Translate(centerOffset);

        foreach (var edge in graph.Edges)
            edge.Curve?.Translate(centerOffset);

        graph.UpdateBoundingBox();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var graph = _pGraph.GeomGraph;
        if (graph.Nodes.Count == 0) return;

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        float offsetX = Width / 2f + _panOffset.X;
        float offsetY = Height / 2f + _panOffset.Y;

        using Matrix transform = new();
        transform.Translate(offsetX, offsetY);
        transform.Scale(_renderScale, -_renderScale);
        g.Transform = transform;

        // Draw edges first
        foreach (var geomEdge in graph.Edges)
            DrawEdge(g, geomEdge);

        // Draw nodes on top
        foreach (var geomNode in graph.Nodes)
            DrawNode(g, geomNode);

        g.ResetTransform();
    }

    private static string GetEncodingOptionsDisplay(ConvertBlock block)
    {
        return block.TargetFormat switch
        {
            ImageFormat.Jpeg => $"Quality: {block.JpegOptions.Quality}",
            ImageFormat.Png => $"Compression: {block.PngOptions.CompressionLevel}",
            _ => "Options: Default"
        };
    }

    #region Rendering Utilities

    private void DrawEdge(Graphics g, GeomEdge geomEdge)
    {
        var sourceNode = geomEdge.Source;
        var targetNode = geomEdge.Target;

        if (sourceNode?.BoundingBox == null || targetNode?.BoundingBox == null)
            return;

        PointF start = new(
            (float)(sourceNode.Center.X + sourceNode.BoundingBox.Width / 2),
            (float)sourceNode.Center.Y
        );

        PointF end = new(
            (float)(targetNode.Center.X - targetNode.BoundingBox.Width / 2),
            (float)targetNode.Center.Y
        );

        using var edgePen = new Pen(Color.FromArgb(150, 150, 150), 2);
        g.DrawLine(edgePen, start, end);
    }

    private void DrawNode(Graphics g, GeomNode geomNode)
    {
        if (geomNode.UserData is not ConvertBlock block) return;

        var bounds = geomNode.BoundingBox;
        RectangleF rect = new(
            (float)bounds.Left,
            (float)bounds.Bottom,
            (float)bounds.Width,
            (float)bounds.Height
        );

        float radius = 8;
        var state = g.Save();

        bool isCenterBlock = geomNode == _pGraph.CenterNode;

        using (var bgBrush = new SolidBrush(Color.FromArgb(60, 60, 60)))
        using (var borderPen = new Pen(
            isCenterBlock ? _selectedBlockOutlineColor : Color.FromArgb(100, 100, 100),
            isCenterBlock ? 3 : 2))
        using (var path = CreateRoundedRectPath(rect, radius))
        {
            g.FillPath(bgBrush, path);
            g.DrawPath(borderPen, path);
        }

        RectangleF headerRect = new(rect.X, rect.Y, rect.Width, 25);
        using (var headerBrush = new SolidBrush(Color.FromArgb(80, 80, 80)))
        using (var headerPath = CreateRoundedRectPath(headerRect, radius, topOnly: true))
        {
            g.FillPath(headerBrush, headerPath);
        }

        g.Restore(state);
        state = g.Save();

        using (var flipMatrix = new Matrix(1, 0, 0, -1, 0, 2 * (rect.Y + rect.Height / 2)))
        {
            g.MultiplyTransform(flipMatrix);
        }

        using (var textBrush = new SolidBrush(Color.White))
        using (var labelFont = new Font("Segoe UI", 10, FontStyle.Bold))
        using (var detailFont = new Font("Segoe UI", 8))
        {
            g.DrawString("Convert", labelFont, textBrush,
                new PointF(rect.X + 10, rect.Y + 5));

            float yOffset = rect.Y + 35;
            string[] properties =
            [
                $"Format: {block.TargetFormat}",
                $"Re-encode: {block.AlwaysReEncode}",
                GetEncodingOptionsDisplay(block)
            ];

            foreach (var prop in properties)
            {
                g.DrawString(prop, detailFont, textBrush,
                    new PointF(rect.X + 10, yOffset));
                yOffset += 18;
            }
        }

        g.Restore(state);
        state = g.Save();

        DrawSocket(g, new PointF(rect.Left - 5, rect.Top + rect.Height / 2), isInput: true);
        DrawSocket(g, new PointF(rect.Right + 5, rect.Top + rect.Height / 2), isInput: false);

        g.Restore(state);
    }

    private static GraphicsPath CreateRoundedRectPath(RectangleF rect, float radius, bool topOnly = false)
    {
        GraphicsPath path = new();
        float diameter = radius * 2;

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);

        if (topOnly)
        {
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + radius);
        }
        else
        {
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        }

        path.CloseFigure();
        return path;
    }

    private void DrawSocket(Graphics g, PointF center, bool isInput)
    {
        float socketRadius = (float)_socketRadius;
        RectangleF socketRect = new(
            center.X - socketRadius,
            center.Y - socketRadius,
            socketRadius * 2,
            socketRadius * 2
        );

        var socketColor = isInput
            ? Color.FromArgb(100, 200, 100)
            : Color.FromArgb(200, 100, 100);

        using var socketBrush = new SolidBrush(socketColor);
        using var socketBorder = new Pen(Color.White, 1.5f);
        g.FillEllipse(socketBrush, socketRect);
        g.DrawEllipse(socketBorder, socketRect);
    }

    #endregion Rendering Utilities
}