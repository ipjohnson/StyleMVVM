using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using Grace.DependencyInjection.Attributes.Interfaces;

namespace StyleMVVM.Messenger
{
	/// <summary>
	/// methods attributed with this will be registered for messages
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class MessageHandlerAttribute : Attribute, ICustomEnrichmentExpressionAttribute
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public MessageHandlerAttribute()
		{
			HoldReference = false;
		}

		/// <summary>
		/// Hold a reference to the handler, false by default
		/// </summary>
		public bool HoldReference { get; set; }

		/// <summary>
		/// Token to register with
		/// </summary>
		public object Token { get; set; }

		/// <summary>
		/// Get a custom enrichment provider, you are given the attributed type and possible attributed member
		/// </summary>
		/// <param name="attributedType">attributed type</param><param name="attributedMember">PropertyInfo or MethodInfo that was attributed, can be null if the class was attributed</param>
		/// <returns>
		/// custom linq expression provider
		/// </returns>
		public ICustomEnrichmentLinqExpressionProvider GetProvider(Type attributedType, object attributedMember)
		{
			MethodInfo methodInfo = attributedMember as MethodInfo;

			if (methodInfo == null)
			{
				return null;
			}

			int parameterCount = methodInfo.GetParameters().Count();

			return parameterCount != 1 ? null : 
					 new MessageHandlerEnrichmentProvider(methodInfo, GetToken(attributedType, methodInfo), HoldReference, false);
		}

		/// <summary>
		/// Get the token to registered
		/// </summary>
		/// <param name="attributedType"></param>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		protected virtual object GetToken(Type attributedType, MethodInfo methodInfo)
		{
			return Token;
		}
	}
}
