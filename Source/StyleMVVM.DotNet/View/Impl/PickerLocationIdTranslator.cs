using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.Utilities;

namespace StyleMVVM.View.Impl
{
	/// <summary>
	/// Translates a picker location to a path
	/// </summary>
	public class PickerLocationIdTranslator : IPickerLocationIdTranslator
	{
		/// <summary>
		/// Translate picker to location
		/// </summary>
		/// <param name="locationId">location</param>
		/// <returns></returns>
		public string Translate(PickerLocationId locationId)
		{
			switch (locationId)
			{
				case PickerLocationId.ComputerFolder:
					return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
					break;
				case PickerLocationId.Desktop:
					return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
					break;
				case PickerLocationId.DocumentsLibrary:
					return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					break;
				case PickerLocationId.Downloads:
					string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
					return Path.Combine(pathUser, "Downloads");
					break;
				case PickerLocationId.HomeGroup:
					break;
				case PickerLocationId.MusicLibrary:
					return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
					break;
				case PickerLocationId.PicturesLibrary:
					return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
					break;
				case PickerLocationId.VideosLibrary:
					return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
					break;
			}

			return null;
		}
	}
}
