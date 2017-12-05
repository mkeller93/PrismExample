using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PrismExample.Shell.Infrastructure.Commands;
using PrismExample.Shell.Infrastructure.ModuleCatalogs;
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

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new SubdirectoryModuleCatalog{ModulePath = ".\\Modules"};
        }
        
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ApplicationCommand>();

            Container.RegisterType<IApplicationCommandRegistry, ApplicationCommandRegistry>(new ContainerControlledLifetimeManager());
        }
    }
}
