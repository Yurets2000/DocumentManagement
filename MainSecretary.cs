using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class MainSecretary : Employee
    {
        public bool InitMainSecretary { get; set; }

        public MainSecretary() : base() { }

        public MainSecretary(Person person, Company company, int salary) : base(person, company, salary)
        {
        }

        public void RedirectPendingDocument(Secretary secretary, Document document) { }
    }
}
