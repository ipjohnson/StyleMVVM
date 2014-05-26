using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Wraps a message handler
	/// </summary>
	public interface IMessageHandlerWrapper
	{
		/// <summary>
		/// is this wrapper, wrapping the specified action
		/// </summary>
		/// <param name="action">Action&lt;T&gt;</param>
		/// <returns>is wrapper</returns>
		bool IsWrapping(object action);

		/// <summary>
		/// Is the handler still alive
		/// </summary>
		bool IsAlive { get; }

		/// <summary>
		/// Execute the handler
		/// </summary>
		/// <param name="message">message to send</param>
		/// <returns>is handler still alive</returns>
		bool Execute(object message);
	}
}
