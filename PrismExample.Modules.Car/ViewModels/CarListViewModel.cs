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

namespace PrismExample.Modules.Car.ViewModels
{
    public class CarListViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly ICarService carService;

        public string Title => "Cars";

        private ObservableCollection<Domain.Car> cars;
        public ObservableCollection<Domain.Car> Cars
        {
            get { return cars; }
            set { SetProperty(ref cars, value); }
        }

        public DelegateCommand<Domain.Car> CarSelectedCommand { get; private set; }

        public CarListViewModel(RegionManager regionManager, ICarService carService)
        {
            this.regionManager = regionManager;
            this.carService = carService;

            CarSelectedCommand = new DelegateCommand<Domain.Car>(CarSelected);
            CreateCars();
        }

        private void CarSelected(Domain.Car car)
        {
            var parameters = new NavigationParameters();
            parameters.Add("car", car);

            if (car != null)
                regionManager.RequestNavigate(RegionNames.CarDetail, NavigationTargets.CarDetail, parameters);
        }

        private void CreateCars()
        {
            var cars = new ObservableCollection<Domain.Car>();
            cars.AddRange(carService.GetCars());
            Cars = cars;
        }
    }
}
