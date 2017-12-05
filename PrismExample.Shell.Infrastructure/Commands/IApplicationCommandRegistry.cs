using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismExample.Shell.Infrastructure.Commands
{
    public interface IApplicationCommandRegistry
    {
        List<ApplicationCommand> Commands { get; }

        void Register<T>(string menu, string submenu, string region);

        event EventHandler Updated;
    }
}
