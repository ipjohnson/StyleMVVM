using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Grace.DependencyInjection;
using Grace.Logging;
using StyleMVVM.Ultilities;

#if NETFX_CORE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM
{
	public static class DesignTimeBootstrapper
	{
		/// <summary>
		/// Creates a new Design time bootstrapper if DesignModeEnabled is true
		/// and there isn't a bootstrapper already
		/// </summary>
		public static void CreateDesignTimeBootstrapper(FrameworkElement frameworkElement)
		{
			if (DesignModeUtility.DesignModeIsEnabled)
			{
				if (!Bootstrapper.HasInstance)
				{
					try
					{
						IBootstrapper newBootStrapper = new Bootstrapper(ExportEnvironment.DesignTime);

						object testResource = null;

						if (Application.Current != null)
						{
#if DOT_NET
							foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
							{
								newBootStrapper.Container.RegisterAssembly(assembly);
							}

							AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomainOnAssemblyLoad;

							AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;

#else
							List<Assembly> designTimeAssemblies = new List<Assembly>();

							FindAllDesignTimeObjects(Application.Current.Resources, designTimeAssemblies);

							foreach (Assembly designTimeAssembly in designTimeAssemblies)
							{
								var localAssembly = designTimeAssembly;

								newBootStrapper.Container.Configure(c => c.ExportAssembly(localAssembly));
							}

#endif
						}

						newBootStrapper.Start();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private static void FindAllDesignTimeObjects(ResourceDictionary resourceDictionary,
																	List<Assembly> designTimeAssemblies)
		{
			foreach (ResourceDictionary mergedDictionary in resourceDictionary.MergedDictionaries)
			{
				FindAllDesignTimeObjects(mergedDictionary, designTimeAssemblies);
			}

			foreach (KeyValuePair<object, object> keyValuePair in resourceDictionary)
			{
				string stringKey = keyValuePair.Key as string;

				if (stringKey != null && stringKey.ToLower().StartsWith("designtimeresource"))
				{
					Logger.Debug("Found Key: " + keyValuePair.Key + " type " + keyValuePair.Key.GetType().FullName);

					Assembly designTimeAssembly = keyValuePair.Value.GetType().GetTypeInfo().Assembly;

					if (!designTimeAssemblies.Contains(designTimeAssembly))
					{
						designTimeAssemblies.Add(designTimeAssembly);
					}
				}
			}
		}

#if DOT_NET
		private static void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			var container = Bootstrapper.Instance.Container;
			var exports = container.GetAllStrategies();

			foreach (IExportStrategy exportStrategy in exports)
			{
				if (exportStrategy.ActivationType != null)
				{
					if (exportStrategy.ActivationType.Assembly.FullName == args.LoadedAssembly.FullName)
					{
						container.RemoveStrategy(exportStrategy);
					}
				}
			}

			container.RegisterAssembly(args.LoadedAssembly);

			container.ProcessRegistration();
		}
#endif
	}
}
