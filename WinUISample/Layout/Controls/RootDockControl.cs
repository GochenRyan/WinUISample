using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Layout.Controls
{
    public sealed class RootDockControl : ItemsControl
    {
        public RootDockControl()
        {
            this.DefaultStyleKey = typeof(RootDockControl);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return base.IsItemItsOwnContainerOverride(item);
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
