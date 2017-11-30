using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismExample.Modules.Person.ViewModels
{
    public class PersonListViewModel : BindableBase
    {
        readonly IRegionManager regionManager;

        public string Title => "People";

        private ObservableCollection<Common.Person> people;
        public ObservableCollection<Common.Person> People
        {
            get { return people; }
            set { SetProperty(ref people, value); }
        }

        public DelegateCommand<Common.Person> PersonSelectedCommand { get; private set; }

        public PersonListViewModel(RegionManager regionManager)
        {
            this.regionManager = regionManager;

            PersonSelectedCommand = new DelegateCommand<Common.Person>(PersonSelected);
            CreatePeople();
        }

        private void PersonSelected(Common.Person person)
        {
            var parameters = new NavigationParameters();
            parameters.Add("person", person);

            if (person != null)
                regionManager.RequestNavigate("PersonDetailsRegion", "PersonDetail", parameters);
        }

        private void CreatePeople()
        {
            var people = new ObservableCollection<Common.Person>();
            for (int i = 0; i < 10; i++)
            {
                people.Add(new Common.Person()
                {
                    FirstName = String.Format("First {0}", i),
                    LastName = String.Format("Last {0}", i),
                    Age = i
                });
            }

            People = people;
        }
    }
}
