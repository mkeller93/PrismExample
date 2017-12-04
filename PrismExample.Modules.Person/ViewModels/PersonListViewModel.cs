using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismExample.Infrastructure.Interface;

namespace PrismExample.Modules.Person.ViewModels
{
    public class PersonListViewModel : BindableBase
    {
        private IPersonService personService;
        private readonly IRegionManager regionManager;

        public string Title => "People";

        private ObservableCollection<Domain.Person> people;
        public ObservableCollection<Domain.Person> People
        {
            get { return people; }
            set { SetProperty(ref people, value); }
        }

        public DelegateCommand<Domain.Person> PersonSelectedCommand { get; private set; }

        public PersonListViewModel(RegionManager regionManager, IPersonService personService)
        {
            this.personService = personService;
            this.regionManager = regionManager;

            PersonSelectedCommand = new DelegateCommand<Domain.Person>(PersonSelected);
            CreatePeople();
        }

        private void PersonSelected(Domain.Person person)
        {
            var parameters = new NavigationParameters();
            parameters.Add("person", person);

            if (person != null)
                regionManager.RequestNavigate(RegionNames.PersonDetail, NavigationTargets.PersonDetail, parameters);
        }

        private void CreatePeople()
        {
            People = new ObservableCollection<Domain.Person>(personService.GetPersons());
        }
    }
}
