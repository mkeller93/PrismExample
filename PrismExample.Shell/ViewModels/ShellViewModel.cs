using Prism.Mvvm;

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

        public ShellViewModel()
        {
            
        }
    }
}
