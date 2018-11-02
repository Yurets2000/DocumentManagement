using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Person
    {
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
            if (obj == null)
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
            Id = SqlPerson.AddPerson(this);
        }

        public static Person GetPerson(int Id)
        {
            return SqlPerson.GetPerson(Id);
        }

        public void Update()
        {
            SqlPerson.UpdatePerson(this);
        }

        public void Delete()
        {
            Secretary secretary = SqlSecretary.GetSecretaryByPerson(Id);
            MainSecretary mainSecretary = SqlMainSecretary.GetMainSecretaryByPerson(Id);
            Director director = SqlDirector.GetDirectorByPerson(Id);
            if (secretary != null)
            {
                secretary.Delete();
            }
            if (mainSecretary != null)
            {
                mainSecretary.Delete();
            }
            if (director != null)
            {
                director.Company.Delete();
            }
            SqlPerson.DeletePerson(Id);
        }
    }
}
