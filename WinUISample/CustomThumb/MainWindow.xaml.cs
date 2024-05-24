using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomThumb
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

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _isMoving = true;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double delta = e.HorizontalChange;

            if (delta > 0)
            {
                if (grid2.Width - delta <= 10)
                    return;

                grid1.Width += delta;
                grid2.Width -= delta;
            }
            else if (delta < 0)
            {
                if (grid1.Width + delta <= 10)
                    return;

                grid2.Width -= delta;
                grid1.Width += delta;
            }
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            _isMoving = false;
        }

        private bool _isMoving = false;
    }
}
