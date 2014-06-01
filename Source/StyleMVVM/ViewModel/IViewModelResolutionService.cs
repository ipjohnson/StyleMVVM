using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
#if NETFX_CORE
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
		/// <param name="exportLocator">export locator to use for resolving models</param>
		/// <param name="view">view to bind to </param>
		/// <param name="viewModelName">model name to use</param>
		/// <returns>true if the model could be resolved</returns>
		bool ResolveViewModel(IExportLocator exportLocator, FrameworkElement view, string viewModelName);

		/// <summary>
		/// This method resolves a viewmodel for a view and binds it.
		/// </summary>
		/// <param name="exportLocator">export locator to use for resolving models</param>
		/// <param name="view">view to bind to</param>
		/// <param name="viewModelName">view model type to use</param>
		/// <returns>true if the model could be resolved</returns>
		void ResolveViewModel(IExportLocator exportLocator,  FrameworkElement view, Type viewModelName);
	}
}
