using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.Services;

namespace StyleMVVM
{
	/// <summary>
	/// The Bootstrapper contains the dependency injection container for the application as well as starts up all services
	/// The bootstrapper has 3 phases of existence.
	/// 1) Construction - Container is created and default implementations are loaded
	/// 2) Startup - All classes have been registered in the DI container
	/// 3) Launched - The app is ready for UI Components like Charm service to be initialized
	/// </summary>
	public class Bootstrapper : IBootstrapper
	{
		private volatile static IBootstrapper instance;
		private static readonly object instanceLock = new object();

		/// <summary>
		/// Default bootstrapper
		/// </summary>
		/// <param name="exportEnvironment"></param>
		public Bootstrapper(ExportEnvironment exportEnvironment = ExportEnvironment.RunTime)
		{
			Initialize(exportEnvironment);

			Instance = this;
		}

		/// <summary>
		/// Dependency injection container for this bootstrapper
		/// </summary>
		public IDependencyInjectionContainer Container { get; private set; }

		/// <summary>
		/// Configure container
		/// </summary>
		/// <param name="registrationBlock">registration block</param>
		public void Configure(ExportRegistrationDelegate registrationBlock)
		{
			Container.Configure(registrationBlock);
		}

		/// <summary>
		/// Configure container
		/// </summary>
		/// <param name="configurationModule"></param>
		public void Configure(IConfigurationModule configurationModule)
		{
			Container.Configure(configurationModule);
		}

		/// <summary>
		/// Start bootstrapper
		/// </summary>
		public void Start()
		{
			Container.LocateAllWithMetadata<object>(MetadataConstants.ServiceStartupBeforeLaunch, true);
		}

		/// <summary>
		/// Launch bootstrapper
		/// </summary>
		public void Launched()
		{
			Container.LocateAllWithMetadata<object>(MetadataConstants.ServiceStartupAfterLaunch);
		}

		/// <summary>
		/// Shutdown bootstrapper
		/// </summary>
		public void Shutdown()
		{
			Container.Dispose();

			Container = null;
		}

		/// <summary>
		/// Global bootstrapper for the application
		/// </summary>
		public static IBootstrapper Instance
		{
			get
			{
				if (instance == null)
				{
					lock (instanceLock)
					{
						if (instance == null)
						{
							instance = new Bootstrapper();
						}
					}
				}

				return instance;
			}
			set
			{
				if (instance != null)
				{
					return;
				}

				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = value;
					}
				}
			}
		}

		/// <summary>
		/// Is there an Instance created
		/// </summary>
		public static bool HasInstance
		{
			get { return instance != null; }
		}

		/// <summary>
		/// Initialize the bootstrapper
		/// </summary>
		/// <param name="exportEnvironment">export environment</param>
		private void Initialize(ExportEnvironment exportEnvironment)
		{
			Container = new DependencyInjectionContainer(exportEnvironment);

			Container.Configure(new CompositionRoot());

			Instance = this;
		}
	}
}
