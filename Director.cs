using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Director : Employee
    {
        public bool InitDirector { get; set; }

        public Director() : base() { }

        public Director(Person person, Company company, int salary) : base(person, company, salary)
        {
        }
    }
}
