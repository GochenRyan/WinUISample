using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace Layout.Models
{
    public class DocumentDock : DockBase, IDocumentDock
    {
        public DocumentDock() : base()
        {
            VisibleDockables = new List<IDockable>();
        }

        public static readonly DependencyProperty VisibleDockablesProperty = DependencyProperty.Register(
            nameof(VisibleDockables),
            typeof(IList<IDockable>),
            typeof(DocumentDock),
            new PropertyMetadata(null));

        public override IList<IDockable> VisibleDockables
        {
            get => (IList<IDockable>)GetValue(VisibleDockablesProperty);
            set
            {
                SetValue(VisibleDockablesProperty, value);
            }
        }
    }
}
