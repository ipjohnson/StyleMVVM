using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE

using Windows.ApplicationModel;
#else
using System.Windows;
#endif

namespace StyleMVVM.Ultilities
{
	public static class DesignModeUtility
	{

		public static bool DesignModeIsEnabled
		{
			get
			{
#if NETFX_CORE
				return DesignMode.DesignModeEnabled;
#else
				return false;
#endif
			}
		}
	}
}
