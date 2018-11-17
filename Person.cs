using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Person
    {
        public UpdateState UpdateState { get; set; } = UpdateState.UPDATE_NEEDED;
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Working { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }

        public Person() { }

        public Person(string name, string surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            Person person = (Person)obj;
            if (person.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == person.Id;
        }

        public void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Id = DataLists.GeneratePersonId();
            dataLists.Persons.Add(this);
        }

        public void Delete()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Secretary secretary = dataLists.Secretaries.Find((s) => s.Id == Id);
            MainSecretary mainSecretary = dataLists.MainSecretaries.Find((ms) => ms.Id == Id);
            Director director = dataLists.Directors.Find((d) => d.Id == Id);
            if (secretary != null)
            {
                secretary.Quit();
                secretary = null;
            }
            if (mainSecretary != null)
            {
                mainSecretary.Quit();
                mainSecretary = null;
            }
            if (director != null)
            {
                // С компанией удаляется и директор
                director.Company.Delete();
            }
            dataLists.Persons.Remove(this);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
