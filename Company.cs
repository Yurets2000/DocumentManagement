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
                        _director = SqlDirectorDAO.GetDirector(_director.EmployeeId);
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
                        _companyChancery = SqlChanceryDAO.GetChancery(_companyChancery.Id);
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

        public override string ToString()
        {
            return Name + "(" + Type.ToString() + ")";
        }
       
    }
}
