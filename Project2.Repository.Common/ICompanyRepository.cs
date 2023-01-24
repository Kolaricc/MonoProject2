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
        Task<List<Company>> GetAllCompaniesAsync();

        Task<Company> FindByIdAsync(Guid id);

        Task<List<Company>> FindByNameAsync(string name);

        Task AddNewCompanyAsync(Company company);

        Task<bool> UpdateCompanyAsync(Guid id, Company company);

        Task<bool> DeleteAsync(Guid id);
    }
}
