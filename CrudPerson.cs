using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CrudPerson
    {
        /*
        public static Person ConstructPerson(string name, string surname, int age)
        {
            Person person = new Person(name, surname, age)
            {
                Working = false
            };
            return person;
        }

        public static Person CreatePerson(Person person)
        {
            int id = SqlPerson.AddPerson(person);
            person.Id = id;
            return person;
        }

        public static Person ReadPerson(int personId)
        {
            return SqlPerson.GetPerson(personId);
        }

        public static void UpdatePerson(Person person)
        {
            SqlPerson.UpdatePerson(person);
        }

        public static void DeletePerson(Person person)
        {
            Secretary secretary = SqlSecretary.GetSecretaryByPerson(person.Id);
            MainSecretary mainSecretary = SqlMainSecretary.GetMainSecretaryByPerson(person.Id);
            Director director = SqlDirector.GetDirectorByPerson(person.Id);
            if(secretary != null)
            {
                CrudSecretary.DeleteSecretary(secretary);
            }
            if(mainSecretary != null)
            {
                CrudMainSecretary.DeleteMainSecretary(mainSecretary);
            }
            if(director != null)
            {
                CrudCompany.DeleteCompany(director.Company);
            }
            SqlPerson.DeletePerson(person.Id);
            person = null;
        }
        */
    }

}
