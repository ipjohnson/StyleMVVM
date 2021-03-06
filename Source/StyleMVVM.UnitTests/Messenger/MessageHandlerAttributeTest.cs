﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.Messenger;

#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace StyleMVVM.UnitTests.Messenger
{
	[TestClass]
	public class MessageHandlerAttributeTest
	{
		[TestMethod]
		public void MessageHandlerAttribute()
		{
			DependencyInjectionContainer container = new DependencyInjectionContainer();

            container.Configure(c => c.Export<DispatchedMessenger>().As<IDispatchedMessenger>().Lifestyle.Singleton());

			TestClass tc = container.Locate<TestClass>();

			IDispatchedMessenger dispatchedMessenger = container.Locate<IDispatchedMessenger>();

			dispatchedMessenger.Send(5);

			Assert.AreEqual(5, tc.TestInt);
		}

		[TestMethod]
		public void MessageHandlerAttributeToken()
		{
			DependencyInjectionContainer container = new DependencyInjectionContainer();

            container.Configure(c => c.Export<DispatchedMessenger>().As<IDispatchedMessenger>().Lifestyle.Singleton());

			TestClass tc = container.Locate<TestClass>();

			IDispatchedMessenger dispatchedMessenger = container.Locate<IDispatchedMessenger>();

			dispatchedMessenger.Send(5, "Multiply");

			Assert.AreEqual(50, tc.TestInt);
		}
	}
}
