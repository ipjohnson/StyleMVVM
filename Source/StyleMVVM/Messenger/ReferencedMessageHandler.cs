using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.Messenger
{
	public class ReferencedMessageHandler<TMessage> : BaseMessageHandler
	{
		private readonly Action<TMessage> messageHandler;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="messageHandler">message handler</param>
		/// <param name="background">background</param>
		public ReferencedMessageHandler(Action<TMessage> messageHandler, bool background)
		{
			this.messageHandler = messageHandler;
			
			Background = background;
		}
 
		/// <summary>
		/// Is the handler wrapped
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public override bool IsWrapping(object action)
		{
			Action<TMessage> localAction = action as Action<TMessage>;

			if (localAction != null)
			{
				return localAction.Equals(messageHandler);
			}

			return false;
		}

		/// <summary>
		/// Is the handler alive
		/// </summary>
		public override bool IsAlive
		{
			get { return true; }
		}

		/// <summary>
		/// Internal execute
		/// </summary>
		/// <param name="message"></param>
		protected override void InternalExecute(object message)
		{
			TMessage tMessage = (TMessage)message;

			messageHandler(tMessage);
		}
	}
}
