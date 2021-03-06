﻿using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PrismExample.Shell.Infrastructure.Commands;
using PrismExample.Shell.Infrastructure.ModuleCatalogs;

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
