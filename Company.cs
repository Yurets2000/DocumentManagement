using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Company
    {
        public bool InitCompany { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public CompanyType Type { get; set; }

        private Director _director;
        private Chancery _companyChancery;

        public Director Director
        {
            get
            {
                if(_director != null && _director.InitDirector)
                {
                    if(_director.EmployeeId <= 0)
                    {
                        throw new Exception("Director Id <= 0");
                    }
                    else
                    {
                        _director = SqlDirector.GetDirector(_director.EmployeeId);
                    }
                }
                return _director;
            }
            set
            {
                _director = value;
            }
        }

        public Chancery CompanyChancery
        {
            get
            {
                if(_companyChancery != null && _companyChancery.InitChancery)
                {
                    if(_companyChancery.Id <= 0)
                    {
                        throw new Exception("Company Chancery Id <= 0");
                    }
                    else
                    {
                        _companyChancery = SqlChancery.GetChancery(_companyChancery.Id);
                    }
                }
                return _companyChancery;
            }
            set
            {
                _companyChancery = value;
            }
        }

        public Company() { }

        public Company(Director director, CompanyType type, Chancery companyChancery, string name, string address)
        {
            Director = director;
            CompanyChancery = companyChancery;
            Type = type;
            Name = name;
            Address = address;
        }

        public static Company GetCompany(int Id)
        {
            return SqlCompany.GetCompany(Id);
        }

        public void Persist()
        {
            //Добавляем директора в БД
            if (Director.Company != null)
            {
                Director.Company = null;
            }
            Director.Persist();
            //Добавляем секретариат в БД
            if (CompanyChancery.ChanceryCompany != null)
            {
                CompanyChancery.ChanceryCompany = null;
            }
            CompanyChancery.Persist();
            //Добавляем компанию в БД
            Id = SqlCompany.AddCompany(this);
            //Обновляем поля в объектах Директор и Секретариат
            Director.Company = this;
            Director.Update();
            CompanyChancery.ChanceryCompany = this;
            CompanyChancery.Update();
            //Обновляем директора и секретариат в компании
            //Director = director;
            //company.CompanyChancery = chancery;
        }

        public void Update()
        {
            SqlCompany.UpdateCompany(this);
        }

        public void Delete()
        {
            Director.Quit();
            Director = null;
            CompanyChancery.Delete();
            CompanyChancery = null;
            SqlCompany.DeleteCompany(Id);
        }

        public override string ToString()
        {
            return Name + "(" + Type.ToString() + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
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

    }
}
