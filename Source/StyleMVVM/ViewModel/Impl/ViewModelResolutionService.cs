using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using Grace.Logging;
using StyleMVVM.View;
using StyleMVVM.View.Impl;

#if NETFX_CORE
using Windows.ApplicationModel;
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace StyleMVVM.ViewModel.Impl
{
	/// <summary>
	/// This class is responsible or resolving and binding all viewmodels
	/// </summary>
	public class ViewModelResolutionService : IViewModelResolutionService
	{
		private const string SUPPLEMENTAL_STRING = "ViewModelResolutionService";
		private readonly IEnumerable<IViewModelBinder> binders;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="injectionScope">injection scope</param>
		/// <param name="binderCollection">binder collection</param>
		public ViewModelResolutionService(IEnumerable<IViewModelBinder> binderCollection)
		{
			binders = binderCollection;
		}

		/// <summary>
		/// Creates and Binds a viewmodel to a view
		/// </summary>
		/// <param name="injectionScope"></param>
		/// <param name="view">view to bind to</param>
		/// <param name="viewModelName">name of the view model to bind to</param>
		public bool ResolveViewModel(IExportLocator injectionScope, FrameworkElement view, string viewModelName)
		{
			bool foundModel = false;
			object viewModel = null;
			object oldViewModel = view.DataContext;

			if (!string.IsNullOrEmpty(viewModelName))
			{
				try
				{
					IInjectionContext injectionContext = injectionScope.CreateContext();
					IViewContext viewContext = new ViewContext(view);

					injectionContext.Export((s, c) => viewContext);

					viewModel = injectionScope.Locate(viewModelName, injectionContext);

					if (viewModel != null)
					{
						foundModel = true;

						Logger.Debug(string.Format("Resolved ViewModel Name {0} to Type {1}", viewModelName, viewModel.GetType().FullName));
					}
				}
				catch (Exception exp)
				{
					Logger.Error("Exception thrown while creating ViewModel: " + viewModelName, SUPPLEMENTAL_STRING, exp);
				}
			}

			if (foundModel)
			{
				ProcessNewModel(view, viewModel);

				ProcessOldModel(view, oldViewModel);
			}

			return foundModel;
		}

		/// <summary>
		/// Creates and Binds a viewmodel to a view
		/// </summary>
		/// <param name="injectionScope">injection scope to resolve from</param>
		/// <param name="view">view to bind to</param>
		/// <param name="viewModelType">type of the view model to bind to</param>
		public void ResolveViewModel(IExportLocator injectionScope, FrameworkElement view, Type viewModelType)
		{
			object viewModel = null;
			object oldViewModel = view.DataContext;

			if (viewModelType != null)
			{
				try
				{
					viewModel = injectionScope.Locate(viewModelType);

					if (viewModel == null)
					{
						Logger.Error("Could not locate exported ViewModel of type: " + viewModelType.FullName, SUPPLEMENTAL_STRING);
					}
					else
					{
						Logger.Debug(string.Format("Resolved ViewModel Type {0} to Type {1}",
															viewModelType.FullName,
															viewModel.GetType().FullName));
					}
				}
				catch (Exception exp)
				{
					Logger.Error("Exception thrown while creating ViewModel: " + viewModelType.FullName, SUPPLEMENTAL_STRING, exp);
				}
			}

			ProcessNewModel(view, viewModel);

			ProcessOldModel(view, oldViewModel);
		}

		private void ProcessNewModel(FrameworkElement view, object newViewModel)
		{
			if (newViewModel != null)
			{
				foreach (IViewModelBinder modelBinder in binders)
				{
					try
					{
						modelBinder.BindViewModelToView(view, newViewModel);
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while binding model", SUPPLEMENTAL_STRING, exp);
					}
				}
			}
		}

		private void ProcessOldModel(FrameworkElement view, object oldViewModel)
		{
			if (oldViewModel != null)
			{
				foreach (IViewModelBinder modelBinder in binders)
				{
					try
					{
						modelBinder.UnbindViewModelFromView(view, oldViewModel);
					}
					catch (Exception exp)
					{
						Logger.Error("Exception thrown while unbinding model", SUPPLEMENTAL_STRING, exp);
					}
				}
			}
		}
	}
}
