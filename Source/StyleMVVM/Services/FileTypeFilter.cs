namespace StyleMVVM.Services
{
    public class FilePickerFilter
    {
        public FilePickerFilter(string filter, string description)
        {
            Filter = filter;
            Description = description;
        }

        public string Filter { get; set; }
        public string Description { get; set; }
    }
}