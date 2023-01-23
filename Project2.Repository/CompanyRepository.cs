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
        public List<Company> GetAllCompanies()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Company> companies = new List<Company>();
                SqlCommand selectCompany = new SqlCommand("SELECT * FROM Company Order by name Asc;", connection);
                connection.Open();

                SqlDataReader reader = selectCompany.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company company = new Company();
                        company.SetCompany((Guid)reader[0], (string)reader[1], (string)reader[2]);
                        companies.Add(company);
                    }
                }
                reader.Close();
                return companies;
            }
        }

        public List<Company> FindByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Company> companies = new List<Company>();
                SqlCommand selectCompany = new SqlCommand($"SELECT * FROM Company Where Name = '{name}';", connection);
                connection.Open();
                SqlDataReader reader = selectCompany.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Company company = new Company();
                    company.SetCompany((Guid)reader[0], (string)reader[1], (string)reader[2]);
                    companies.Add(company);
                }
                reader.Close();
                return companies;
            }
        }

        public Company FindById(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Company company = new Company();
                SqlCommand selectCompany = new SqlCommand($"SELECT * FROM Company Where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = selectCompany.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    company.SetCompany((Guid)reader[0], (string)reader[1], (string)reader[2]);
                }
                reader.Close();
                return company;
            }
        }
        public void AddNewCompany(Company company)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand insertCompany = new SqlCommand($"Insert Into Company Values('{company.Id}','{company.Name}','{company.Email}');", connection);
                insertCompany.ExecuteReader();
            }
        }

        public bool UpdateCompany(Guid id, Company company)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand findCompany = new SqlCommand($"Select * From Company where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = findCompany.ExecuteReader();
                if (!reader.HasRows)
                {
                    return false;
                }
                reader.Close();
                SqlCommand updateCompany = new SqlCommand($"Update Company set name = '{company.Name}', email = '{company.Email}' where id = '{id}';", connection);
                updateCompany.ExecuteReader();
                return true;
            }
        }

        public bool Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand findCompany = new SqlCommand($"Select * From Company where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = findCompany.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    SqlCommand deleteCompany = new SqlCommand($"Delete From Company where Id = '{id}';", connection);
                    deleteCompany.ExecuteReader();
                    return true;
                }
                reader.Close();
                return false;
            }
        }
    }
}
