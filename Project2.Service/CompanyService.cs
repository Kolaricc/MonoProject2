using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Repository;
using Project2.Model;
using Project2.Service.Common;

namespace Project2.Service
{
    public class CompanyService : ICompanyService
    {
        CompanyRepository repository = new CompanyRepository();

        public List<Company> GetAllCompanies()
        {
            return repository.GetAllCompanies();
        }

        public List<Company> FindByName(string name)
        {
            return repository.FindByName(name);
        }

        public Company FindById(Guid id)
        {
            return repository.FindById(id);
        }

        public void AddNewCompany(Company company)
        {
            repository.AddNewCompany(company);
        }

        public bool UpdateCompany(Guid id, Company company)
        {
            return repository.UpdateCompany(id, company);
        }

        public bool Delete(Guid id)
        {
            return repository.Delete(id);
        }
    }
}
