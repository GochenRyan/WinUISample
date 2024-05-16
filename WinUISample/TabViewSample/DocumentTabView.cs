using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TabViewSample
{
    public class DocumentTabView : ListView
    {
        public DocumentTabView()
        {
            this.DefaultStyleKey = typeof(DocumentTabView);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DocumentTabItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DocumentTabItem;
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }

        protected override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);
        }
    }
}
