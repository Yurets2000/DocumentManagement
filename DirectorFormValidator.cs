using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class DirectorFormValidator
    {
        public static bool ValidateSalary(string salaryString)
        {
            if (int.TryParse(salaryString, out int salary))
            {
                return salary > 3200 && salary < 1000000;
            }
            return false;
        }
    }
}
