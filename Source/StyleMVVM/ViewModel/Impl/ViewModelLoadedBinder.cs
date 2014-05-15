using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grace.Logging;

#if NET_PORTABLE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif


namespace StyleMVVM.ViewModel.Impl
{
	/// <summary>
	/// This class wires the Loaded & Unloaded events on the View to the ViewModel
	/// </summary>
	public class ViewModelLoadedBinder : IViewModelBinder
	{
		private static string supplementalString = "ViewModelLoadedBinder";

		/// <summary>
		/// If the viewmodel implements ILoadedAwareViewModel interfacce then event handlers are connected to the View 
		/// and fire the Loaded and Unloaded handlers on the ViewModel
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void BindViewModelToView(FrameworkElement frameworkElement, object viewModel)
		{
			if (viewModel is ILoadedAwareViewModel)
			{
				frameworkElement.Loaded -= ViewModelOnLoaded;
				frameworkElement.Unloaded -= ViewModelOnUnloaded;

				frameworkElement.Loaded += ViewModelOnLoaded;
				frameworkElement.Unloaded += ViewModelOnUnloaded;
			}
		}

		public void UnbindViewModelFromView(FrameworkElement frameworkElement, object viewModel)
		{
			if (viewModel is ILoadedAwareViewModel)
			{
				frameworkElement.Loaded -= ViewModelOnLoaded;
				frameworkElement.Unloaded -= ViewModelOnUnloaded;
			}
		}

		#region OnLoaded / Unloaded

		private static void ViewModelOnUnloaded(object sender, RoutedEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;

			if (frameworkElement != null)
			{
				ILoadedAwareViewModel viewModel = frameworkElement.DataContext as ILoadedAwareViewModel;

				if (viewModel != null)
				{
					try
					{
						viewModel.OnUnloaded(sender, e);
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while calling OnUnloaded", supplementalString, exp);
					}
				}
			}
		}

		private static void ViewModelOnLoaded(object sender, RoutedEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;

			if (frameworkElement != null)
			{
				ILoadedAwareViewModel viewModel = frameworkElement.DataContext as ILoadedAwareViewModel;

				if (viewModel != null)
				{
					try
					{
						viewModel.OnLoaded(sender, e);
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while calling OnLoaded", supplementalString, exp);
					}
				}
			}
		}

		#endregion
	}
}
