using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace Project2_WebApi.Controllers
{
    public class CompanyController : ApiController
    {

        [HttpGet]
        [Route("api/Company/All")]
        public HttpResponseMessage GetAllItems()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Company> companies = new List<Company>();
                SqlCommand command = new SqlCommand("SELECT * FROM Company Order by name Asc;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company company = new Company();
                        company.SetCompany((Guid)reader[0], (string)reader[1], (string)reader[2]);
                        companies.Add(company);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<Company>>(HttpStatusCode.OK, companies);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "List is empty!");
                }
            }
        }

        [HttpGet]
        [Route("api/Company/name")]
        public HttpResponseMessage FindName([FromBody] string Name)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Company> companies = new List<Company>();
                SqlCommand command = new SqlCommand($"SELECT * FROM Company Where Name = '{Name}';", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company company = new Company();
                        company.SetCompany((Guid)reader[0], (string)reader[1], (string)reader[2]);
                        companies.Add(company);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<Company>>(HttpStatusCode.OK, companies);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
                }
            }
        }

        [HttpGet]
        [Route("api/Company")]
        public HttpResponseMessage FindId(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Company> companies = new List<Company>();
                SqlCommand command = new SqlCommand($"SELECT * FROM Company Where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company company = new Company();
                        company.SetCompany((Guid)reader[0], (string)reader[1], (string)reader[2]);
                        companies.Add(company);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<Company>>(HttpStatusCode.OK, companies);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
                }
            }
        }

        [HttpPost]
        [Route("api/Company")]
        public HttpResponseMessage AddNewItem(Company newItem)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"Insert Into Company Values('{newItem.Id}','{newItem.Name}','{newItem.Email}');", connection);
                command.ExecuteReader();
                return Request.CreateResponse(HttpStatusCode.OK, "Item added");
            }
        }

        [HttpPut]
        [Route("api/Company/change")]
        public HttpResponseMessage UpdateItem(Guid id, Company updatedItem)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                SqlCommand GetItem = new SqlCommand($"Select * From Company where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = GetItem.ExecuteReader();
                if (!reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
                }
                reader.Close();
                SqlCommand command = new SqlCommand($"Update Company set name = '{updatedItem.Name}', email = '{updatedItem.Email}' where id = '{id}';", connection);
                command.ExecuteReader();
                return Request.CreateResponse(HttpStatusCode.OK, "Item successfully changed");
            }
        }

        [HttpDelete]
        [Route("api/Company/delete")]
        public HttpResponseMessage Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                SqlCommand FindItem = new SqlCommand($"Select * From Company where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = FindItem.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    SqlCommand command = new SqlCommand($"Delete From Company where Id = '{id}';", connection);
                    command.ExecuteReader();
                    return Request.CreateResponse(HttpStatusCode.OK, "Item deleted");
                }
                reader.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
            }
        }
    }
}