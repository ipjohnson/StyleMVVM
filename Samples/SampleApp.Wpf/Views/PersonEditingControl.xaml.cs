using System.Windows;
using Samples.Wpf.ViewModels;

namespace Samples.Wpf.Views
{
    public partial class PersonEditingControl
    {
        public PersonEditingControl()
        {
            InitializeComponent();
        }

        #region Person
        public static readonly DependencyProperty PersonProperty =
          DependencyProperty.Register("Person", typeof(PersonViewModel), typeof(PersonEditingControl),
            new FrameworkPropertyMetadata(null));

        public PersonViewModel Person
        {
            get { return (PersonViewModel)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }

        #endregion

        
    }
}
