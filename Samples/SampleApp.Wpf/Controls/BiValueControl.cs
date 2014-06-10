using System.Windows;
using System.Windows.Controls;

namespace Samples.Wpf.Controls
{
    public class BiValueControl : ItemsControl
    {
        static BiValueControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BiValueControl), new FrameworkPropertyMetadata(typeof(BiValueControl)));
        }
    }
}
