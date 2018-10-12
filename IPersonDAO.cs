using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public interface IPersonDAO
    {
        List<Person> GetAllPersons();
        Person GetPersonById(int id);
        void AddPerson(Person person);
        void RemovePerson(int id);
    }
}
