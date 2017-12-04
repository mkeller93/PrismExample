using System.Collections.Generic;
using System.Linq;
using PrismExample.Domain;
using PrismExample.Infrastructure.Interface;

namespace PrismExample.Infrastructure.Service
{
    public class CarService : ICarService
    {
        public IEnumerable<Car> GetCars()
        {
            return Enumerable.Range(1, 10).Select(x => new Car {Make = $"Make {x}"});
        }
    }
}
