using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedBinding
{
    [ContentProperty(Name= "ItemModes")]
    public class ItemsModel1 : DependencyObject, IItem, IEquatable<ItemsModel1>
    {
        public static readonly DependencyProperty ItemModesProperty = DependencyProperty.Register(
            nameof(ItemModes),
            typeof(IList<IItem>),
            typeof(ItemsModel1),
            new PropertyMetadata(new List<IItem>()));

        public IList<IItem> ItemModes
        {
            get
            {
                return (IList<IItem>)GetValue(ItemModesProperty);
            }
            set
            {
                SetValue(ItemModesProperty, value);
            }
        }

        public bool Equals(ItemsModel1 other)
        {
            if (other is ItemsModel1 itemsModel)
            {
                return this == itemsModel;
            }

            return false;
        }
    }
}
