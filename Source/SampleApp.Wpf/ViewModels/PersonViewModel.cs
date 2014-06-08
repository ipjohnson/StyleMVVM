using System.Windows.Input;
using System.Windows.Media;
using ReactiveUI;
using Samples.Wpf.Services;
using StyleMVVM.ViewModel;

namespace Samples.Wpf.ViewModels
{
    public class PersonViewModel
    {
        private readonly IFilePickerService filePickerService;

        public PersonViewModel(IFilePickerService filePickerService)
        {
            this.filePickerService = filePickerService;
            ChangePictureCommnad = new DelegateCommand(o => filePickerService.PickFile());
        }

        public string Name { get; set; }
        public ReactiveList<HouseViewModel> OwnedHouses { get; set; }
        public decimal Age { get; set; }

        public ImageSource Picture { get; set; }

        public ICommand ChangePictureCommnad { get; private set; }
    }
}