using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;

namespace StyleMVVM
{
	public interface IBootstrapper
	{
		/// <summary>
		/// The default DI Container for the bootstrapper
		/// </summary>
		IDependencyInjectionContainer Container { get; }

		/// <summary>
		/// Configure the bootstrappers DI
		/// </summary>
		/// <param name="registrationDelegate">registration delegate</param>
		void Configure(ExportRegistrationDelegate registrationDelegate);

		/// <summary>
		/// Configure the bootstrappers DI
		/// </summary>
		/// <param name="configurationModule">new configuration module</param>
		void Configure(IConfigurationModule configurationModule);

		/// <summary>
		/// Call this after you've configured the container but before launching
		/// </summary>
		void Start();

		/// <summary>
		/// Called when the App is launched
		/// </summary>
		void Launched();

		/// <summary>
		/// shuts down the boot strapper and container
		/// </summary>
		void Shutdown();
	}
}
