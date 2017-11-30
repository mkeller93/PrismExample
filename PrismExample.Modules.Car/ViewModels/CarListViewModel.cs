using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismExample.Modules.Car.ViewModels
{
    public class CarListViewModel : BindableBase
    {
        readonly IRegionManager regionManager;

        public string Title => "Cars";

        private ObservableCollection<Common.Car> cars;
        public ObservableCollection<Common.Car> Cars
        {
            get { return cars; }
            set { SetProperty(ref cars, value); }
        }

        public DelegateCommand<Common.Car> CarSelectedCommand { get; private set; }

        public CarListViewModel(RegionManager regionManager)
        {
            this.regionManager = regionManager;

            CarSelectedCommand = new DelegateCommand<Common.Car>(CarSelected);
            CreateCars();
        }

        private void CarSelected(Common.Car car)
        {
            var parameters = new NavigationParameters();
            parameters.Add("car", car);

            if (car != null)
                regionManager.RequestNavigate("CarDetailsRegion", "CarDetail", parameters);
        }

        private void CreateCars()
        {
            var cars = new ObservableCollection<Common.Car>();

            for (int i = 0; i < 10; i++)
            {
                cars.Add(new Common.Car()
                {
                    Make = $"Make {i}"
                });
            }

            Cars = cars;
        }
    }
}
