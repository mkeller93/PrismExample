using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using PrismExample.Modules.Person.Views;

namespace PrismExample.Modules.Person
{
    public class PersonModule : IModule
    {
        readonly IRegionManager regionManager;
        readonly IUnityContainer container;

        public PersonModule(RegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonList));
            container.RegisterTypeForNavigation<PersonDetail>();
        }
    }
}
