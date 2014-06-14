using System.Windows.Input;

namespace Samples.Wpf
{
    public interface IApplicationCommandsProvider
    {
        ICommand QuitCommand { get; set; }
    }
}