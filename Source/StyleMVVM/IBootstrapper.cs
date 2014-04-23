using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;

namespace StyleMVVM
{
	/// <summary>
	/// This is the base functionality all bootstrappers have to implement
	/// Start/Stop & have a container
	/// </summary>
	public interface IBootstrapper
	{
		/// <summary>
		/// The default DI Container for the bootstrapper
		/// </summary>
		IDependencyInjectionContainer Container { get; set; }

		/// <summary>
		/// starts the bootstrapper and container
		/// </summary>
		void Start();

		/// <summary>
		/// starts the bootstrapper and container
		/// </summary>
		void Start(bool launchUI);

		/// <summary>
		/// Called when the App is launched
		/// </summary>
		void Launched();

		/// <summary>
		/// shuts down the boot strapper and container
		/// </summary>
		void Shutdown();

		bool IsReady { get; }
	}
}
