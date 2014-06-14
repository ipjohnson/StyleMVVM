using System;
using System.Windows;
using System.Windows.Controls;
using Samples.Wpf;

namespace Designer.Main
{
    public class SeparatorStyleSelector : StyleSelector
    {
        public Style RegularItemStyle { get; set; }
        public Style SeparatorItemStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ViewModelMenu)
            {
                var mi = item as ViewModelMenu;
                return mi.Text.Equals("--", StringComparison.OrdinalIgnoreCase) ? SeparatorItemStyle : RegularItemStyle;
            }
            return null;
        }
    }
}