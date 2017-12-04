using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismExample.Domain;

namespace PrismExample.Infrastructure.Interface
{
    public interface ICarService
    {
        IEnumerable<Car> GetCars();
    }
}
