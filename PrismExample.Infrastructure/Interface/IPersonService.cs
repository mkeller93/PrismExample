using System.Collections.Generic;
using PrismExample.Domain;

namespace PrismExample.Infrastructure.Interface
{
    public interface IPersonService
    {
        IEnumerable<Person> GetPersons();
    }
}
