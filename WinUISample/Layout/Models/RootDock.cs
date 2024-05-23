using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using System.Collections.Generic;

namespace Layout.Models
{
    [ContentProperty(Name = "VisibleDockables")]
    public class RootDock : DockBase, IRootDock
    {
        public static DependencyProperty VisibleDockablesProperty = DependencyProperty.Register(
            nameof(VisibleDockables),
            typeof(IList<IDockable>),
            typeof(RootDock),
            new PropertyMetadata(new List<IDockable>()));

        public IList<IDockable> VisibleDockables
        {
            get => (IList<IDockable>)GetValue(VisibleDockablesProperty);
            set
            {
                SetValue(VisibleDockablesProperty, value);
            }
        }

        public override IList<IDockable> GetVisibleDockables()
        {
            return VisibleDockables;
        }
    }
}
