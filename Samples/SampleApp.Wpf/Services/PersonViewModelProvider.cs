using AutoMapper;
using Samples.Wpf.Model;
using Samples.Wpf.Properties;
using Samples.Wpf.ViewModels;
using StyleMVVM.View;

namespace Samples.Wpf.Services
{
    [UsedImplicitly]
    public class PersonViewModelService : IPersonViewModelService
    {
        private readonly IFilePickerService filePickerService;

        public PersonViewModelService(IFilePickerService filePickerService)
        {
            this.filePickerService = filePickerService;
        }

        public PersonViewModel Create(Person person)
        {
            return Mapper.Map<PersonViewModel>(person);
        }
    }
}