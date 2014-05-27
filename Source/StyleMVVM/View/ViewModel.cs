using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grace.Logging;
using StyleMVVM.Ultilities;
using StyleMVVM.ViewModel;

#if NETFX_CORE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.View
{
	public class ViewModel
	{
		private static string supplementalString = "ViewModel";

		public static string GetName(DependencyObject obj)
		{
			return (string)obj.GetValue(NameProperty);
		}

		public static void SetName(DependencyObject obj, string value)
		{
			obj.SetValue(NameProperty, value);
		}

		// Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty NameProperty =
			 DependencyProperty.RegisterAttached("Name", typeof(string), typeof(ViewModel), new PropertyMetadata(null, NameChanged));


		private static async void NameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement)
			{
				if (DesignModeUtility.DesignModeIsEnabled)
				{
					DesignTimeBootstrapper.CreateDesignTimeBootstrapper(d as FrameworkElement);
				}

				IViewModelResolutionService resolutionService = 
					Bootstrapper.Instance.Container.Locate(typeof(IViewModelResolutionService)) as IViewModelResolutionService;

				if (resolutionService != null)
				{
					if (resolutionService.ResolveViewModel(d as FrameworkElement, e.NewValue as string) == false)
					{
						Logger.Error("Could not find ViewModel by the name of " + e.NewValue, supplementalString);
					}
				}
				else
				{
					Logger.Error("Could not find IViewModelResolutionService service", supplementalString);
				}
			}
			else
			{
				Logger.Error("ViewModel.Name can only be applied to FrameworkElement", supplementalString);
			}
		}

	}
}
