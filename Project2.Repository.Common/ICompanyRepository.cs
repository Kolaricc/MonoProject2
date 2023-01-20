using Project2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Repository.Common
{
    public interface ICompanyRepository
    {
        List<Company> GetAllCompanies();

        Company FindById(Guid id);

        void AddNewCompany(Company company);

        bool UpdateCompany(Guid id, Company company);

        bool Delete(Guid id);
    }
}
