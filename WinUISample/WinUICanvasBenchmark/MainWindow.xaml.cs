using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Diagnostics;
using Windows.Foundation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUICanvasBenchmark
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            DrawCurves();
        }

        private void DrawCurves()
        {
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++) // 1000 curves as an example
            {
                var points = GenerateRandomPoints(100); // Generate 100 points per curve
                var pathFigure = new PathFigure { StartPoint = points[0] };

                for (int j = 1; j < points.Length; j++)
                {
                    pathFigure.Segments.Add(new LineSegment { Point = points[j] });
                }

                var pathGeometry = new PathGeometry();
                pathGeometry.Figures.Add(pathFigure);

                var path = new Path
                {
                    Data = pathGeometry,
                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 1
                };

                DrawCanvas.Children.Add(path);
            }

            stopwatch.Stop();
            Debug.WriteLine("WinUI 3 drawing time: " + stopwatch.ElapsedMilliseconds + " ms");
        }

        private Point[] GenerateRandomPoints(int count)
        {
            Point[] points = new Point[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = new Point(_random.Next(0, 800), _random.Next(0, 600));
            }
            return points;
        }

        private readonly Random _random = new();
    }
}
