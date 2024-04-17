using DockToy.models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

namespace DockToy.controls
{
    [ContentProperty(Name = "Content")]
    public class DocumentDock : DependencyObject, IDocument
    {
        public object Content {  get; set; }
    }
}
