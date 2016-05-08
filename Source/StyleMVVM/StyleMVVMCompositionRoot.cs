using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.Messenger;
using StyleMVVM.View;
using StyleMVVM.View.Impl;
using StyleMVVM.ViewModel;
using StyleMVVM.ViewModel.Impl;

#if NETFX_CORE
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace StyleMVVM
{
	/// <summary>
	/// Composition root for StyleMVVM framework
	/// </summary>
	public class StyleMVVMCompositionRoot : IConfigurationModule
	{
		/// <summary>
		/// Configure the container for StyleMVVM
		/// </summary>
		/// <param name="registrationBlock">registration block</param>
		public void Configure(IExportRegistrationBlock registrationBlock)
		{
			SetupDispatchedMessenger(registrationBlock);

			SetupValidation(registrationBlock);

			SetupViewModelService(registrationBlock);

			SetupViewService(registrationBlock);
		}

		private void SetupViewModelService(IExportRegistrationBlock registrationBlock)
		{
			registrationBlock.Export<ViewModelResolutionService>().
							  As<IViewModelResolutionService>().
                              Lifestyle.Singleton();

			registrationBlock.Export<ViewModelDataContextBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelLoadedBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelNavigationBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelParentDataContextBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelViewAwareBinder>().As<IViewModelBinder>();
		}

		private void SetupDispatchedMessenger(IExportRegistrationBlock registrationBlock)
		{
			registrationBlock.Export<DispatchedMessenger>().
							  As<IDispatchedMessenger>().
                              Lifestyle.Singleton();
		}

		private void SetupViewService(IExportRegistrationBlock registrationBlock)
		{
			registrationBlock.Export<NavigationService>().
									As<INavigationService>().
									WithCtorParam<Frame>().IsRequired(false);

			registrationBlock.Export<FilePickerService>().
									As<IFilePickerService>().
                                    Lifestyle.Singleton();

#if !NETFX_CORE
			registrationBlock.Export<PickerLocationIdTranslator>().
									As<IPickerLocationIdTranslator>().
                                    Lifestyle.Singleton();
#endif
		}

		private void SetupValidation(IExportRegistrationBlock registrationBlock)
		{
			
		}
	}
}
