using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using PrismExample.Shell.Infrastructure.Commands;

namespace PrismExample.Shell.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string title = "Prism Unity Application";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public List<ApplicationCommand> Commands => registry.Commands.ToList();

        private readonly IApplicationCommandRegistry registry;

        public ShellViewModel(IApplicationCommandRegistry registry)
        {
            this.registry = registry;

            this.registry.Updated += (s, e) => RaisePropertyChanged(nameof(Commands));
        }
    }
}
