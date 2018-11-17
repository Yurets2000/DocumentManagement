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

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            CompanyType companyType = (CompanyType)obj;
            if (companyType.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == companyType.Id;
        }

        public void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Id = DataLists.GenerateCompanyTypeId();
            dataLists.CompanyTypes.Add(this);
        }

        public void Delete()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            dataLists.CompanyTypes.Remove(this);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
