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
using PrismExample.Modules.Person.Views;
using PrismExample.Shell.Infrastructure.Commands;

namespace PrismExample.Modules.Person
{
    public class PersonModule : IModule
    {
        readonly IUnityContainer container;
        private readonly IApplicationCommandRegistry registry;

        public PersonModule(IUnityContainer container, IApplicationCommandRegistry registry)
        {
            this.container = container;
            this.registry = registry;
        }

        public void Initialize()
        {
            container.RegisterType<IPersonService, PersonService>();

            registry.Register<PersonList>("Person", "List", Shell.Infrastructure.RegionNames.Content);

            container.RegisterTypeForNavigation<PersonDetail>();
        }
    }
}
