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
    }
}
