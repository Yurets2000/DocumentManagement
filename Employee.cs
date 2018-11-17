using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public abstract class Employee : Person
    {
        public InitializationState InitState { get; set; } = InitializationState.NOT_INITIALIZED;
        public new UpdateState UpdateState { get; set; } = UpdateState.UPDATE_NEEDED;
        public int Salary { get; set; }
        public int EmployeeId { get; set; }
        public Company Company { get; set; }

        public Employee() { }

        public Employee(Person person, Company company, int salary) : base(person.Name, person.Surname, person.Age)
        {
            Id = person.Id;
            Working = true;
            person.Working = true;
            Company = company;
            Salary = salary;
        }

        public abstract void Quit();

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
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

        public override int GetHashCode()
        {
            var hashCode = -1499410252;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EmployeeId.GetHashCode();
            return hashCode;
        }
    }
}
