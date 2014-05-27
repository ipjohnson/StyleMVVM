using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// Extensions for registering message handlers
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public static class IExportTypeSetConfigurationExtensions
	{
		static IExportTypeSetConfigurationExtensions()
		{
			MessageHandlerPostFix = "MessageHandler";
		}

		/// <summary>
		/// Postfix for message handler methods
		/// </summary>
		public static string MessageHandlerPostFix { get; set; }

		/// <summary>
		/// Registers all message handlers with the DispatchedMessenger
		/// Note: Message handlers are defined as public non-static methods that have one parameter
		/// and end in MessageHandler
		/// </summary>
		/// <param name="configuration">configuration object</param>
		/// <param name="messageHandlerPostfix">message handler postfix</param>
		/// <returns></returns>
		public static IExportTypeSetConfiguration RegisterMessageHandlers(this IExportTypeSetConfiguration configuration, string messageHandlerPostfix = null)
		{
			configuration.EnrichWithExpression(type => ProcessTypeForMessageHandlerMethods(type, messageHandlerPostfix));

			return configuration;
		}

		private static IEnumerable<ICustomEnrichmentLinqExpressionProvider> ProcessTypeForMessageHandlerMethods(Type exportType, string messageHandlerPostfix)
		{
			if (messageHandlerPostfix == null)
			{
				messageHandlerPostfix = MessageHandlerPostFix;
			}

			foreach (MethodInfo methodInfo in exportType.GetRuntimeMethods())
			{
				if (methodInfo.IsStatic ||
					!methodInfo.IsPublic ||
					!methodInfo.Name.EndsWith(messageHandlerPostfix))
				{
					continue;
				}

				yield return new MessageHandlerEnrichmentProvider(methodInfo, null, false, false);
			}
		}
	}
}
