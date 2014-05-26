using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.Logging;
using StyleMVVM.LanguageExtensions;
using StyleMVVM.Messenger;
using StyleMVVM.View;

#if NET_PORTABLE
using Windows.UI.Core;
#endif

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Base message handler
	/// </summary>
	public abstract class BaseMessageHandler : IMessageHandlerWrapper
	{
		/// <summary>
		/// Back ground thread
		/// </summary>
		protected bool Background { get; set; }

		/// <summary>
		/// Is this handler wrapping action
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public abstract bool IsWrapping(object action);

		/// <summary>
		/// Is handler alive
		/// </summary>
		public abstract bool IsAlive { get; }

		/// <summary>
		/// Execute the message handler
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool Execute(object message)
		{
			if (IsAlive)
			{
				var dispatcher = ViewHelper.MainDispatcher;

				if (Background ||
						(dispatcher != null &&
						!dispatcher.HasThreadAccess()))
				{
					dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						() =>
						{
							try
							{
								InternalExecute(message);
							}
							catch (Exception exp)
							{
								Logger.Error(
									"Exception thrown while executing message async on dispatcher handler",
									GetType().FullName,
									exp);
							}
						});
				}
				else
				{
					InternalExecute(message);
				}
			}

			return false;
		}

		/// <summary>
		/// Internal execute the handler
		/// </summary>
		/// <param name="message">message</param>
		protected abstract void InternalExecute(object message);
	}
}
