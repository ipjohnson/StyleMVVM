using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Dispatched messenger
	/// </summary>
	public class DispatchedMessenger : IDispatchedMessenger
	{
		private ConcurrentDictionary<Type, ConcurrentDictionary<object, List<IMessageHandlerWrapper>>> messageHandlersByType =
			new ConcurrentDictionary<Type, ConcurrentDictionary<object, List<IMessageHandlerWrapper>>>();

		/// <summary>
		/// Default token to use when registering
		/// </summary>
		public const string DefaultToken = "DefaultToken";

		/// <summary>
		/// Send a message
		/// </summary>
		/// <typeparam name="TMessage">Message type to send</typeparam>
		/// <param name="message">message</param>
		/// <param name="token">token to send with</param>
		public void Send<TMessage>(TMessage message, object token = null)
		{
			if (token == null)
			{
				token = DefaultToken;
			}

			SendMessageByType(message, typeof(TMessage), token);
		}

		private void SendMessageByType(object message, Type messageType, object token)
		{
			while (messageType != null)
			{
				ConcurrentDictionary<object, List<IMessageHandlerWrapper>> handlerDictionary;

				if (messageHandlersByType.TryGetValue(messageType, out handlerDictionary))
				{
					List<IMessageHandlerWrapper> handlerWrappers;

					if (handlerDictionary.TryGetValue(token, out handlerWrappers))
					{
						List<IMessageHandlerWrapper> deleteList = null;

						foreach (IMessageHandlerWrapper messageHandlerWrapper in handlerWrappers)
						{
							if (!messageHandlerWrapper.Execute(message))
							{
								if (deleteList == null)
								{
									deleteList = new List<IMessageHandlerWrapper>();
								}

								deleteList.Add(messageHandlerWrapper);
							}
						}

						if (deleteList != null)
						{
							handlerDictionary.AddOrUpdate(token, 
								obj => new List<IMessageHandlerWrapper>(), 
								(obj, currentList) =>
							      {
								      List<IMessageHandlerWrapper> newList = new List<IMessageHandlerWrapper>();

								      foreach (IMessageHandlerWrapper messageHandlerWrapper in currentList)
								      {
									      bool add = true;

									      foreach (IMessageHandlerWrapper handlerWrapper in deleteList)
									      {
										      if (messageHandlerWrapper.Equals(handlerWrapper))
										      {
											      add = false;
											      deleteList.Remove(handlerWrapper);
											      break;
										      }
									      }

									      if (add)
									      {
										      newList.Add(messageHandlerWrapper);
									      }
								      }

								      return newList;
							      });
						}
					}
				}

				Type baseType = messageType.GetTypeInfo().BaseType;

				if (baseType != null && baseType != typeof(object))
				{
					messageType = baseType;
				}
				else
				{
					messageType = null;
				}
			}
		}

		/// <summary>
		/// Register a message handler
		/// </summary>
		/// <typeparam name="TMessage">message type</typeparam>
		/// <param name="handler">message handler</param>
		/// <param name="token">token</param>
		/// <param name="holdReference">hold reference</param>
		/// <param name="backGround">run in background</param>
		public void Register<TMessage>(Action<TMessage> handler, object token = null, bool holdReference = false, bool backGround = false)
		{
			if (token == null)
			{
				token = DefaultToken;
			}

			IMessageHandlerWrapper handlerWrapper = null;

			if (holdReference)
			{
				handlerWrapper = new ReferencedMessageHandler<TMessage>(handler, backGround);
			}
			else
			{
				handlerWrapper = new WeakMessageHandler<TMessage>(handler, backGround);
			}

			ConcurrentDictionary<object, List<IMessageHandlerWrapper>> handlerDictionary;

			if (!messageHandlersByType.TryGetValue(typeof(TMessage), out handlerDictionary))
			{
				messageHandlersByType.AddOrUpdate(typeof(TMessage),
					t =>
					{
						handlerDictionary = new ConcurrentDictionary<object, List<IMessageHandlerWrapper>>();

						return handlerDictionary;
					},
					(t, current) =>
					{
						handlerDictionary = current;

						return current;
					});
			}

			handlerDictionary.AddOrUpdate(token,
				t => new List<IMessageHandlerWrapper> { handlerWrapper },
				(t, current) => new List<IMessageHandlerWrapper>(current) { handlerWrapper });
		}

		/// <summary>
		/// Unregister handler
		/// </summary>
		/// <typeparam name="TMessage">message handler type</typeparam>
		/// <param name="handler">handler</param>
		/// <param name="token">token</param>
		public void Unregister<TMessage>(Action<TMessage> handler, object token = null)
		{
			if (token == null)
			{
				token = DefaultToken;
			}

			ConcurrentDictionary<object, List<IMessageHandlerWrapper>> handlerDictionary;

			if (messageHandlersByType.TryGetValue(typeof(TMessage), out handlerDictionary))
			{
				handlerDictionary.AddOrUpdate(token,
					t => new List<IMessageHandlerWrapper>(),
					(t, current) =>
					{
						var newList = new List<IMessageHandlerWrapper>(current);

						newList.RemoveAll(x => x.IsWrapping(handler));

						return newList;
					});
			}
		}
	}
}
