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
		/// Register message handler
		/// </summary>
		/// <typeparam name="T">message type</typeparam>
		/// <param name="handler">message handler</param>
		/// <param name="holdReference">hold a reference to the object referenced by the handler</param>
		/// <param name="backGround">execute the callback on a back ground thread</param>
		void Register<T>(Action<T> handler, bool holdReference = false, bool backGround = false);

		/// <summary>
		/// Unregister message handler
		/// </summary>
		/// <typeparam name="T">message type</typeparam>
		/// <param name="handler">message handler</param>
		void Unregister<T>(Action<T> handler);
	}
}
