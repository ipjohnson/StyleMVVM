using System.Windows;
using System.Windows.Controls;

namespace StyleMVVM.DotNet.TestApp.Controls
{
    public class BiValueControl : ItemsControl
    {
        static BiValueControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BiValueControl), new FrameworkPropertyMetadata(typeof(BiValueControl)));
        }
    }
}
