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
	public class StyleNavigationEventArgs
	{
		private Uri uri;
		private object parameter;
		private object content;
		private NavigationMode navigationMode = NavigationMode.New;
		private readonly NavigationEventArgs args;

		public StyleNavigationEventArgs()
		{
		}

		public StyleNavigationEventArgs(NavigationEventArgs args)
		{
			this.args = args;
		}

		public Uri Uri
		{
			get
			{
				if (args != null)
				{
					return args.Uri;
				}

				return uri;
			}
			set
			{
				if (args == null)
				{
					uri = value;
				}
			}
		}

#if !WINDOWS_PHONE
		public object Parameter
		{
			get
			{
				if (args != null)
				{
#if NETFX_CORE
					return args.Parameter;
#else
					return args.ExtraData;
					
#endif
				}

				return parameter;
			}
			set
			{
				if (args == null)
				{
					parameter = value;
				}
			}
		}
#endif

		public object Content
		{
			get
			{
				if (args != null)
				{
					return args.Content;
				}

				return content;
			}
			set
			{
				if (args == null)
				{
					content = value;
				}
			}
		}

#if NETFX_CORE
		public NavigationMode NavigationMode
		{
			get
			{
				if (args != null)
				{
					return args.NavigationMode;
				}

				return navigationMode;
			}
			set
			{
				if (args != null)
				{
					throw new Exception("You can't set the navigation mode when navigation args are specified");
				}

				navigationMode = value;
			}
		}
#endif
	}
}
