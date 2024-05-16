using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace VisualState
{
    [TemplateVisualState(Name = LeftState, GroupName = AreaStates)]
    [TemplateVisualState(Name = MiddleState, GroupName = AreaStates)]
    [TemplateVisualState(Name = RightState, GroupName = AreaStates)]
    [TemplatePart(Name = StateButtonPartName, Type = typeof(FrameworkElement))]
    public partial class AreaButton : ContentControl
    {
        public const string StateButtonPartName = "StateButton";

        public const string AreaStates = "AreaStates";
        public const string LeftState = "Left";
        public const string MiddleState = "Middle";
        public const string RightState = "Right";

        public AreaButton()
        {
            this.DefaultStyleKey = typeof(AreaButton);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _StateButtonPart = GetTemplateChild(StateButtonPartName) as FrameworkElement;
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
            var pos = e.GetCurrentPoint(_StateButtonPart).Position;
            if (pos.X < _StateButtonPart.Width * 0.25)
            {
                VisualStateManager.GoToState(this, LeftState, true);
            }
            else if (pos.X > _StateButtonPart.Width * 0.75)
            {
                VisualStateManager.GoToState(this, RightState, true);
            }
            else
            {
                VisualStateManager.GoToState(this, MiddleState, true);
            }
        }

        private FrameworkElement _StateButtonPart;
    }
}
