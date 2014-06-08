using System.Threading.Tasks;
using Microsoft.Win32;

namespace Samples.Wpf.Services
{
    public interface IFilePickerService
    {
        Task<string> PickFile();
    }

    public class FilePickerService : IFilePickerService
    {
        public Task<string> PickFile()
        {
            var openfileFileDialog = new OpenFileDialog();
            openfileFileDialog.ShowDialog();
            return new Task<string>( () => string.Empty);
        }
    }
}