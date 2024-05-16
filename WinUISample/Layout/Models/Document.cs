using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

namespace Layout.Models
{
    [ContentProperty(Name = "Content")]
    public class Document : DockableBase
    {
        DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(Document),
            new PropertyMetadata(default));

        public object Content 
        { 
            get => GetValue(ContentProperty); 
            set => SetValue(ContentProperty, value); 
        }
    }
}
