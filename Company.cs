using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Company
    {
        //Нужно ли проинициализировать указатели в объекте
        public InitializationState InitState { get; set; } = InitializationState.NOT_INITIALIZED;
        public UpdateState UpdateState { get; set; } = UpdateState.UPDATE_NEEDED;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public CompanyType Type { get; set; }
        public Director Director { get; set; }
        public Chancery Chancery { get; set; }

        public Company() { }

        public Company(Director director, CompanyType type, Chancery companyChancery, string name, string address)
        {
            Director = director;
            Chancery = companyChancery;
            Type = type;
            Name = name;
            Address = address;
        }

        public void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Director.Company = this;
            Director.Persist();
            Chancery.Company = this;
            Chancery.Persist();
            Id = DataLists.GenerateCompanyId();
            dataLists.Companies.Add(this);
        }

        public void Delete()
        {
            Director.Quit();
            Director = null;
            Chancery.Delete();
            Chancery = null;
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            dataLists.Companies.Remove(this);
        }

        public override string ToString()
        {
            return Name + "(" + Type.ToString() + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            Company company = (Company)obj;
            if (company.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == company.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
