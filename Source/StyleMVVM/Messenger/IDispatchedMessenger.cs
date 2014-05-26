using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Dispatched message handler
	/// </summary>
	public interface IDispatchedMessenger
	{
		/// <summary>
		/// Send a message
		/// </summary>
		/// <typeparam name="TMessage"></typeparam>
		/// <param name="message"></param>
		/// <param name="token"></param>
		void Send<TMessage>(TMessage message, object token = null);

		/// <summary>
		/// Register message handler
		/// </summary>
		/// <typeparam name="TMessage">message type</typeparam>
		/// <param name="handler">message handler</param>
		/// <param name="token">token to use for filtering</param>
		/// <param name="holdReference">hold a reference to the object referenced by the handler</param>
		/// <param name="backGround">execute the callback on a back ground thread</param>
		void Register<TMessage>(Action<TMessage> handler, object token = null, bool holdReference = false, bool backGround = false);

		/// <summary>
		/// Unregister message handler
		/// </summary>
		/// <typeparam name="TMessage">message type</typeparam>
		/// <param name="handler">message handler</param>
		/// <param name="token"></param>
		void Unregister<TMessage>(Action<TMessage> handler, object token = null);
	}
}
