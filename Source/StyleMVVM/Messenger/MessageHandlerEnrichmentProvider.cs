using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using Grace.DependencyInjection.Attributes.Interfaces;
using Grace.Utilities;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Provides linq expressions to enrich an export delegate
	/// </summary>
	public class MessageHandlerEnrichmentProvider : ICustomEnrichmentLinqExpressionProvider
	{
		private static MethodInfo RegisterMethod;

		static MessageHandlerEnrichmentProvider()
		{
			RegisterMethod = typeof(MessageHandlerEnrichmentProvider).GetRuntimeMethod("RegisterMethodAsMessageHandler", new[]
			                                                                                         {
				                                                                                         typeof(IInjectionScope),
																																	  typeof(IInjectionContext),
																																	  typeof(object),
																																	  typeof(MethodInfoWrapper),
																																	  typeof(object),
																																	  typeof(bool),
																																	  typeof(bool)
			                                                                                         });
		}


		private readonly bool holdReference;
		private readonly bool background;
		private readonly object token;
		private readonly MethodInfo messageHandler;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="messageHandler">message handler</param>
		/// <param name="token">message token</param>
		/// <param name="holdReference">hold reference</param>
		/// <param name="background">background</param>
		public MessageHandlerEnrichmentProvider(MethodInfo messageHandler, object token, bool holdReference, bool background)
		{
			if (messageHandler.GetParameters().Count() != 1)
			{
				throw new ArgumentException("Method must have one parameter");
			}

			this.messageHandler = messageHandler;
			this.holdReference = holdReference;
			this.background = background;
			this.token = token;
		}

		/// <summary>
		/// Provide a list of linq expressions that will be added to the Linq expression tree
		/// </summary>
		/// <param name="context"/>
		/// <returns/>
		public IEnumerable<Expression> ProvideExpressions(ICustomEnrichmentLinqExpressionContext context)
		{
			MethodInfo closedMethodInfo =
				RegisterMethod.MakeGenericMethod(messageHandler.GetParameters().Select(x => x.ParameterType).ToArray());

			yield return Expression.Call(closedMethodInfo,
													context.ExportStrategyScopeParameter,
													context.InjectionContextParameter,
													context.InstanceVariable,
													Expression.Constant(new MethodInfoWrapper(messageHandler)),
													Expression.Constant(token),
													Expression.Constant(holdReference),
													Expression.Constant(background));
		}

		/// <summary>
		/// Registers an object for message handling
		/// </summary>
		/// <typeparam name="TMessage">message type</typeparam>
		/// <param name="injectionScope"></param>
		/// <param name="injectionContext"></param>
		/// <param name="injectionTaget"></param>
		/// <param name="messageHandlerInfo"></param>
		/// <param name="token"></param>
		/// <param name="holdRefernce"></param>
		/// <param name="background"></param>
		public static void RegisterMethodAsMessageHandler<TMessage>(IInjectionScope injectionScope,
																						IInjectionContext injectionContext,
																						object injectionTaget,
																						MethodInfoWrapper messageHandlerInfo,
																						object token,
																						bool holdRefernce,
																						bool background)
		{
			Action<TMessage> messageHandler = (Action<TMessage>)messageHandlerInfo.MethodInfo.CreateDelegate(typeof(Action<TMessage>), injectionTaget);

			IDispatchedMessenger messenger = injectionScope.Locate<IDispatchedMessenger>();

			if (messenger == null)
			{
				throw new Exception("Attempting to register handler " + messageHandlerInfo.MethodInfo.Name + " and could not locate IDispatchedMessenger");
			}

			messenger.Register(messageHandler, token, holdRefernce, background);
		}
	}
}
