using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismExample.Domain;
using PrismExample.Infrastructure.Interface;

namespace PrismExample.Infrastructure.Service
{
    public class CarService : ICarService
    {
        public IEnumerable<Car> GetCars()
        {
            foreach (var x in Enumerable.Range(1, 10))
            {
                yield return new Car {Make = $"Make {x}"};
            }
        }
    }
}
