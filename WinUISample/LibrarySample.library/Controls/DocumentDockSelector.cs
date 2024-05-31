using Layout.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Layout.Controls
{
    public class DocumentDockSelector : DataTemplateSelector
    {
        public DataTemplate DocumentDockTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is IDocumentDock)
            {
                return DocumentDockTemplate;
            }
            else
            {
                return null;
            }
        }
    }
}
