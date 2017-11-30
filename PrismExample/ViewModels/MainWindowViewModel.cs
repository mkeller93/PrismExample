using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace PrismExample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Unity Application";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public MainWindowViewModel()
        {
            
        }
    }
}
