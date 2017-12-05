using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PrismExample.Infrastructure.Interface;
using PrismExample.Infrastructure.Service;
using PrismExample.Modules.Car.Views;
using PrismExample.Shell.Infrastructure.Commands;

namespace PrismExample.Modules.Car
{
    public class CarModule : IModule
    {
        readonly IUnityContainer container;
        private readonly IApplicationCommandRegistry registry;

        public CarModule(IUnityContainer container, IApplicationCommandRegistry registry)
        {
            this.container = container;
            this.registry = registry;
        }

        public void Initialize()
        {
            container.RegisterType<ICarService, CarService>();

            registry.Register<CarList>("Car", "List", Shell.Infrastructure.RegionNames.Content);
            
            container.RegisterTypeForNavigation<CarDetail>();
        }
    }
}
