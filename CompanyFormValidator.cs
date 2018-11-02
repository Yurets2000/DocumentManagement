using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DocumentManagement
{
    public class CompanyFormValidator
    {

        public static bool ValidateName(string name)
        {
            string pattern = @"^([A-Za-z]+[\s(\.\s)]?)+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name);
        }

        public static bool ValidateAddress(string address)
        {
            string pattern = @"^город [А-Я][а-я]+, (ул\.|пер\.) [А-Я][а-я]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(address);
        }
    }
}
