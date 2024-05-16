using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using System.Collections.Generic;

namespace Layout.Models
{
    [ContentProperty(Name = "VisibleDockables")]
    public class DockBase : DockableBase, IDock
    {
        public static DependencyProperty VisibleDockablesProperty = DependencyProperty.Register(
            nameof(VisibleDockables),
            typeof(IList<IDockable>),
            typeof(DockBase),
            new PropertyMetadata(new List<IDockable>()));

        public IList<IDockable> VisibleDockables 
        { 
            get => (IList<IDockable>)GetValue(VisibleDockablesProperty); 
            set => SetValue(VisibleDockablesProperty, value); 
        }
    }
}
