using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace Layout.Models
{
    public class RootDock : DockBase, IRootDock
    {
        public DependencyProperty VisibleDockablesProperty = DependencyProperty.Register(
            nameof(VisibleDockables),
            typeof(IList<IDockable>),
            typeof(RootDock),
            new PropertyMetadata(new List<IDockable>()));

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
