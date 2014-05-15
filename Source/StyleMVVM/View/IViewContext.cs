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

namespace StyleMVVM.View
{
	/// <summary>
	/// Represents a view
	/// </summary>
	public interface IViewContext
	{
		/// <summary>
		/// View being wrapped
		/// </summary>
		FrameworkElement View { get; }
	}
}
