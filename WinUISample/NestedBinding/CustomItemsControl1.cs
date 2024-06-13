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

namespace NestedBinding
{
    public sealed class CustomItemsControl1 : ItemsControl
    {
        public CustomItemsControl1()
        {
            this.DefaultStyleKey = typeof(CustomItemsControl1);
            DataContextChanged += CustomItemsControl1_DataContextChanged;
        }

        private void CustomItemsControl1_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
           if (DataContext is ItemsModel1 itemsModel1)
            {
                ItemsSource = itemsModel1.ItemModes;
            }
        }

        protected override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
