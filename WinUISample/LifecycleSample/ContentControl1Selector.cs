using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifecycleSample
{
    public class ContentControl1Selector : DataTemplateSelector
    {
        public DataTemplate Model1Template { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Model1)
            {
                return Model1Template;
            }
            return null;
        }
    }
}
