using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    [Serializable]
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

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            DocumentType documentType = (DocumentType)obj;
            if (documentType.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == documentType.Id;
        }

        public void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Id = DataLists.GenerateDocumentTypeId();
            dataLists.DocumentTypes.Add(this);
        }

        public void Delete()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            dataLists.DocumentTypes.Remove(this);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
