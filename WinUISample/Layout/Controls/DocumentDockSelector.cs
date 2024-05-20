using Layout.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Layout.Controls
{
    public class DocumentDockSelector : DataTemplateSelector
    {
        public DataTemplate DocumentTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is IDocument)
            {
                return DocumentTemplate;
            }
            else
            {
                return null;
            }
        }
    }
}
