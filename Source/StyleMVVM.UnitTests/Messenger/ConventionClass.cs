using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.UnitTests.Messenger;

namespace StyleMVVM.DotNet.UnitTests.Messenger
{
	public class ConventionClass
	{
		public int Test { get; set; }

		public void TestMessageHandler(int test)
		{
			Test = test;
		}

		public void TestClassMessageHandler(TestClass testClass)
		{
			Test = testClass.TestInt;
		}
	}
}
