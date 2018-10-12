using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class DictionaryCompanyDAO : ICompanyDAO
    {
        private static Dictionary<int, Company> companies = new Dictionary<int, Company>();
        private static DictionaryCompanyDAO dao;

        private DictionaryCompanyDAO() { }

        public static DictionaryCompanyDAO GetInstance()
        {
            if (dao == null)
            {
                dao = new DictionaryCompanyDAO();
            }
            return dao;
        }

        public void RemoveCompany(int id)
        {
            companies.Remove(id);
        }

        public void AddCompany(Company company)
        {
            companies.Add(company.Id, company);
        }

        public Company GetCompanyById(int id)
        {
            return companies[id];
        }

        public Company GetCompanyByName(string name)
        {
            List<Company> companies = GetAllCompanies();
            foreach (Company c in companies)
            {
                if (c.Name.Equals(name))
                {
                    return c;
                }
            }
            return null;
        }

        public List<Company> GetAllCompanies()
        {
            return companies.Values.ToList();
        }
    }
}
