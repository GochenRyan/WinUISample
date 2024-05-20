using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using System.Collections.Generic;

namespace Layout.Models
{
    //[ContentProperty(Name = "VisibleDockables")]
    public class DockBase : DockableBase, IDock
    {
        public static DependencyProperty VisibleDockablesProperty = DependencyProperty.Register(
            nameof(VisibleDockables),
            typeof(IList<IDockable>),
            typeof(DockBase),
            new PropertyMetadata(new List<IDockable>(), new PropertyChangedCallback(OnPropertyChanged)));

        public static DependencyProperty ActiveDockableProperty = DependencyProperty.Register(
            nameof(ActiveDockable),
            typeof(IDockable),
            typeof(DockBase),
            new PropertyMetadata(null));

        public IList<IDockable> VisibleDockables
        {
            get => (IList<IDockable>)GetValue(VisibleDockablesProperty);
            set
            {
                SetValue(VisibleDockablesProperty, value);
            }
        }
        public IDockable ActiveDockable
        {
            get => (IDockable)GetValue(ActiveDockableProperty);
            set
            {
                SetValue(ActiveDockableProperty, value);
                Factory?.SetActiveDockable(value);
            }
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
