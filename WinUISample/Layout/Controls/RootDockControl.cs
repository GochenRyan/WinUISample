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

        //
        // Summary:
        //     Creates or identifies the element that is used to display the given item.
        //
        // Returns:
        //     The element that is used to display the given item.
        protected override DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }

        //
        // Summary:
        //     Determines whether the specified item is (or is eligible to be) its own container.
        //
        //
        // Parameters:
        //   item:
        //     The item to check.
        //
        // Returns:
        //     **true** if the item is (or is eligible to be) its own container; otherwise,
        //     **false**.
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return base.IsItemItsOwnContainerOverride(item);
        }

        //
        // Summary:
        //     Prepares the specified element to display the specified item.
        //
        // Parameters:
        //   element:
        //     The element that's used to display the specified item.
        //
        //   item:
        //     The item to display.
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }

        //
        // Summary:
        //     Invoked when the value of the Items property changes.
        //
        // Parameters:
        //   e:
        //     Event data. Not specifically typed in the current implementation.
        protected override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);
        }
    }
}
