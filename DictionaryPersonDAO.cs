using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class DictionaryPersonDAO : IPersonDAO
    {
        private static Dictionary<int, Person> persons = new Dictionary<int, Person>();
        private static DictionaryPersonDAO dao;

        private DictionaryPersonDAO() { }

        public static DictionaryPersonDAO GetInstance()
        {
            if (dao == null)
            {
                dao = new DictionaryPersonDAO();
            }
            return dao;
        }

        public void AddPerson(Person person)
        {
            persons.Add(person.Id, person);
        }

        public List<Person> GetAllPersons()
        {
            return persons.Values.ToList();
        }

        public Person GetPersonById(int id)
        {
            return persons[id];
        }

        public void RemovePerson(int id)
        {
            persons.Remove(id);
        }
    }
}
