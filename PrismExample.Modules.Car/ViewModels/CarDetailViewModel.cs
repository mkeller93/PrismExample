using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismExample.Modules.Car.ViewModels
{
    public class CarDetailViewModel : BindableBase, INavigationAware
    {
        private Domain.Car _selectedCar;
        public Domain.Car SelectedCar
        {
            get { return _selectedCar; }
            set { SetProperty(ref _selectedCar, value); }
        }

        public CarDetailViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["car"] is Domain.Car car)
                SelectedCar = car;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["car"] is Domain.Car car)
                return SelectedCar != null && SelectedCar.Make == car.Make;

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
