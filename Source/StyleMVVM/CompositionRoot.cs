using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.ViewModel;
using StyleMVVM.ViewModel.Impl;

namespace StyleMVVM
{
	public class CompositionRoot : IConfigurationModule
	{
		public void Configure(IExportRegistrationBlock registrationBlock)
		{
			SetupValidation(registrationBlock);

			SetupViewModelService(registrationBlock);

			SetupViewService(registrationBlock);
		}

		private void SetupViewModelService(IExportRegistrationBlock registrationBlock)
		{
			registrationBlock.Export<ViewModelResolutionService>().
									As<IViewModelResolutionService>().AndSingleton();

			registrationBlock.Export<ViewModelDataContextBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelLoadedBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelNavigationBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelParentDataContextBinder>().As<IViewModelBinder>();
			registrationBlock.Export<ViewModelViewAwareBinder>().As<IViewModelBinder>();
		}

		private void SetupViewService(IExportRegistrationBlock registrationBlock)
		{
			
		}

		private void SetupValidation(IExportRegistrationBlock registrationBlock)
		{
			
		}
	}
}
