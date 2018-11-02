using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CrudCompany
    {    
        /*
        public static Company ConstructCompany(Director director, CompanyType type, string name, string address)
        {
            Chancery chancery = CrudChancery.ConstructChancery();
            Company company = new Company(director, type, chancery, name, address);
            return company;
        }

        public static Company CreateCompany(Company company)
        {
            //Добавляем директора в БД
            Director director = company.Director;
            if (director.Company != null)
            {
                director.Company = null;
            }
            director = CrudDirector.CreateDirector(director);
            //Добавляем секретариат в БД
            Chancery chancery = company.CompanyChancery;
            if(chancery.ChanceryCompany != null)
            {
                chancery.ChanceryCompany = null;
            }
            chancery = CrudChancery.CreateChancery(company.CompanyChancery);
            //Добавляем компанию в БД
            int companyId = SqlCompany.AddCompany(company);
            company.Id = companyId;
            //Обновляем поля в объектах Директор и Секретариат
            director.Company = company;
            CrudDirector.UpdateDirector(director);
            chancery.ChanceryCompany = company;
            CrudChancery.UpdateChancery(chancery);
            //Обновляем директора и секретариат в компании
            company.Director = director;
            company.CompanyChancery = chancery;

            return company;
        }

        public static Company ReadCompany(int companyId)
        {
            return SqlCompany.GetCompany(companyId);
        }

        public static void UpdateCompany(Company company)
        {
            SqlCompany.UpdateCompany(company);
        }

        public static void DeleteCompany(Company company)
        {
            CrudDirector.DeleteDirector(company.Director);
            CrudChancery.DeleteChancery(company.CompanyChancery);
            SqlCompany.DeleteCompany(company.Id);
            company = null;
        }
        */
    }
}
