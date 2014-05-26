using System;
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
	public class DispatchedMessengerTests
	{
		[TestMethod]
		public void BasicTest()
		{
			DispatchedMessenger messenger = new DispatchedMessenger();

			bool called = false;

			messenger.Register<int>(x =>
			                        {
				                        Assert.AreEqual(5, x);

				                        called = true;
			                        });


			messenger.Send(5);

			Assert.IsTrue(called);
		}

		[TestMethod]
		public void TokenTest()
		{
			DispatchedMessenger messenger = new DispatchedMessenger();

			bool called = false;

			messenger.Register<int>(x =>
			{
				Assert.AreEqual(5, x);

				called = true;
			},"TestToken");


			messenger.Send(5, "TestToken");

			Assert.IsTrue(called);
		}
	}
}
