using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.Utilities;

namespace StyleMVVM.View
{
	/// <summary>
	/// translates a picker location into a string
	/// </summary>
	public interface IPickerLocationIdTranslator
	{
		/// <summary>
		/// Translate a picker location into a location on disk
		/// </summary>
		/// <param name="locationId"></param>
		/// <returns></returns>
		string Translate(PickerLocationId locationId);
	}
}
