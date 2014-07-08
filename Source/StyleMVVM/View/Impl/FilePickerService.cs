using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.Services;
#if NETFX_CORE
using Windows.Storage;
using Windows.Storage.Pickers;
#else
using Microsoft.Win32;
using StyleMVVM.Utilities;
#endif

namespace StyleMVVM.View.Impl
{
	/// <summary>
	/// Default File Picker Service.
	/// </summary>
	public class FilePickerService : IFilePickerService
	{
#if NETFX_CORE	   
        /// <inheritdoc />
		public async Task<IReadOnlyList<StorageFile>> PickMultipleFilesAsync(
            PickerLocationId location = PickerLocationId.DocumentsLibrary, params FilePickerFilter[] filePickerFilters)
		{
			var openPicker = new FileOpenPicker { SuggestedStartLocation = location };

		    AddFilters(openPicker, filePickerFilters);

			return await openPicker.PickMultipleFilesAsync();
		}

        /// <inheritdoc />
	    public async Task<StorageFile> PickFileAsync(PickerLocationId location = PickerLocationId.DocumentsLibrary, params FilePickerFilter[] filePickerFilters)
		{
			var openPicker = new FileOpenPicker
			                            {
				                            SuggestedStartLocation = location
			                            };

            AddFilters(openPicker, filePickerFilters);

			return await openPicker.PickSingleFileAsync();
		}

	    private static void AddFilters(FileOpenPicker openPicker, IList<FilePickerFilter> filePickerFilters)
	    {
	        if (filePickerFilters != null)
	        {
	            foreach (var filterType in filePickerFilters)
	            {
	                openPicker.FileTypeFilter.Add(filterType.Filter);
	            }
	        }
	    }

#else
		private readonly IPickerLocationIdTranslator translator;

		/// <summary>
		/// Default file picker
		/// </summary>
		/// <param name="translator"></param>
		public FilePickerService(IPickerLocationIdTranslator translator)
		{
			this.translator = translator;
		}

        /// <inheritdoc />
	    public async Task<IReadOnlyList<string>> PickMultipleFilesAsync(PickerLocationId location, params FilePickerFilter[] filePickerFilters)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			                                {
			                                    InitialDirectory = TranslateLocation(location),
			                                    Multiselect = true,
			                                    Filter = CreateFilter(filePickerFilters)
			                                };

		    openFileDialog.ShowDialog();

			if (openFileDialog.FileNames != null)
			{
				return new List<string>(openFileDialog.FileNames);
			}

			return new List<string>();
		}

        /// <inheritdoc />
		public async Task<string> PickFileAsync(PickerLocationId location, params FilePickerFilter[] fileTypeFilters)
		{
			var openFileDialog = new OpenFileDialog
			                     {
			                         InitialDirectory = TranslateLocation(location),
			                         Filter = CreateFilter(fileTypeFilters),
			                         Multiselect = false
			                     };

		    openFileDialog.ShowDialog();

			return openFileDialog.FileName;
		}

	    private static string CreateFilter(IList<FilePickerFilter> filters)
	    {
	        var stringBuilder = new StringBuilder();

	        foreach (var fileTypeFilter in filters)
	        {
                stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}|{1}", fileTypeFilter.Description, fileTypeFilter.Filter);
	        }

	        return stringBuilder.ToString();
	    }

        /// <inheritdoc />
	    public async Task<string> PickSaveFileAsync(PickerLocationId location, params FilePickerFilter[] fileTypeFilters)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog
			                                {
			                                    InitialDirectory = TranslateLocation(location),
			                                    Filter = CreateFilter(fileTypeFilters)
			                                };

	        saveFileDialog.ShowDialog();

			return saveFileDialog.FileName;
		}

		private string TranslateLocation(PickerLocationId location)
		{
			return translator.Translate(location);
		}
#endif

    }
}
