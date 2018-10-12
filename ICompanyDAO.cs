using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public interface ICompanyDAO
    {
        List<Company> GetAllCompanies();
        Company GetCompanyById(int id);
        Company GetCompanyByName(string name);
        void AddCompany(Company company);
        void RemoveCompany(int id);
    }
}
