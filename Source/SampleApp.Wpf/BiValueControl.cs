namespace SampleApps.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
  
    public class BiValueControl : ItemsControl
    {
        static BiValueControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BiValueControl), new FrameworkPropertyMetadata(typeof(BiValueControl)));
        }
    }
}
