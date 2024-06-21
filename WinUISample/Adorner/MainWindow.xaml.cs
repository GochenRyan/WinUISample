using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Adorner
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

        // Handles the Click event on the Button inside the Popup control and 
        // closes the Popup. 
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            
        }

        // Handles the Click event on the Button on the page and opens the Popup. 
        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            var popup = new Popup();
            var tb = new TextBox 
            {
                Text = "Adorner",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            var grid = new Grid()
            {
                Height = PlacementGrid.Height,
                Width = PlacementGrid.Width,
                Background = new SolidColorBrush(Colors.Brown)
        };
            grid.Children.Add(tb);
            
            popup.XamlRoot = this.Content.XamlRoot;
            popup.Child = grid;

            var t = PlacementGrid.TransformToVisual(this.Content);
            var windowPoint = t.TransformPoint(new Point());

            popup.HorizontalOffset = windowPoint.X;
            popup.VerticalOffset = windowPoint.Y;
            popup.IsOpen = true;

            m_popup = popup;
        }

        Popup m_popup;
    }
}
