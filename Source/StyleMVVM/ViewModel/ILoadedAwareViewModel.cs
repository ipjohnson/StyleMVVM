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
	/// <summary>
	/// ViewModels implement this interface when they want to
	/// hook into the FrameworkElement Loaded/Unloaded events
	/// </summary>
	public interface ILoadedAwareViewModel
	{
		/// <summary>
		/// Called when the view is Loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void OnLoaded(object sender, RoutedEventArgs args);

		/// <summary>
		/// Called when the view is Unloaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void OnUnloaded(object sender, RoutedEventArgs args);
	}
}
