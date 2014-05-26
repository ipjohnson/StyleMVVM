using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grace.Utilities;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Weak message handler
	/// </summary>
	/// <typeparam name="TMessage"></typeparam>
	public class WeakMessageHandler<TMessage> : BaseMessageHandler
	{
		private readonly WeakAction<TMessage> weakHandler;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="handler">message handler</param>
		/// <param name="background">background handler</param>
		public WeakMessageHandler(Action<TMessage> handler, bool background)
		{
			weakHandler = new WeakAction<TMessage>(handler);

			Background = background;
		}

		/// <summary>
		/// is this wrapper, wrapping the specified action
		/// </summary>
		/// <param name="action">Action&lt;T&gt;</param>
		/// <returns>is wrapper</returns>
		public override bool IsWrapping(object action)
		{
			Action<TMessage> messageHandler = action as Action<TMessage>;

			return messageHandler != null && 
			       messageHandler.Target == weakHandler.Target && 
			       messageHandler.GetMethodInfo().Equals(weakHandler.MethodInfo);
		}

		/// <summary>
		/// Is the handler still alive
		/// </summary>
		public override bool IsAlive
		{
			get { return weakHandler.IsAlive; }
		}

		/// <summary>
		/// Internal execute
		/// </summary>
		/// <param name="message"></param>
		protected override void InternalExecute(object message)
		{
			TMessage tMessage =(TMessage) message;

			weakHandler.Invoke(tMessage);
		}
	}
}
