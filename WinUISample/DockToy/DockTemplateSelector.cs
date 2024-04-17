using DockToy.models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockToy
{
    public class DockTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; } = new();
        public DataTemplate RootDockTemplate { get; set; }
        public DataTemplate DocumnentDockTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is IRoot)
            {
                return RootDockTemplate;
            }
            else if (item is IDocument) 
            {
                return DocumnentDockTemplate;
            }
            else
            {
                return null;
            }
        }
    }
}
