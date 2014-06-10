using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.Ultilities;
#if NETFX_CORE
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.View
{
	/// <summary>
	/// Conventions binding at design time
	/// </summary>
	public class Conventions
	{
		/// <summary>
		/// Bind at design time
		/// </summary>
		/// <param name="obj">dependency object</param>
		/// <returns></returns>
		public static bool GetBindAtDesignTime(DependencyObject obj)
		{
			return (bool)obj.GetValue(BindAtDesignTimeProperty);
		}

		/// <summary>
		/// Sets the bind at design time property
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="value"></param>
		public static void SetBindAtDesignTime(DependencyObject obj, bool value)
		{
			obj.SetValue(BindAtDesignTimeProperty, value);
		}

		/// <summary>
		/// Bind the viewmodel to the view at design time
		/// </summary>
		public static readonly DependencyProperty BindAtDesignTimeProperty =
			 DependencyProperty.RegisterAttached("BindAtDesignTime", typeof(bool), typeof(Conventions), new PropertyMetadata(false, BindAtDesignTimeChanged));

		/// <summary>
		/// Bind the view model to the view at design time
		/// </summary>
		/// <param name="d">view to bind</param>
		/// <param name="e">property change event</param>
		private static void BindAtDesignTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool designTime = (bool)e.NewValue;
			FrameworkElement element = d as FrameworkElement;

			if (element == null || !DesignModeUtility.DesignModeIsEnabled)
			{
				return;
			}

			if (designTime)
			{
				IExportLocator exportLocator = DesignTimeBootstrapper.GetDesignTimeExportLocatorInstance(element);

				IViewBinderService viewBinderService = exportLocator.Locate<IViewBinderService>();

				if (viewBinderService != null)
				{
					viewBinderService.Bind(element, null);
				}
				else
				{
					throw new Exception("Could not locate IViewBinderService");
				}
			}
			else
			{
				element.DataContext = null;
			}
		}
	}
}
