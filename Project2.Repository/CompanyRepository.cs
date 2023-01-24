using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Model;
using Project2.Repository.Common;

namespace Project2.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
        string connectionString = "Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True";
        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Company> companies = new List<Company>();
                SqlCommand selectCompany = new SqlCommand("SELECT * FROM Company Order by name Asc;", connection);
                await connection.OpenAsync();

                SqlDataReader readerAsync = await selectCompany.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    while (await readerAsync.ReadAsync())
                    {
                        Company company = new Company();
                        company.SetCompany((Guid)readerAsync[0], (string)readerAsync[1], (string)readerAsync[2]);
                        companies.Add(company);
                    }
                }
                readerAsync.Close();
                return companies;
            }
        }

        public async Task<List<Company>> FindByNameAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Company> companies = new List<Company>();
                SqlCommand selectCompany = new SqlCommand($"SELECT * FROM Company Where Name = '{name}';", connection);
                await connection.OpenAsync();
                SqlDataReader readerAsync = await selectCompany.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    await readerAsync.ReadAsync();
                    Company company = new Company();
                    company.SetCompany((Guid)readerAsync[0], (string)readerAsync[1], (string)readerAsync[2]);
                    companies.Add(company);
                }
                readerAsync.Close();
                return companies;
            }
        }

        public async Task<Company> FindByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Company company = new Company();
                SqlCommand selectCompany = new SqlCommand($"SELECT * FROM Company Where Id = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader readerAsync = await selectCompany.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    await readerAsync.ReadAsync();
                    company.SetCompany((Guid)readerAsync[0], (string)readerAsync[1], (string)readerAsync[2]);
                }
                readerAsync.Close();
                return company;
            }
        }
        public async Task AddNewCompanyAsync(Company company)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand insertCompanyAsync = new SqlCommand($"Insert Into Company Values('{company.Id}','{company.Name}','{company.Email}');", connection);
                await insertCompanyAsync.ExecuteReaderAsync();
            }
        }

        public async Task<bool> UpdateCompanyAsync(Guid id, Company company)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand findCompany = new SqlCommand($"Select * From Company where Id = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader readerAsync = await findCompany.ExecuteReaderAsync();
                if (!readerAsync.HasRows)
                {
                    return false;
                }
                readerAsync.Close();
                SqlCommand updateCompany = new SqlCommand($"Update Company set name = '{company.Name}', email = '{company.Email}' where id = '{id}';", connection);
                await updateCompany.ExecuteReaderAsync();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand findCompany = new SqlCommand($"Select * From Company where Id = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader readerAsync = await findCompany.ExecuteReaderAsync();
                if (readerAsync.HasRows)
                {
                    readerAsync.Close();
                    SqlCommand deleteCompany = new SqlCommand($"Delete From Company where Id = '{id}';", connection);
                    await deleteCompany.ExecuteReaderAsync();
                    return true;
                }
                readerAsync.Close();
                return false;
            }
        }
    }
}
