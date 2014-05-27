using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Grace.Logging;

#if NETFX_CORE
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
#else
using System.Windows.Threading;
#endif

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// This class is intended to allow a message sender to get a callback from the receiver with a return. 
	/// It is dispatcher aware so you will get the call back on the correct dispatcher
	/// </summary>
	/// <typeparam name="T">the message callback parameter.</typeparam>
	public class CallbackMessage<T>
	{
#if NETFX_CORE
		private CoreDispatcher dispatcher;
#else

		private readonly Dispatcher dispatcher;
#endif
		private readonly Action<T> callback;

		public CallbackMessage(Action<T> callback)
		{
#if NETFX_CORE
			if (Window.Current != null && Window.Current.Dispatcher != null && Window.Current.Dispatcher.HasThreadAccess)
			{
				dispatcher = Window.Current.Dispatcher;
			}
#else

			if (!(Thread.CurrentThread.IsBackground || Thread.CurrentThread.IsThreadPoolThread))
			{
				dispatcher = Dispatcher.CurrentDispatcher;
			}
#endif
			this.callback = callback;
		}

		public void Invoke(T parameter)
		{
			if (dispatcher != null)
			{
				bool access;

#if NETFX_CORE
				
				access = dispatcher.HasThreadAccess;
#else
				access = dispatcher.CheckAccess();
#endif

				if (access)
				{
					ExecuteCallback(parameter);
				}
				else
				{
#if NETFX_CORE
					dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ExecuteCallback(parameter));
					
#else
					dispatcher.BeginInvoke(new Action(() => ExecuteCallback(parameter)));
#endif
				}
			}
			else
			{
#if NETFX_CORE
				ThreadPool.RunAsync((x) => ExecuteCallback(parameter));
#else

				ThreadPool.QueueUserWorkItem((x) => ExecuteCallback(parameter));
#endif
			}
		}

		private void ExecuteCallback(T parameter)
		{
			try
			{
				callback(parameter);
			}
			catch (Exception exp)
			{
				Logger.Error(
					"Exception thrown on callback", GetType().FullName, exp);
			}
		}
	}
}
