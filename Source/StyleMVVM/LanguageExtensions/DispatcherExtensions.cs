using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Core;
#else
using System.Windows.Threading;
#endif

namespace StyleMVVM.LanguageExtensions
{

#if !NETFX_CORE
	/// <summary>
	/// Dispatcher priority
	/// </summary>
	public enum CoreDispatcherPriority
	{
		/// <summary>
		/// Idle
		/// </summary>
		Idle = -2,

		/// <summary>
		/// Low priority
		/// </summary>
		Low = -1,

		/// <summary>
		/// Normal priority
		/// </summary>
		Normal = 0,

		/// <summary>
		/// High priority
		/// </summary>
		High = 1
	}

#endif

	/// <summary>
	/// Dispatcher Extensions
	/// </summary>
	public static class DispatcherExtensions
	{
#if NETFX_CORE
		
		/// <summary>
		/// Has thread access to this dispatcher
		/// </summary>
		/// <param name="dispatcher">dispatcher</param>
		/// <returns>has access</returns>
		public static bool HasThreadAccess(this CoreDispatcher dispatcher)
		{
			return dispatcher.HasThreadAccess;
		}

#else
		/// <summary>
		/// Has thread access to this dispatcher
		/// </summary>
		/// <param name="dispatcher">dispatcher</param>
		/// <returns>has access</returns>
		public static bool HasThreadAccess(this Dispatcher dispatcher)
		{
			return dispatcher.CheckAccess();
		}

		/// <summary>
		/// Run an action on a dispatcher
		/// </summary>
		/// <param name="dispatcher">dispatcher</param>
		/// <param name="priority">priority</param>
		/// <param name="action">action</param>
		/// <returns>task</returns>
		public static Task RunAsync(this Dispatcher dispatcher, CoreDispatcherPriority priority, Action action)
		{
			DispatcherPriority dispatcherPriority = DispatcherPriority.Normal;

			switch (priority)
			{
				case CoreDispatcherPriority.Idle:
					dispatcherPriority = DispatcherPriority.Background;
					break;
				case CoreDispatcherPriority.Low:
					dispatcherPriority = DispatcherPriority.Render;
					break;
				case CoreDispatcherPriority.High:
					dispatcherPriority = DispatcherPriority.Send;
					break;
			}

			return dispatcher.BeginInvoke(dispatcherPriority, action).Task;
		}

#endif

	}
}
