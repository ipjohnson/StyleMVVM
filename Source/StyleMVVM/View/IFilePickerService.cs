﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.Storage;
using Windows.Storage.Pickers;
#else
using StyleMVVM.Utilities;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// Abstraction for picking files
	/// </summary>
	public interface IFilePickerService
	{
#if NETFX_CORE
		/// <summary>
		/// Opens a file picker and allows the user to pick multiple files and return a list of the files the user chose
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		Task<IReadOnlyList<StorageFile>> PickMultipleFilesAsync(
			PickerLocationId location, params string[] filterTypes);

		/// <summary>
		/// Opens a file picker and allows the user to pick one file and returns a StorageFile to the caller
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		Task<StorageFile> PickFileAsync(PickerLocationId location, params string[] filterTypes);
#else
		/// <summary>
		/// Opens a file picker and allows the user to pick multiple files and return a list of the files the user chose
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		Task<IReadOnlyList<string>> PickMultipleFilesAsync(
			PickerLocationId location, params string[] filterTypes);

		/// <summary>
		/// Opens a file picker and allows the user to pick one file and returns a StorageFile to the caller
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		Task<string> PickFileAsync(PickerLocationId location, params string[] filterTypes);

		/// <summary>
		/// Opens the Save file dialog allowing a user to pick a file to save to
		/// </summary>
		/// <param name="location"></param>
		/// <param name="filterTypes"></param>
		/// <returns></returns>
		Task<string> PickSaveFileAsync(PickerLocationId location, params string[] filterTypes);
#endif
	}
}
