using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindToAncestor
{
    public class CustomSelector : DataTemplateSelector
    {
        public DataTemplate Model1 { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Model1)
            {
                return Model1;
            }

            return null;
        }
    }
}
