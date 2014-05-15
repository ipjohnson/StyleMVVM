using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// View models that want to recieve a navigation parameter should implement this interface
	/// </summary>
	public interface INavigationParameterAwareViewModel
	{
		/// <summary>
		/// The NavigationParameter for the Page this ViewModel is associated with
		/// </summary>
		object NavigationParameter { get; set; }
	}
}
