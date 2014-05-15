using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET_PORTABLE
using Windows.UI.Xaml.Navigation;
#else
using System.Windows.Navigation;
#endif

namespace StyleMVVM.View
{
	public class StyleNavigatingCancelEventArgs
	{
		private bool cancel;
		private readonly NavigatingCancelEventArgs cancelArg;

		public StyleNavigatingCancelEventArgs()
		{
		}

		public StyleNavigatingCancelEventArgs(NavigatingCancelEventArgs cancelArg)
		{
			this.cancelArg = cancelArg;
		}

		public bool Cancel
		{
			get
			{
				if (cancelArg != null)
				{
					return cancelArg.Cancel;
				}

				return cancel;
			}
			set
			{
				if (cancelArg != null)
				{
					cancelArg.Cancel = value;
				}
				else
				{
					cancel = value;
				}
			}
		}

		public NavigationMode NavigationMode
		{
			get { return cancelArg.NavigationMode; }
		}
	}
}
