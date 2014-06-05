using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleMVVM.ViewModel;

#if NETFX_CORE
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.Conventions.Binders
{
	/// <summary>
	/// Conventions ViewModelBinder
	/// </summary>
	public class ConventionsViewModelBinder : IViewModelBinder
	{
		public void BindViewModelToView(FrameworkElement frameworkElement, object viewModel)
		{
			
		}

		public void UnbindViewModelFromView(FrameworkElement frameworkElement, object viewModel)
		{

		}
	}
}
