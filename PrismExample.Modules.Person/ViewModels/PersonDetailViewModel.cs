using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismExample.Modules.Person.ViewModels
{
    public class PersonDetailViewModel : BindableBase, INavigationAware
    {
        private Domain.Person _selectedPerson;
        public Domain.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        public PersonDetailViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["person"] is Domain.Person person)
                SelectedPerson = person;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["person"] is Domain.Person person)
                return SelectedPerson != null && SelectedPerson.LastName == person.LastName;

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
