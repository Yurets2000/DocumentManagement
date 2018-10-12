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
            string pattern = @"^\s*([A-Z][a-z]+[\s?(\.\s)])+\s*$?";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name);
        }

        public static bool ValidateAddress(string address)
        {
            //          string pattern = @"^\s*город [А-Я][а-я]+\,
            //                   \s(ул\.|пер\.)
            //                  \s[А-Я][а-я]+\,
            //                  \s(\d+)?\s*$";
            string pattern = @"^\s*город [А-Я][а-я]+\s*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(address);
        }
    }
}
