using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public abstract class Employee : Person
    {
        public int Salary { get; set; }
        public int EmployeeId { get; set; }

        private Company _company;

        public Company Company
        {
            get
            {
                if(_company != null && _company.InitCompany)
                {
                    if(_company.Id <= 0)
                    {
                        throw new Exception("Company Id <= 0");
                    }
                    else
                    {
                        _company = SqlCompany.GetCompany(_company.Id);
                        _company.InitCompany = false;
                    }
                }
                return _company;
            }
            set
            {
                _company = value;
            }
        }

        public Employee() { }

        public Employee(Person person, Company company, int salary) : base(person.Name, person.Surname, person.Age)
        {
            Id = person.Id;
            Working = true;
            Company = company;
            Salary = salary;
        }

        public abstract void Quit();

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Employee employee = (Employee)obj;
            if (employee.EmployeeId <= 0 || EmployeeId <= 0)
            {
                return false;
            }
            return EmployeeId == employee.EmployeeId;
        }
    }
}
