using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SearchTree
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            CreateFakeData();

            FilterTV.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(IList<NodeItem> list)
        {
            Debug.WriteLine("======================");
            foreach (var item in list)
            {
                Debug.WriteLine(item.Name);
                Debug.WriteLine(item.Path);
            }
            Debug.WriteLine("======================");
        }

        private async void CreateFakeData()
        {
            var root1 = new RootNodeItem { Name = "Events", IsExpanded = false };
            var root2 = new RootNodeItem { Name = "Button_40", IsExpanded = true };
            var item1 = new NodeItem { Name = "Transform", IsExpanded = true };
            var item1_1 = new NodeItem { Name = "Translation", IsExpanded = true };
            var item1_1_1 = new NodeItem { Name = "X", IsExpanded = true };
            var item1_1_2 = new NodeItem { Name = "Y", IsExpanded = true };
            var item1_1_3 = new NodeItem { Name = "Z", IsExpanded = true };
            var item1_2 = new NodeItem { Name = "Scale", IsExpanded = true };
            var item1_2_1 = new NodeItem { Name = "X", IsExpanded = true };
            var item1_2_2 = new NodeItem { Name = "Y", IsExpanded = true };
            var item1_2_3 = new NodeItem { Name = "Z", IsExpanded = true };

            root2.Children.Add(item1);
            item1.Children.Add(item1_1);
            item1.Children.Add(item1_2);
            item1_1.Children.Add(item1_1_1);
            item1_1.Children.Add(item1_1_2);
            item1_1.Children.Add(item1_1_3);
            item1_2.Children.Add(item1_2_1);
            item1_2.Children.Add(item1_2_2);
            item1_2.Children.Add(item1_2_3);

            TestSource.Add(root1);
            TestSource.Add(root2);

            await Task.Delay(5000);
            item1_2.Children.Remove(item1_2_1);
            await Task.Delay(5000);
            item1_2.Children.Add(item1_2_1);
        }

        ObservableCollection<RootNodeItem> TestSource = new();
    }
}
