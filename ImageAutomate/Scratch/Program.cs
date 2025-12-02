using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using Image image = new Image<Rgba32>(100, 100);

Star star = new(50, 50, 5, 20, 45);

PointF[] points = star.Points.ToArray();

Color[] colors =
{
    Color.Red
};

PathGradientBrush brush = new(points, colors);

image.Mutate(x => x.Fill(brush).Rotate(180));

image.Save("star.png");