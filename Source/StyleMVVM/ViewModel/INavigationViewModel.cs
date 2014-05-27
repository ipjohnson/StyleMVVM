using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.View;

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// This interfaces is implemented by ViewModels that are interested
	/// in page Navigation (use only as ViewModels for Page Views)
	/// </summary>
	public interface INavigationViewModel : INavigationParameterAwareViewModel
	{
		/// <summary>
		/// Called when the Page is Navigated To
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void NavigatedTo(object sender, StyleNavigationEventArgs e);

		/// <summary>
		/// Called when the PAge is navigated from
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void NavigatedFrom(object sender, StyleNavigationEventArgs e);

		/// <summary>
		/// Called directly before the page is navigated away from
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		Task NavigatingFrom(object sender, StyleNavigatingCancelEventArgs e);
	}
}
