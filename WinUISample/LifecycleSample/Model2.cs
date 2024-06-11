using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifecycleSample
{
    public class Model2 : DependencyObject
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(Model2),
            new PropertyMetadata(""));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set 
            { 
                SetValue(TextProperty, value); 
            }
        }
    }
}
