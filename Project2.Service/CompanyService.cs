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

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await companyRepository.GetAllCompaniesAsync();
        }

        public async Task<List<Company>> FindByNameAsync(string name)
        {
            return await companyRepository.FindByNameAsync(name);
        }

        public async Task<Company> FindByIdAsync(Guid id)
        {
            return await companyRepository.FindByIdAsync(id);
        }

        public async Task AddNewCompanyAsync(Company company)
        {
            await companyRepository.AddNewCompanyAsync(company);
        }

        public async Task<bool> UpdateCompanyAsync(Guid id, Company company)
        {
            return await companyRepository.UpdateCompanyAsync(id, company);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await companyRepository.DeleteAsync(id);
        }
    }
}
