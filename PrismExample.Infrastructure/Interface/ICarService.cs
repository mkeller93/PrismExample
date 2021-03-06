﻿using System.Collections.Generic;
using PrismExample.Domain;

namespace PrismExample.Infrastructure.Interface
{
    public interface ICarService
    {
        IEnumerable<Car> GetCars();
    }
}
