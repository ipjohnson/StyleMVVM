using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Xaml.Navigation;
#else
using System.Windows.Navigation;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// This interface needs to be implemented in a Page if the viewmodel 
	/// is to have full navigation access.
	/// </summary>
	public interface INavigatingPage
	{
		/// <summary>
		/// NavigatedTo event that is called when Page calls OnNavigatedTo
		/// </summary>
		event EventHandler<NavigationEventArgs> NavigatedTo;

		/// <summary>
		/// NavigatedFrom event that is called when Page calls OnNavigatedFrom
		/// </summary>
		event EventHandler<NavigationEventArgs> NavigatedFrom;

		/// <summary>
		/// NavigatingFrom event that is called when Page calls OnNavigatingFrom
		/// </summary>
		event EventHandler<NavigatingCancelEventArgs> NavigatingFrom;
	}
}
