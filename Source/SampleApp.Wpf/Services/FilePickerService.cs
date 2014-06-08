using System.Threading.Tasks;
using Microsoft.Win32;

namespace Samples.Wpf.Services
{
    public interface IFilePickerService
    {
        string PickFile();
    }

    public class FilePickerService : IFilePickerService
    {
        public string PickFile()
        {
            var openfileFileDialog = new OpenFileDialog();
            openfileFileDialog.ShowDialog();
            return openfileFileDialog.FileName;
        }
    }
}