using Project2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Service.Common
{
    public interface ICompanyService
    {

        List<Company> GetAllCompanies();

        Company FindById(Guid id);

        List<Company> FindByName(string name);

        void AddNewCompany(Company company);

        bool UpdateCompany(Guid id, Company company);

        bool Delete(Guid id);
    }
}
