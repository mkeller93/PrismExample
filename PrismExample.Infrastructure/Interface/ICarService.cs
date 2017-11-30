using PrismExample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismExample.Infrastructure.Interface
{
    public interface ICarService
    {
        IEnumerable<Car> GetCars();
    }
}
