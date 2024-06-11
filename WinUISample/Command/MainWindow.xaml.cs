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

namespace Command
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            UpdateButtonTextCommand = new(UpdateButtonText);
            UpdateButtonText2Command = new(UpdateButtonText2);
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        public void UpdateButtonText(string text)
        {
            string tmp = newBtnText.Text;
            myButton.Content = text;
        }

        public void UpdateButtonText2()
        {
            myButton.Content = "1111111";
        }

        public RelayCommand UpdateButtonTextCommand { get; private set; }
        public RelayCommand UpdateButtonText2Command { get; private set; }
    }
}
