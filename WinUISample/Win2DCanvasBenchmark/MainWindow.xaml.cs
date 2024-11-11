using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Win2DCanvasBenchmark
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var stopwatch = Stopwatch.StartNew();

            var session = args.DrawingSession;
            for (int i = 0; i < 1000; i++) // Draw 1000 curves
            {
                var points = GenerateRandomPoints(100); // 100 points per curve

                for (int j = 1; j < points.Length; j++)
                {
                    session.DrawLine((float)points[j - 1].X, (float)points[j - 1].Y,
                                     (float)points[j].X, (float)points[j].Y, Colors.Blue);
                }
            }

            stopwatch.Stop();
            Debug.WriteLine("Win2D drawing time: " + stopwatch.ElapsedMilliseconds + " ms");
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
