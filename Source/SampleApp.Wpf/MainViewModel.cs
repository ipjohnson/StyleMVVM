namespace SampleApps.Wpf
{
    using System.Linq;

    using AutoMapper;

    using ReactiveUI;

    using SampleApps.Wpf.Properties;
    using SampleApps.Wpf.Services;
    using SampleApps.Wpf.ViewModels;

    using StyleMVVM.ViewModel;

    [ViewModel(Name = "Main"), UsedImplicitly]
    public class MainViewModel : BaseViewModel
    {
        private readonly ISampleService sampleService;
        private PersonViewModel selectedPerson;        
        private decimal ageFilter;

        public MainViewModel(ISampleService sampleService)
        {
            this.sampleService = sampleService;            

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

        private static ReactiveList<PersonViewModel> GetPeopleFromService(ISampleService service)
        {
            var people = service.GetPeople();
            return new ReactiveList<PersonViewModel>(people.Select(Mapper.Map<PersonViewModel>))
                       {
                           ChangeTrackingEnabled = true
                       };
        }
    }
}