using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.Logging;

#if NET_PORTABLE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Media;
#endif

namespace StyleMVVM.ViewModel.Impl
{
	/// <summary>
	/// This class bind the Parents View's DataContext to the current ViewModel
	/// </summary>
	public class ViewModelParentDataContextBinder : IViewModelBinder
	{
		/// <summary>
		/// If the viewModel implements the IParentDataContextAwareViewModel interface then the Parent View's DataContext
		/// will be set to the ViewModel.ParentDataContext property at view load time
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void BindViewModelToView(FrameworkElement frameworkElement, object viewModel)
		{
			if (viewModel is IParentDataContextAwareViewModel)
			{
				frameworkElement.Loaded -= InjectParentDataContextOnload;

				frameworkElement.Loaded += InjectParentDataContextOnload;
			}
		}

		/// <summary>
		/// if the view model implements the IParentDataContextAwareViewModel interface then the parents view will be disconnected.
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void UnbindViewModelFromView(FrameworkElement frameworkElement, object viewModel)
		{
			if (viewModel is IParentDataContextAwareViewModel)
			{
				frameworkElement.Loaded -= InjectParentDataContextOnload;
			}
		}

		private static void InjectParentDataContextOnload(object sender, RoutedEventArgs routedEventArgs)
		{
			FrameworkElement fe = sender as FrameworkElement;

			if (fe != null)
			{
				try
				{
					IParentDataContextAwareViewModel model = fe.DataContext as IParentDataContextAwareViewModel;

					if (model != null)
					{
						DependencyObject parent = VisualTreeHelper.GetParent(fe);

						while (parent != null && !(parent is FrameworkElement))
						{
							parent = VisualTreeHelper.GetParent(parent);
						}

						FrameworkElement frameworkElementParent = parent as FrameworkElement;

						if (frameworkElementParent != null)
						{
							model.ParentDataContext = frameworkElementParent.DataContext;
						}
					}
				}
				catch (Exception exp)
				{
					Logger.Error("Exception thrown while injecting parent data context into view model", "ViewModel", exp);
				}
				finally
				{
					fe.Loaded -= InjectParentDataContextOnload;
				}
			}
		}
	}
}
