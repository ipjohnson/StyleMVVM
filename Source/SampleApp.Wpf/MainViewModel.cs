using System.Linq;
using AutoMapper;
using ReactiveUI;
using Samples.Wpf.Properties;
using Samples.Wpf.Services;
using Samples.Wpf.ViewModels;
using StyleMVVM.ViewModel;

namespace Samples.Wpf
{
    [ViewModel(Name = "Main"), UsedImplicitly]
    public class MainViewModel : BaseViewModel
    {
        private readonly IPersonViewModelService personViewModelService;
        private readonly IFilePickerService filePickerService;
        private PersonViewModel selectedPerson;        
        private decimal ageFilter;

        public MainViewModel(ISampleService sampleService, IPersonViewModelService personViewModelService)
        {
            this.personViewModelService = personViewModelService;

            this.AgeFilter = 30;

            this.People = GetPeopleFromService(sampleService);

            this.FilteredPeopleByAge = this.People.CreateDerivedCollection(
                model => model,
                model => model.Age > this.AgeFilter,
                (model, viewModel) => 0,
                this.WhenAny(model => model.AgeFilter, change => change.Value));
        }

        public IReactiveDerivedList<PersonViewModel> FilteredPeopleByAge { get; set; }

        public ReactiveList<PersonViewModel> People { get; set; }

        public PersonViewModel SelectedPerson
        {
            get
            {
                return this.selectedPerson;
            }

            set
            {
                this.selectedPerson = value;                
                this.OnPropertyChanged();
            }
        }

        public decimal AgeFilter
        {
            get
            {
                return this.ageFilter;
            }

            set
            {
                this.ageFilter = value;
                this.OnPropertyChanged();
            }
        }

        private ReactiveList<PersonViewModel> GetPeopleFromService(ISampleService service)
        {
            var people = service.GetPeople();
            return new ReactiveList<PersonViewModel>(people.Select(personViewModelService.Create))
                       {
                           ChangeTrackingEnabled = true
                       };
        }
    }
}