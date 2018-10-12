using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CompanyType
    {
        public int Id { get; set; }
        public readonly string type;

        public CompanyType(string type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return type;
        }
    }
}
