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

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// This interface is implemented by ViewModels that need access to the
	/// view they are associated to
	/// </summary>
	public interface IViewAware
	{
		/// <summary>
		/// The view this viewmodel is associated with
		/// </summary>
		FrameworkElement View { get; set; }
	}
}
