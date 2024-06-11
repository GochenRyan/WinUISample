using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LifecycleSample
{
    public sealed class CustomPanel1 : Panel
    {
        public CustomPanel1() : base()
        {
            //this.DefaultStyleKey = typeof(CustomPanel1);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (double.IsInfinity(availableSize.Width) ||
                double.IsInfinity(availableSize.Height))
            {
                throw new Exception("CustomPanel1 cannot be inside a control that offers infinite space.");
            }

            if (Children == null || Children.Count == 0)
            {
                base.MeasureOverride(availableSize);
                return availableSize;
            }

            var cnt = Children.Count;
            var itemSize = new Size(availableSize.Width / cnt, availableSize.Height);

            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                child.Measure(itemSize);
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children == null || Children.Count == 0)
            {
                return base.ArrangeOverride(finalSize);
            }

            var left = 0.0;
            var top = 0.0;

            var childHeight = finalSize.Height;
            var childWidth = finalSize.Width / Children.Count;

            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var rect = new Rect(left, top, childWidth, childHeight);
                child.Arrange(rect);
                left += childWidth;
            }

            return finalSize;
        }
    }
}
