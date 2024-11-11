using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SearchTree
{
    public sealed partial class FilterTreeView : UserControl
    {
        public FilterTreeView()
        {
            this.InitializeComponent();
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text))
            {
                PropertiesTreeView.Visibility = Visibility.Visible;
                SearchResultTreeView.Visibility = Visibility.Collapsed;
                m_isFiltering = false;
            }
            else
            {
                m_currentKeyword = sender.Text;
                m_isFiltering = true;
                PropertiesTreeView.Visibility = Visibility.Collapsed;
                SearchResultTreeView.Visibility = Visibility.Visible;

                await ShowFilterView();
            }
        }

        /// <summary>
        /// Expand items one by one, allowing enough time for containers to be generated.
        /// </summary>
        /// <returns></returns>
        private async Task ExpandAll(ObservableCollection<RootNodeItem> source)
        {
            Queue<NodeItem> queue = new();
            foreach (var item in source)
            {
                queue.Enqueue(item);
            }

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                item.IsExpanded = true;

                // TODO: WinUI3's TreeView expansion bug, pending fix. See: https://github.com/microsoft/microsoft-ui-xaml/issues/10140#issuecomment-2463041046
                await Task.Delay(10);

                foreach (var child in item.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void FilterTree(string keyword)
        {
            for (int i = 0; i < SearchableDataSource.Count; i++)
            {
                FilterTreeCore(SearchableDataSource[i], keyword);
            }

            for (int i = SearchableDataSource.Count - 1; i >= 0; --i)
            {
                var item = SearchableDataSource[i];
                if (item.Children.Count == 0 && !item.Name.Contains(keyword))
                {
                    SearchableDataSource.Remove(item);
                }
            }
        }

        private NodeItem FilterTreeCore(NodeItem node, string keyword)
        {
            for (int i = node.Children.Count - 1; i >= 0; i--)
            {
                var child = node.Children[i];
                var filteredChild = FilterTreeCore(child, keyword);
                if (filteredChild == null)
                {
                    var item = node.Children[i];
                    node.Children.RemoveAt(i);
                    RecycleItem(item);
                }
            }

            if (node.Name.Contains(keyword))
            {
                return node;
            }

            return node.Children.Count > 0 ? node : null;
        }

        private void HighlightSearchTerm(string keyword)
        {
            Queue<NodeItem> queue = new();
            foreach (var item in SearchableDataSource)
            {
                queue.Enqueue(item);
            }

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                var dependencyObject = SearchResultTreeView.ContainerFromItem(item);

                if (dependencyObject is TreeViewItem treeViewItem && treeViewItem.ContentTemplateRoot is RichTextBlock richTextBlock)
                {
                    richTextBlock.TextHighlighters.Clear();

                    string str = item.Name;
                    int startIndex = 0;
                    while ((startIndex = str.IndexOf(keyword, startIndex)) != -1)
                    {
                        int endIndex = startIndex + keyword.Length - 1;
                        TextRange textRange = new TextRange()
                        {
                            StartIndex = startIndex,
                            Length = keyword.Length
                        };
                        TextHighlighter highlighter = new TextHighlighter()
                        {
                            Background = new SolidColorBrush(Colors.GreenYellow),
                            Ranges = { textRange }
                        };

                        richTextBlock.TextHighlighters.Add(highlighter);
                        startIndex += keyword.Length;
                    }
                }

                if (item.Children.Count > 0)
                {
                    foreach (var child in item.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        private void RecoverTreeViewItems()
        {
            Queue<NodeItem> queue = new();
            foreach (var item in SearchableDataSource)
            {
                queue.Enqueue(item);
            }

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                var dependencyObject = SearchResultTreeView.ContainerFromItem(item);

                if (dependencyObject is TreeViewItem treeViewItem && treeViewItem.ContentTemplateRoot is RichTextBlock richTextBlock)
                {
                    richTextBlock.TextHighlighters.Clear();
                }

                if (item.Children.Count > 0)
                {
                    foreach (var child in item.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        private void RecoverDataSource()
        {
            for (int i = 0; i < SearchableDataSource.Count; i++)
            {
                RecycleItem(SearchableDataSource[i]);
            }
            SearchableDataSource.Clear();

            foreach (var rootNode in OriginDataSource)
            {
                var rootNodeClone = GetUsableItem<RootNodeItem>();
                rootNodeClone.Name = rootNode.Name;
                rootNodeClone.IsExpanded = false;

                var queue = new Queue<(NodeItem, NodeItem)>();
                queue.Enqueue((rootNode, rootNodeClone));

                while (queue.Count > 0)
                {
                    var (original, clone) = queue.Dequeue();
                    foreach (var child in original.Children)
                    {
                        var childClone = GetUsableItem<NodeItem>();
                        childClone.Name = child.Name;
                        childClone.IsExpanded = false;

                        clone.Children.Add(childClone);
                        queue.Enqueue((child, childClone));
                    }
                }

                SearchableDataSource.Add(rootNodeClone);
            }
        }

        private void RecycleItem(NodeItem nodeItem)
        {
            Queue<NodeItem> nodeItems = new();

            if (nodeItem is RootNodeItem root)
            {
                RootNodeItemPool.Enqueue(root);
                foreach (NodeItem child in root.Children)
                {
                    nodeItems.Enqueue(child);
                }
                root.Clear();
            }
            else
            {
                nodeItems.Enqueue(nodeItem);
            }

            while (nodeItems.Count > 0)
            {
                NodeItem item = nodeItems.Dequeue();
                if (item.Children.Count > 0)
                {
                    foreach (NodeItem child in item.Children)
                    {
                        nodeItems.Enqueue(child);
                    }
                }

                item.Clear();
                NodeItemPool.Enqueue(item);
            }
        }

        private T GetUsableItem<T>() where T : NodeItem, new()
        {
            return typeof(T) == typeof(RootNodeItem)
                ? (T)(object)GetRootNodeItemFromPool()
                : (T)(object)GetNodeItemFromPool();
        }

        private RootNodeItem GetRootNodeItemFromPool()
        {
            return RootNodeItemPool.Count > 0 ? RootNodeItemPool.Dequeue() : new RootNodeItem();
        }

        private NodeItem GetNodeItemFromPool()
        {
            return NodeItemPool.Count > 0 ? NodeItemPool.Dequeue() : new NodeItem();
        }

        private void TreeView_SelectionChanged(TreeView sender, TreeViewSelectionChangedEventArgs args)
        {
            var selectedItems = args.AddedItems.OfType<NodeItem>().ToList();
            SelectionChanged?.Invoke(selectedItems);
        }

        public static readonly DependencyProperty OriginDataSourceProperty = DependencyProperty.Register(
            nameof(OriginDataSource),
            typeof(ObservableCollection<RootNodeItem>),
            typeof(FilterTreeView),
            new PropertyMetadata(new ObservableCollection<RootNodeItem>()));

        public ObservableCollection<RootNodeItem> OriginDataSource
        {
            get => (ObservableCollection<RootNodeItem>)GetValue(OriginDataSourceProperty);
            set 
            {
                foreach(var root in m_originDataSource)
                {
                    root.DescendantChanged -= Root_DescendantChanged;
                }
                m_originDataSource.CollectionChanged -= CollectionChanged;
                SetValue(OriginDataSourceProperty, value); 
                m_originDataSource = value;
                m_originDataSource.CollectionChanged += CollectionChanged;

                foreach (var root in m_originDataSource)
                {
                    root.DescendantChanged += Root_DescendantChanged;
                }
            } 
        }

        private async void Root_DescendantChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (m_isFiltering)
            {
                await ShowFilterView();
            }
        }

        private ObservableCollection<RootNodeItem> m_originDataSource = new();
        private async void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var root in e.NewItems)
                {
                    if (root is RootNodeItem rootNodeItem)
                    {
                        rootNodeItem.DescendantChanged += Root_DescendantChanged;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                foreach (var root in e.OldItems)
                {
                    if (root is RootNodeItem rootNodeItem)
                    {
                        rootNodeItem.DescendantChanged -= Root_DescendantChanged;
                    }
                }
            }

            if (m_isFiltering)
            {
                await ShowFilterView();
            }
        }

        private async Task ShowFilterView()
        {
            RecoverTreeViewItems();
            RecoverDataSource();
            FilterTree(m_currentKeyword);
            await ExpandAll(SearchableDataSource);
            HighlightSearchTerm(m_currentKeyword);
        }

        private string m_currentKeyword = "";

        public Action<IList<NodeItem>> SelectionChanged;

        private ObservableCollection<RootNodeItem> SearchableDataSource = new();

        private Queue<NodeItem> NodeItemPool = new();
        private Queue<RootNodeItem> RootNodeItemPool = new();

        private bool m_isFiltering = false;
    }


    public class NodeItem
    {
        public NodeItem()
        {
            m_children.CollectionChanged += M_children_CollectionChanged;
        }

        public string Name { get; set; }
        private ObservableCollection<NodeItem> m_children = new();
        public ObservableCollection<NodeItem> Children
        {
            get
            {
                if (m_children == null)
                {
                    m_children = new ObservableCollection<NodeItem>();
                }
                return m_children;
            }
            set
            {
                m_children.CollectionChanged -= M_children_CollectionChanged;
                m_children = value;
                m_children.CollectionChanged += M_children_CollectionChanged;
            }
        }

        public NodeItem Parent { get; private set; }
        public NodeItem Ancestor { get; private set; }

        public string Path
        {
            get
            {
                string path = Name;
                var ancestor = Parent;
                while (ancestor != null)
                {
                    path = ancestor.Name + "." + path;
                    ancestor = ancestor.Parent;
                }
                return path;
            }
        }

        private void SetAncestor(NodeItem parent)
        {
            var tmp = parent;
            while (tmp.Parent != null)
            {
                tmp = tmp.Parent;
            }
            Ancestor = tmp;
        }

        private void M_children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (NodeItem item in e.NewItems)
                    {
                        if (item != null)
                        {
                            item.Parent = this;
                            item.SetAncestor(this);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems == null)
                        return;
                    foreach (NodeItem item in e.OldItems)
                    {
                        if (item != null)
                        {
                            item.Parent = null;
                            item.Ancestor = null;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems == null)
                        return;
                    foreach (NodeItem oldItem in e.OldItems)
                    {
                        if (oldItem != null)
                        {
                            oldItem.Parent = null;
                            oldItem.Ancestor = null;
                        }
                    }
                    foreach (NodeItem newItem in e.NewItems)
                    {
                        if (newItem != null)
                        {
                            newItem.Parent = this;
                            newItem.SetAncestor(this);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    if (e.OldItems == null)
                        return;
                    foreach (NodeItem item in e.OldItems)
                    {
                        if (item != null)
                        {
                            item.Parent = null;
                            item.Ancestor = null;
                        }
                    }
                    break;
            }

            if (Ancestor is RootNodeItem root)
            {
                root.NotifyDescendantChanged(e);
            }
        }

        public bool IsExpanded;

        public void Clear()
        {
            IsExpanded = false;
            m_children.Clear();
            Parent = null;
            Ancestor = null;
            Name = "";
        }
    }

    public class RootNodeItem : NodeItem
    {
        public event NotifyCollectionChangedEventHandler DescendantChanged;
        public void NotifyDescendantChanged(NotifyCollectionChangedEventArgs e)
        {
            DescendantChanged?.Invoke(this, e);
        }
    }
}
