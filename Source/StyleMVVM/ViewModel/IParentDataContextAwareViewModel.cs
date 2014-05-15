using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// ViewModels that wish to know what the data context is for the parent view can implement this interface and ParentDataContext will
	/// be populated when the view is loaded into the visual tree.
	/// </summary>
	public interface IParentDataContextAwareViewModel
	{
		/// <summary>
		/// The Data Context associated with the parent View for this view model
		/// </summary>
		object ParentDataContext { get; set; }
	}
}
