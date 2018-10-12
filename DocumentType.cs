using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class DocumentType
    {
        public int Id { get; set; }
        public readonly string type;

        public DocumentType() { }

        public DocumentType(String type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return type;
        }
    }
}
