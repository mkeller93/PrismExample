using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PrismExample.Shell.Infrastructure.Commands;
using PrismExample.Shell.Views;

namespace PrismExample.Shell
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(Modules.Person.PersonModule));
            catalog.AddModule(typeof(Modules.Car.CarModule));            
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ApplicationCommand>();

            Container.RegisterType<IApplicationCommandRegistry, ApplicationCommandRegistry>(new ContainerControlledLifetimeManager());
        }
    }
}
