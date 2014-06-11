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
	public class FilePickerService : IFilePickerService
	{
#if NETFX_CORE
		/// <summary>
		/// Opens a file picker and allows the user to pick multiple files and return a list of the files the user chose
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		public async Task<IReadOnlyList<StorageFile>> PickMultipleFilesAsync(
			PickerLocationId location = PickerLocationId.DocumentsLibrary, IList<FilePickerFilter> filePickerFilters)
		{
			FileOpenPicker openPicker = new FileOpenPicker();

			openPicker.SuggestedStartLocation = location;

			if (filterTypes != null)
			{
				foreach (string filterType in filterTypes)
				{
					openPicker.FilePickerFilter.Add(filterType);
				}
			}

			return await openPicker.PickMultipleFilesAsync();
		}

		/// <summary>
		/// Opens a file picker and allows the user to pick one file and returns a StorageFile to the caller
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		public async Task<StorageFile> PickFileAsync(PickerLocationId location = PickerLocationId.DocumentsLibrary,
																				 IList<FilePickerFilter> filePickerFilters)
		{
			FileOpenPicker openPicker = new FileOpenPicker
			                            {
				                            SuggestedStartLocation = location
			                            };

			if (filterTypes != null)
			{
				foreach (string filterType in filterTypes)
				{
					openPicker.FilePickerFilter.Add(filterType);
				}
			}

			return await openPicker.PickSingleFileAsync();
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

		/// <summary>
		/// Opens a file picker and allows the user to pick multiple files and return a list of the files the user chose
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		public async Task<IReadOnlyList<string>> PickMultipleFilesAsync(PickerLocationId location, IList<FilePickerFilter> filePickerFilters)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = TranslateLocation(location);

			openFileDialog.Multiselect = true;
			openFileDialog.ShowDialog();

			if (openFileDialog.FileNames != null)
			{
				return new List<string>(openFileDialog.FileNames);
			}

			return new List<string>();
		}

		public async Task<string> PickFileAsync(PickerLocationId location, IList<FilePickerFilter> fileTypeFilters)
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

	    public async Task<string> PickSaveFileAsync(PickerLocationId location, IList<FilePickerFilter> fileTypeFilters)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.InitialDirectory = TranslateLocation(location);

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
