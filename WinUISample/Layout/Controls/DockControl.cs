using Layout.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Layout.Controls
{
    //[ContentProperty(Name = "Layout")]
    public partial class DockControl : ContentControl
    {
        public DockControl()
        {
            this.DefaultStyleKey = typeof(DockControl);
        }

        //public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register(
        //    nameof(Layout),
        //    typeof(IDock),
        //    typeof(DockControl),
        //    new PropertyMetadata(default));

        //public IDock? Layout
        //{
        //    get => (IDock)GetValue(LayoutProperty);
        //    set => SetValue(LayoutProperty, value);
        //}

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
    }
}
