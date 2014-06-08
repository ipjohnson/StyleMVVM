using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using ReactiveUI;
using Samples.Wpf.Annotations;
using Samples.Wpf.Model;
using Samples.Wpf.ViewModels;

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
            return new PersonViewModel(filePickerService)
            {
                Name = person.Name,
                Age = person.Age,
                Picture = person.Picture,
                OwnedHouses = new ReactiveList<HouseViewModel>(Mapper.Map<IEnumerable<HouseViewModel>>(person.OwnedHouses)),
            };
        }
    }
}