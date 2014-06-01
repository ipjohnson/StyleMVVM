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
	/// <summary>
	/// Static class that will setup a bootstrapper at design time
	/// </summary>
	public static class DesignTimeBootstrapper
	{
		/// <summary>
		/// Gets the design time Export locator
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IExportLocator GetDesignTimeExportLocator(DependencyObject obj)
		{
			return (IExportLocator)obj.GetValue(DesignTimeExportLocatorProperty);
		}

		/// <summary>
		/// Sets the design time locator
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="value"></param>
		public static void SetDesignTimeExportLocator(DependencyObject obj, IExportLocator value)
		{
			obj.SetValue(DesignTimeExportLocatorProperty, value);
		}

		/// <summary>
		/// Design time locator
		/// </summary>
		public static readonly DependencyProperty DesignTimeExportLocatorProperty =
			 DependencyProperty.RegisterAttached("DesignTimeExportLocator", typeof(IExportLocator), typeof(DesignTimeBootstrapper), new PropertyMetadata(null));


		/// <summary>
		/// Creates a new Design time bootstrapper if DesignModeEnabled is true
		/// and there isn't a bootstrapper already
		/// </summary>
		public static IExportLocator GetDesignTimeExportLocatorInstance(FrameworkElement frameworkElement)
		{
			IExportLocator returnValue = GetDesignTimeExportLocator(frameworkElement);

			if (returnValue == null &&
				 DesignModeUtility.DesignModeIsEnabled)
			{
				if (!Bootstrapper.HasInstance)
				{
					try
					{
						IBootstrapper newBootStrapper = new Bootstrapper(ExportEnvironment.DesignTime);

						newBootStrapper.Start();
					}
					catch (Exception)
					{
					}
				}

				if (Application.Current != null)
				{
					List<Assembly> designTimeAssemblies = new List<Assembly>();
					List<IConfigurationModule> configurationModules = new List<IConfigurationModule>();

					FindAllDesignTimeObjects(Application.Current.Resources, designTimeAssemblies, configurationModules);

					returnValue = Bootstrapper.Instance.Container.CreateChildScope(
						c =>
						{
							foreach (Assembly designTimeAssembly in designTimeAssemblies)
							{
								var localAssembly = designTimeAssembly;

								c.ExportAssembly(localAssembly);
							}

							foreach (IConfigurationModule configurationModule in configurationModules)
							{
								configurationModule.Configure(c);
							}
						});

					SetDesignTimeExportLocator(frameworkElement, returnValue);
				}
			}

			return returnValue;
		}

		private static void FindAllDesignTimeObjects(ResourceDictionary resourceDictionary,
																	List<Assembly> designTimeAssemblies,
																	List<IConfigurationModule> configurationModules)
		{
			foreach (ResourceDictionary mergedDictionary in resourceDictionary.MergedDictionaries)
			{
				FindAllDesignTimeObjects(mergedDictionary, designTimeAssemblies, configurationModules);
			}

			foreach (KeyValuePair<object, object> keyValuePair in resourceDictionary)
			{
				string stringKey = keyValuePair.Key as string;

				IConfigurationModule configurationModule = keyValuePair.Value as IConfigurationModule;

				if (configurationModule != null)
				{
					configurationModules.Add(configurationModule);
				}
				else if (stringKey != null && stringKey.ToLower().StartsWith("designtimeresource"))
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
