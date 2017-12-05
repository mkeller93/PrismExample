using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace PrismExample.Shell.Infrastructure.Commands
{
    public class ApplicationCommandRegistry : IApplicationCommandRegistry
    {
        public List<ApplicationCommand> Commands { get; } = new List<ApplicationCommand>();

        public event EventHandler Updated;

        private readonly IUnityContainer container;

        public ApplicationCommandRegistry(IUnityContainer container)
        {
            this.container = container;
        }

        public void Register<T>(string menu, string submenu, string region)
        {
            var menuItem = Commands.SingleOrDefault(x => x.Header == menu);

            if (menuItem == null)
            {
                menuItem = container.Resolve<ApplicationCommand>();
                menuItem.Header = menu;
                Commands.Add(menuItem);
            }

            var subMenuItem = menuItem.Commands.SingleOrDefault(x => x.Header == submenu);

            if (subMenuItem == null)
            {
                subMenuItem = container.Resolve<ApplicationCommand>();
                subMenuItem.Header = submenu;

                subMenuItem.Region = region;
                subMenuItem.ViewType = typeof(T);

                menuItem.Commands.Add(subMenuItem);
            }

            Updated?.Invoke(this, null);
        }
    }
}
