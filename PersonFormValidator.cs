using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DocumentManagement
{
    public class PersonFormValidator
    {
        public static bool ValidateName(string name, string surname)
        {
            string pattern = @"^\s*[А-Я][a-я]+\s*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name) && regex.IsMatch(surname);
        }

        public static bool ValidateAge(string ageString)
        {
            if (int.TryParse(ageString, out int age))
            {
                return age > 0 && age < 120;
            }
            return false;
        }
    }
}
