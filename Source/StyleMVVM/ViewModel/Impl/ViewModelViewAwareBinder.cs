using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET_PORTABLE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.ViewModel.Impl
{
	/// <summary>
	/// This class binds the View property for IViewAware interface when implemented in a ViewModel
	/// </summary>
	public class ViewModelViewAwareBinder : IViewModelBinder
	{
		/// <summary>
		/// If the viewmodel implements IViewAware then the View property is set.
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void BindViewModelToView(FrameworkElement frameworkElement, object viewModel)
		{
			IViewAware viewAware = viewModel as IViewAware;

			if (viewAware != null)
			{
				viewAware.View = frameworkElement;
			}
		}

		/// <summary>
		/// If the view model implements IViewAware then the View property is unset
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void UnbindViewModelFromView(FrameworkElement frameworkElement, object viewModel)
		{
			IViewAware viewAware = viewModel as IViewAware;

			if (viewAware != null)
			{
				viewAware.View = null;
			}
		}
	}
}
