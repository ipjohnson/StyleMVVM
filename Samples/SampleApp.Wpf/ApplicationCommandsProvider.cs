using System.Windows;
using System.Windows.Input;
using Samples.Wpf.Properties;
using StyleMVVM.ViewModel;

namespace Samples.Wpf
{
    [UsedImplicitly]
    public class ApplicationCommandsProvider : IApplicationCommandsProvider
    {
        public ApplicationCommandsProvider()
        {
            QuitCommand = new DelegateCommand(o => Application.Current.Shutdown());
        }
        public ICommand QuitCommand { get; set; }
    }
}