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
	/// This class binds the ViewModel to the View.Data context property
	/// </summary>
	public class ViewModelDataContextBinder : IViewModelBinder
	{
		/// <summary>
		/// Bind the ViewModel to the View's DataContext
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void BindViewModelToView(FrameworkElement frameworkElement, object viewModel)
		{
			frameworkElement.DataContext = viewModel;
		}

		/// <summary>
		/// Remove the ViewModel from the View's DataContext
		/// </summary>
		/// <param name="frameworkElement"></param>
		/// <param name="viewModel"></param>
		public void UnbindViewModelFromView(FrameworkElement frameworkElement, object viewModel)
		{
			if (frameworkElement.DataContext == viewModel)
			{
				frameworkElement.DataContext = null;
			}
		}
	}
}
