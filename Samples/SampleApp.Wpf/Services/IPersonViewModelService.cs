using Samples.Wpf.Model;
using Samples.Wpf.ViewModels;

namespace Samples.Wpf.Services
{
    public interface IPersonViewModelService
    {
        PersonViewModel Create(Person person);
    }
}