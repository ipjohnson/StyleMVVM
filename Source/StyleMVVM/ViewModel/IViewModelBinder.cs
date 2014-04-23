using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.View;

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// Any class that implements and export this interface will be included in the ViewModel binding process
	/// </summary>
	public interface IViewModelBinder
	{
		/// <summary>
		/// This method will be called each time a ViewModel is being bound to a View
		/// </summary>
		/// <param name="frameworkElement">View object</param>
		/// <param name="viewModel">ViewModel object</param>
		void BindViewModelToView(IFrameworkElementWrapper frameworkElement, object viewModel);

		/// <summary>
		/// This method is called to unbind a ViewModel (Only happens when the ViewModel.Name property is changed after a view is constructed)
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		void UnbindViewModelFromView(IFrameworkElementWrapper frameworkElement, object viewModel);
	}
}
