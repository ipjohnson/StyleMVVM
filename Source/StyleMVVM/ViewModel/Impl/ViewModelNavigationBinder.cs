using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grace.Logging;
using StyleMVVM.View;

#if NET_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
#else
using System.Windows;
using System.Windows.Navigation;
#endif

namespace StyleMVVM.ViewModel.Impl
{
	/// <summary>
	/// This class binds the navigation events of the view to the viewmodels Navigation handlers
	/// </summary>
	public class ViewModelNavigationBinder : IViewModelBinder
	{
		private static string supplementalString = "ViewModelNavigationBinder";

		/// <summary>
		/// If the view implements INavigatingPage and the ViewModel implements the INavigationViewModel interface
		/// then event handlers will be hooked up to the View's navigations events
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void BindViewModelToView(FrameworkElement frameworkElement, object viewModel)
		{
			if (viewModel is INavigationViewModel && frameworkElement is INavigatingPage)
			{
				INavigatingPage page = frameworkElement as INavigatingPage;

				page.NavigatedFrom -= NavigationViewModelNavigatedFrom;
				page.NavigatedTo -= NavigationViewModelNavigatedTo;
				page.NavigatingFrom -= NavigationViewModelNavigatingFrom;

				page.NavigatedFrom += NavigationViewModelNavigatedFrom;
				page.NavigatedTo += NavigationViewModelNavigatedTo;
				page.NavigatingFrom += NavigationViewModelNavigatingFrom;
			}
		}

		/// <summary>
		/// If the view implements INavigatingPage and the ViewModel implements the INavigationViewModel interface
		/// then event handlers will be removed from the View's navigations events
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void UnbindViewModelFromView(FrameworkElement frameworkElement, object viewModel)
		{
			if (viewModel is INavigationViewModel && frameworkElement is INavigatingPage)
			{
				INavigatingPage page = frameworkElement as INavigatingPage;

				page.NavigatedFrom -= NavigationViewModelNavigatedFrom;
				page.NavigatedTo -= NavigationViewModelNavigatedTo;
				page.NavigatingFrom -= NavigationViewModelNavigatingFrom;
			}
		}

		#region Navigation Handlers

		private static async void NavigationViewModelNavigatingFrom(object sender, NavigatingCancelEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;

			if (frameworkElement != null)
			{
				INavigationViewModel viewModel = frameworkElement.DataContext as INavigationViewModel;

				if (viewModel != null)
				{
					try
					{
						await viewModel.NavigatingFrom(sender, new StyleNavigatingCancelEventArgs(e));
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while calling NavigatingFrom", supplementalString, exp);
					}
				}
			}
		}

		private static void NavigationViewModelNavigatedTo(object sender, NavigationEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;

			if (frameworkElement != null)
			{
				INavigationViewModel viewModel = frameworkElement.DataContext as INavigationViewModel;

				if (viewModel != null)
				{
					try
					{
						viewModel.NavigatedTo(sender, new StyleNavigationEventArgs(e));
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while calling NavigatedTo", supplementalString, exp);
					}
				}
			}
		}

		private static void NavigationViewModelNavigatedFrom(object sender, NavigationEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;

			if (frameworkElement != null)
			{
				INavigationViewModel viewModel = frameworkElement.DataContext as INavigationViewModel;

				if (viewModel != null)
				{
					try
					{
						viewModel.NavigatedFrom(sender, new StyleNavigationEventArgs(e));
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while calling NavigatedFrom", supplementalString, exp);
					}
				}
			}
		}

		#endregion
	}
}
