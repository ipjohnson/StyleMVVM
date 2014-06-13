using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ReactiveUI;

using StyleMVVM.Services;
using StyleMVVM.Utilities;
using StyleMVVM.View;
using StyleMVVM.ViewModel;

namespace Samples.Wpf.ViewModels
{
    using Samples.Wpf.Utils;

    public class PersonViewModel : BaseViewModel
    {
        private readonly IFilePickerService filePickerService;
        private ImageSource picture;

        private string path;

        public PersonViewModel(IFilePickerService filePickerService)
        {
            this.filePickerService = filePickerService;
            this.ChangePictureCommand = new CancellableCommand(
                o =>
                {
                    var uri = new Uri(path, UriKind.RelativeOrAbsolute);
                    Picture = new BitmapImage(uri);
                });

            this.ChangePictureCommand.Executing += (sender, args) =>
                {
                    var fileTypeFilters = new List<FilePickerFilter> { new FilePickerFilter("*.jpg;*.png", "Picture files") };
                    path = this.filePickerService.PickFileAsync(PickerLocationId.Desktop, fileTypeFilters).Result;

                    args.Cancel = string.IsNullOrEmpty(this.path);
                };
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

        public CancellableCommand ChangePictureCommand { get; private set; }
    }
}