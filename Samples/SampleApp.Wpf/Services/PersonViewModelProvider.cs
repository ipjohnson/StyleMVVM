using AutoMapper;
using Samples.Wpf.Model;
using Samples.Wpf.Properties;
using Samples.Wpf.ViewModels;

namespace Samples.Wpf.Services
{
    [UsedImplicitly]
    public class PersonViewModelService : IPersonViewModelService
    {
        public PersonViewModel Create(Person person)
        {
            return Mapper.Map<PersonViewModel>(person);
        }
    }
}