using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET_PORTABLE
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows.Controls;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// Represents an object that can Navigate a frame within the app.
	/// At Runtime this just wraps a Frame class, at UnitTest Time this is 
	/// substituted for something else
	/// </summary>
	public interface INavigationService : INotifyPropertyChanged
	{
		/// <summary>
		/// Frame this navigation service is associated with
		/// </summary>
		Frame Frame { get; }

		/// <summary>
		/// Is the navigation service currently valid
		/// </summary>
		bool IsValid { get; }

		/// <summary>
		/// Can the service navigate back
		/// </summary>
		bool CanGoBack { get; }

		/// <summary>
		/// Can the service navigate forward
		/// </summary>
		bool CanGoForward { get; }

		/// <summary>
		/// Navigate Home if possible
		/// </summary>
		void GoHome();

		/// <summary>
		/// Navigate Back if possible
		/// </summary>
		void GoBack();

		/// <summary>
		/// Navigate forward if possible
		/// </summary>
		void GoForward();

		/// <summary>
		/// Navigate to a new page by name
		/// </summary>
		/// <param name="pageName">name of the page (usually class name)</param>
		/// <param name="navigationParam">navigation parameter</param>
		/// <returns>true if it could navigate to said page</returns>
		bool Navigate(string pageName, object navigationParam = null);

		/// <summary>
		/// Navigate to a new page By Type
		/// </summary>
		/// <param name="pageType">new page type</param>
		/// <param name="navigationParam">navigation parameter</param>
		/// <returns>true if it could navigate to said page</returns>
		bool Navigate(Type pageType, object navigationParam = null);
	}
}
