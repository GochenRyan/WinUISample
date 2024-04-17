using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Collections.Generic;
using Windows.Foundation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DockToy
{
    public sealed partial class DockControl : ContentControl
    {
        public DockControl()
        {
            this.InitializeComponent();
        }

        protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate)
        {
            base.OnContentTemplateChanged(oldContentTemplate, newContentTemplate);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }

        protected override void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector, DataTemplateSelector newContentTemplateSelector)
        {
            base.OnContentTemplateSelectorChanged(oldContentTemplateSelector, newContentTemplateSelector);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override bool GoToElementStateCore(string stateName, bool useTransitions)
        {
            return base.GoToElementStateCore(stateName, useTransitions);
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return base.OnCreateAutomationPeer();
        }

        protected override void OnDisconnectVisualChildren()
        {
            base.OnDisconnectVisualChildren();
        }

        protected override IEnumerable<IEnumerable<Point>> FindSubElementsForTouchTargeting(Point point, Rect boundingRect)
        {
            return base.FindSubElementsForTouchTargeting(point, boundingRect);
        }

        protected override IEnumerable<DependencyObject> GetChildrenInTabFocusOrder()
        {
            return base.GetChildrenInTabFocusOrder();
        }

        protected override void OnKeyboardAcceleratorInvoked(KeyboardAcceleratorInvokedEventArgs args)
        {
            base.OnKeyboardAcceleratorInvoked(args);
        }

        protected override void OnProcessKeyboardAccelerators(ProcessKeyboardAcceleratorEventArgs args)
        {
            base.OnProcessKeyboardAccelerators(args);
        }

        protected override void OnBringIntoViewRequested(BringIntoViewRequestedEventArgs e)
        {
            base.OnBringIntoViewRequested(e);
        }

        protected override void PopulatePropertyInfoOverride(string propertyName, Microsoft.UI.Composition.AnimationPropertyInfo animationPropertyInfo)
        {
            base.PopulatePropertyInfoOverride(propertyName, animationPropertyInfo);
        }
    }
}
