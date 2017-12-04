using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using PrismExample.Infrastructure.Interface;
using PrismExample.Infrastructure.Service;
using PrismExample.Modules.Car.Views;

namespace PrismExample.Modules.Car
{
    public class CarModule : IModule
    {
        readonly IRegionManager regionManager;
        readonly IUnityContainer container;

        public CarModule(RegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<ICarService, CarService>();

            regionManager.RegisterViewWithRegion(Shell.Infrastructure.RegionNames.Content, typeof(CarList));
            container.RegisterTypeForNavigation<CarDetail>();
        }
    }
}
