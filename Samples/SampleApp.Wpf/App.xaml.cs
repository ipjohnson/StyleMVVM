using System;
using System.Collections.Generic;
using System.Windows;
using AutoMapper;
using Grace.DependencyInjection;
using Samples.Wpf.Model;
using Samples.Wpf.ViewModels;
using StyleMVVM;

namespace Samples.Wpf
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper.Instance.Configure(new CompositionRoot());

            Mapper.Configuration.ConstructServicesUsing(type => Bootstrapper.Instance.Container.Locate(type));
            Mapper.CreateMap<Person, PersonViewModel>()
                .ConstructUsingServiceLocator();

            Mapper.CreateMap<House, HouseViewModel>();
        }
    }

    public class CompositionRoot : IConfigurationModule
    {
        public void Configure(IExportRegistrationBlock registrationBlock)
        {
            var allTypes = this.AllTypes();

            registrationBlock.Export(allTypes).
                ByName().
                Select(TypesThat.EndWith("ViewModel"));

            registrationBlock.Export(allTypes).
                ByInterfaces().
                Select(TypesThat.EndWith("Service")).
                AndSingleton();

            registrationBlock.Export(allTypes)
                .ByInterfaces()
                .Select(TypesThat.EndWith("Provider"))
                .AndSingleton();
        }
        private List<Type> AllTypes()
        {
            return new List<Type>(this.GetType().Assembly.ExportedTypes);
        }
    }
}
