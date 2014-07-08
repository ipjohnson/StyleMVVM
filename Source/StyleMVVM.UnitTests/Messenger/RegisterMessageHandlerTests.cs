using System.Reflection;
using Grace.DependencyInjection;
using StyleMVVM.DotNet.UnitTests.Messenger;
using StyleMVVM.Messenger;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace StyleMVVM.UnitTests.Messenger
{
	[TestClass]
	public class RegisterMessageHandlerTests
	{
		[TestMethod]
		public void RegisterMessageHandlerTest()
		{
			DependencyInjectionContainer container = new DependencyInjectionContainer();

			container.Configure(c =>
			                    {
				                    c.Export(GetType().GetTypeInfo().Assembly.ExportedTypes).
					                    ByType().
					                    RegisterMessageHandlers();
				                    c.Export<DispatchedMessenger>().As<IDispatchedMessenger>().AndSingleton();
			                    });

			ConventionClass convention = container.Locate<ConventionClass>();

			IDispatchedMessenger messenger = container.Locate<IDispatchedMessenger>();

			messenger.Send(5);

			Assert.AreEqual(5, convention.Test);

			messenger.Send(new TestClass{ TestInt = 50});

			Assert.AreEqual(50, convention.Test);
		}
	}
}
