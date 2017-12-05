using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Regions;

namespace PrismExample.Shell.Infrastructure.Commands
{
    public class ApplicationCommand
    {
        public List<ApplicationCommand> Commands { get; } = new List<ApplicationCommand>();

        public DelegateCommand DelegateCommand { get; }

        public string Header { get; set; }

        public string Region { get; set; }
        public Type ViewType { get; set; }

        private readonly IRegionManager regionManager;

        public ApplicationCommand(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            DelegateCommand = new DelegateCommand(RunCommand);
        }

        private void RunCommand()
        {
            if (regionManager.Regions[Region].Views.All(v => v.GetType() != ViewType))
            {
                
                regionManager.RegisterViewWithRegion(Region, ViewType);
            }

            var view = regionManager.Regions[Region].Views.First(v => v.GetType() == ViewType);
            regionManager.Regions[Region].Activate(view);
        }
    }
}
