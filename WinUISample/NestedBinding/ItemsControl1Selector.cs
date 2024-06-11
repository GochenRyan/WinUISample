using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedBinding
{
    internal class ItemsControl1Selector : DataTemplateSelector
    {
        public DataTemplate Model1Template { get; set; }
        public DataTemplate Items1Template { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Model1)
            {
                return Model1Template;
            }
            else if (item is ItemsModel1)
            {
                return Items1Template;
            }
            return null;
        }
    }
}
