using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DocumentManagement
{
    public class DocumentFormValidator
    {
        public static bool ValidateDocumentCode(string documentCodeString)
        {
            if (int.TryParse(documentCodeString, out int documentCode))
            {
                if(documentCode > 0 && documentCode < 9000)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ValidateTitle(string title)
        {
            string pattern = @"([А-Я]?[а-я]+\s?)+";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(title);
        }
    }
}
