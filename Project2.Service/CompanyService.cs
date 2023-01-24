using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Repository;
using Project2.Model;
using Project2.Service.Common;
using Project2.Repository.Common;

namespace Project2.Service
{
    public class CompanyService : ICompanyService
    {
        ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public List<Company> GetAllCompanies()
        {
            return companyRepository.GetAllCompanies();
        }

        public List<Company> FindByName(string name)
        {
            return companyRepository.FindByName(name);
        }

        public Company FindById(Guid id)
        {
            return companyRepository.FindById(id);
        }

        public void AddNewCompany(Company company)
        {
            companyRepository.AddNewCompany(company);
        }

        public bool UpdateCompany(Guid id, Company company)
        {
            return companyRepository.UpdateCompany(id, company);
        }

        public bool Delete(Guid id)
        {
            return companyRepository.Delete(id);
        }
    }
}
