using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using StyleMVVM.Conventions.Binders;
using StyleMVVM.Conventions.Service;
using StyleMVVM.View;
using StyleMVVM.ViewModel;

namespace StyleMVVM.Conventions
{
	/// <summary>
	/// Add this composition root to the bootstrapper if you want to do Conventions
	/// </summary>
	public class ConventionsCompositionRoot : IConfigurationModule
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ConventionsCompositionRoot()
		{
			UseViewModelBinder = true;
		}

		/// <summary>
		/// Use the ViewModelBinder, if true the ViewModel will be inspected and bound to the View
		/// Set to false if you want to specify your own data bindings
		/// </summary>
		public bool UseViewModelBinder { get; set; }

		public void Configure(IExportRegistrationBlock registrationBlock)
		{
			registrationBlock.Export<ConventionsService>().
									As<IConventionsService>().
									AndSingleton();

			registrationBlock.Export<ConventionsViewBinder>().As<IViewBinder>();

			if (UseViewModelBinder)
			{
				registrationBlock.Export<ConventionsViewModelBinder>().As<IViewModelBinder>();
			}
		}
	}
}
