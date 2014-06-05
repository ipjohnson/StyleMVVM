using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.Conventions.Service;
using StyleMVVM.View;
using StyleMVVM.ViewModel;

#if NETFX_CORE
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.Conventions.Binders
{
	/// <summary>
	/// Bind a View 
	/// </summary>
	public class ConventionsViewBinder : IViewBinder
	{
		private readonly IConventionsService conventionsService;
		private readonly IViewModelResolutionService viewModelResolutionService;
		private readonly IExportLocator locator;

		/// <summary>
		/// Default Constructor
		/// </summary>
		/// <param name="conventionsService">conventions service</param>
		/// <param name="viewModelResolutionService">view model resolution service</param>
		/// <param name="locator">locator</param>
		public ConventionsViewBinder(IConventionsService conventionsService,
											IViewModelResolutionService viewModelResolutionService,
											IExportLocator locator)
		{
			this.conventionsService = conventionsService;
			this.viewModelResolutionService = viewModelResolutionService;
			this.locator = locator;
		}

		/// <summary>
		/// Bind View
		/// </summary>
		/// <param name="view">View to bind</param>
		/// <param name="navigationParameter">navigation parameter</param>
		public void Bind(object view, object navigationParameter)
		{
			FrameworkElement frameworkElement = view as FrameworkElement;

			if (frameworkElement != null && !HasDataContext(frameworkElement))
			{
				IEnumerable<string> viewModelNames = conventionsService.ViewModelNameConventions(view.GetType());

				foreach (string viewModelName in viewModelNames)
				{
					if (viewModelResolutionService.ResolveViewModel(locator, frameworkElement, viewModelName))
					{
						break;
					}
				}
			}
		}

		private bool HasDataContext(FrameworkElement frameworkElement)
		{
			object newValue = frameworkElement.ReadLocalValue(FrameworkElement.DataContextProperty);

			return newValue != DependencyProperty.UnsetValue;
		}
	}
}
