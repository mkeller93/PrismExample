using System.Collections.Generic;
using System.Linq;
using PrismExample.Domain;
using PrismExample.Infrastructure.Interface;

namespace PrismExample.Infrastructure.Service
{
    public class PersonService : IPersonService
    {
        public IEnumerable<Person> GetPersons()
        {
            return Enumerable.Range(1, 10).Select(i => new Person()
            {
                FirstName = $"First {i}",
                LastName = $"Last {i}",
                Age = i
            });
        }
    }
}
