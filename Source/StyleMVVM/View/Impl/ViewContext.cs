using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.View.Impl
{
	/// <summary>
	/// Wraps a view framework object
	/// </summary>
	public class ViewContext : IViewContext
	{
		/// <summary>
		/// Default constructor takes view
		/// </summary>
		/// <param name="view">view object</param>
		public ViewContext(FrameworkElement view)
		{
			View = view;
		}

		/// <summary>
		/// View
		/// </summary>
		public FrameworkElement View { get; private set; }
	}
}
