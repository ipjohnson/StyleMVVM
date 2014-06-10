using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ReactiveUI;
using Samples.Wpf.Services;
using StyleMVVM.ViewModel;

namespace Samples.Wpf.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        private readonly IFilePickerService filePickerService;
        private ImageSource picture;

        public PersonViewModel(IFilePickerService filePickerService)
        {
            this.filePickerService = filePickerService;
            ChangePictureCommnad = new DelegateCommand(o => ChangePicture());
        }

        private void ChangePicture()
        {

            var path = filePickerService.PickFile();
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);
            Picture = new BitmapImage(uri);
        }

        public string Name { get; set; }
        public ReactiveList<HouseViewModel> OwnedHouses { get; set; }
        public decimal Age { get; set; }

        public ImageSource Picture
        {
            get { return picture; }
            set
            {
                picture = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand ChangePictureCommnad { get; private set; }
    }
}