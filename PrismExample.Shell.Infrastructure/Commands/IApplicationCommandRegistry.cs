using System;
using System.Collections.Generic;

namespace PrismExample.Shell.Infrastructure.Commands
{
    public interface IApplicationCommandRegistry
    {
        List<ApplicationCommand> Commands { get; }

        void Register<T>(string menu, string submenu, string region);

        event EventHandler Updated;
    }
}
