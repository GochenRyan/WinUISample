using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TemplatePart
{
    [TemplatePart(Name = HeaderPartName, Type = typeof(FrameworkElement))]
    public partial class HeaderedContentControl : ContentControl
    {
        public const string HeaderPartName = "HeaderContentPresenter";

        public HeaderedContentControl()
        {
            this.DefaultStyleKey = typeof(HeaderedContentControl);
        }

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(HeaderedContentControl), new PropertyMetadata(null, OnHeaderChanged));

        private static void OnHeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HeaderedContentControl target = obj as HeaderedContentControl;
            object oldValue = (object)args.OldValue;
            object newValue = (object)args.NewValue;
            if (oldValue != newValue)
                target.OnHeaderChanged(oldValue, newValue);
        }

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(HeaderedContentControl), new PropertyMetadata(null, OnHeaderTemplateChanged));

        private static void OnHeaderTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HeaderedContentControl target = obj as HeaderedContentControl;
            DataTemplate oldValue = (DataTemplate)args.OldValue;
            DataTemplate newValue = (DataTemplate)args.NewValue;
            if (oldValue != newValue)
                target.OnHeaderTemplateChanged(oldValue, newValue);
        }

        protected virtual void OnHeaderChanged(object oldValue, object newValue)
        {
        }

        protected virtual void OnHeaderTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _headerPart = GetTemplateChild(HeaderPartName) as FrameworkElement;
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            _isPointerEntered = true;
            UpdateHeaderVisual();
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            _isPointerEntered = false;
            UpdateHeaderVisual();
        }

        private void UpdateHeaderVisual()
        {
            if (_headerPart == null)
                return;

            if (_isPointerEntered)
                _headerPart.Opacity = 1;
            else
                _headerPart.Opacity = 0.5;

            if (Header == null)
                _headerPart.Visibility = Visibility.Collapsed;
            else
                _headerPart.Visibility = Visibility.Visible;
        }


        private FrameworkElement _headerPart;
        private bool _isPointerEntered;
    }
}
