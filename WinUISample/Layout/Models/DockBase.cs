using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using System.Collections.Generic;

namespace Layout.Models
{
    //[ContentProperty(Name = "VisibleDockables")]
    public class DockBase : DockableBase, IDock
    {
        public DependencyProperty ActiveDockableProperty = DependencyProperty.Register(
            nameof(ActiveDockable),
            typeof(IDockable),
            typeof(DockBase),
            new PropertyMetadata(null));

        public virtual IList<IDockable> VisibleDockables { get; set; }
        public IDockable ActiveDockable
        {
            get => (IDockable)GetValue(ActiveDockableProperty);
            set
            {
                SetValue(ActiveDockableProperty, value);
                Factory?.SetActiveDockable(value);
            }
        }
    }
}
