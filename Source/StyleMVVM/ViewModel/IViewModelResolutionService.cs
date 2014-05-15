using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET_PORTABLE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.ViewModel
{
	public interface IViewModelResolutionService
	{
		/// <summary>
		/// This method resolves a viewmodel for a view and binds it.
		/// </summary>
		/// <param name="view"></param>
		/// <param name="viewModelName"></param>
		/// <returns></returns>
		bool ResolveViewModel(FrameworkElement view, string viewModelName);

		/// <summary>
		/// This method resolves a viewmodel for a view and binds it.
		/// </summary>
		/// <param name="view"></param>
		/// <param name="viewModelName"></param>
		/// <returns></returns>
		void ResolveViewModel(FrameworkElement view, Type viewModelName);
	}
}
