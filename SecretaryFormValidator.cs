using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class SecretaryFormValidator
    {

        public static bool ValidateSalary(string salaryString)
        {
            if (int.TryParse(salaryString, out int salary))
            {
                return salary > 0 && salary < 100000;
            }
            return false;
        }
    }
}
