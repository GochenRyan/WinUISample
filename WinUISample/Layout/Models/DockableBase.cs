using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

namespace Layout.Models
{
    public abstract class DockableBase : DependencyObject, IDockable
    {
        DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(DockableBase),
            new PropertyMetadata(string.Empty));

        public string Title 
        { 
            get => (string)GetValue(TitleProperty); 
            set => SetValue(TitleProperty, value); 
        }
    }
}
