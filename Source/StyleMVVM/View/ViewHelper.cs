using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Core;
using Windows.UI.Xaml;
#else
using System.Windows;
using System.Windows.Threading;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// Static helper methods for helping with the view
	/// </summary>
	public static class ViewHelper
	{
#if NETFX_CORE
		public static CoreDispatcher MainDispatcher
		{
			get
			{
				if (Window.Current != null && Window.Current.Content != null)
				{
					return Window.Current.Content.Dispatcher;
				}

				return null;
			}
		}

#else
		public static Dispatcher MainDispatcher
		{
			get
			{
				if (Application.Current != null)
				{
					return Application.Current.Dispatcher;
				}

				return null;
			}
		}
#endif

		public static UIElement MainContent
		{
			get
			{
#if NETFX_CORE
				if (Window.Current != null)
				{
					return Window.Current.Content;
				}

				return null;

#else
				if (Application.Current != null)
				{
					return Application.Current.MainWindow;
				}

				return null;
#endif
			}
		}
	}
}
