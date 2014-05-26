using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection.Attributes;
using StyleMVVM.Messenger;

namespace StyleMVVM.UnitTests.Messenger
{
	[Export]
	public class TestClass
	{
		[MessageHandler]
		public void TestHandler(int test)
		{
			TestInt = test;
		}

		[MessageHandler(Token = "Multiply")]
		public void MultiplyHandler(int test)
		{
			TestInt = test * 10;
		}

		public int TestInt { get; set; }
	}
}
