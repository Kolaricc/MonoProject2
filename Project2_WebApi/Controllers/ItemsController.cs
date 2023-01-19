using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace Project2_WebApi.Controllers
{
    public class ItemsController : ApiController
    {

        [HttpGet]
        [Route("api/Items/All")]
        public HttpResponseMessage GetAllItems()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Item> items = new List<Item>();
                SqlCommand command = new SqlCommand("SELECT * FROM Item;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Item item = new Item(); 
                        item.SetItem((Guid)reader[0], (string)reader[1], (string)reader[2], (Guid)reader[3],(decimal)reader[4]);
                        items.Add(item);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<Item>>(HttpStatusCode.OK, items);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "List is empty!");
                }
            }
        }

        [HttpGet]
        [Route("api/Items/All/Clean")]
        public HttpResponseMessage GetAllItemsClean()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<CleanItem> items = new List<CleanItem>();
                SqlCommand command = new SqlCommand("SELECT Item.Category, Item.Name, Company.Name, Item.Price FROM Item, Company Where Item.CompanyId = Company.Id;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CleanItem item = new CleanItem();
                        item.SetItem((string)reader[0], (string)reader[1],(string)reader[2], (decimal)reader[3]);
                        items.Add(item);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<CleanItem>>(HttpStatusCode.OK, items);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "List is empty!");
                }
            }
        }

        [HttpGet]
        [Route("api/Items/name")]
        public HttpResponseMessage FindName([FromBody]string Name)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Item> items = new List<Item>();
                SqlCommand command = new SqlCommand($"SELECT * FROM Item Where Item.Name = '{Name}';", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Item item = new Item();
                        item.SetItem((Guid)reader[0], (string)reader[1], (string)reader[2], (Guid)reader[3], (decimal)reader[4]);
                        items.Add(item);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<Item>>(HttpStatusCode.OK, items);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
                }
            }
        }

        [HttpGet]
        [Route("api/Items")]
        public HttpResponseMessage FindId( Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Item> items = new List<Item>();
                SqlCommand command = new SqlCommand($"SELECT * FROM Item Where Item.Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Item item = new Item();
                        item.SetItem((Guid)reader[0], (string)reader[1], (string)reader[2], (Guid)reader[3], (decimal)reader[4]);
                        items.Add(item);
                    }
                    reader.Close();
                    return Request.CreateResponse<List<Item>>(HttpStatusCode.OK, items);
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
                }
            }
        }

        [HttpPost]
        [Route("api/Items")]
        public HttpResponseMessage AddNewItem(Item newItem)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                Guid companyId;
                SqlCommand GetCompany = new SqlCommand($"Select Id From Company where Id = '{newItem.CompanyId}';", connection);
                connection.Open();
                SqlDataReader reader = GetCompany.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    companyId = (Guid)reader[0];
                }
                else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Company not found!");
                }
                reader.Close();
                SqlCommand command = new SqlCommand($"Insert Into Item Values('{newItem.Id}','{newItem.Category}','{newItem.Name}','{companyId}',{newItem.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)});", connection);
                command.ExecuteReader();
                return Request.CreateResponse(HttpStatusCode.OK, "Item added");
            }
        }

        [HttpPut]
        [Route("api/Items/change")]
        public HttpResponseMessage UpdateItem(Guid id, Item updatedItem)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                SqlCommand GetItem = new SqlCommand($"Select * From Item where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = GetItem.ExecuteReader();
                if (!reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
                }
                reader.Close();
                SqlCommand command = new SqlCommand($"Update Item set category = '{updatedItem.Category}', name = '{updatedItem.Name}', companyid = '{updatedItem.CompanyId}', price = {updatedItem.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)} where id = '{id}';", connection);
                command.ExecuteReader();
                return Request.CreateResponse(HttpStatusCode.OK,"Item successfully changed");
            }
        }

        [HttpDelete]
        [Route("api/Items/delete")]
        public HttpResponseMessage Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                SqlCommand FindItem = new SqlCommand($"Select * From Item where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = FindItem.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    SqlCommand command = new SqlCommand($"Delete From Item where Id = '{id}';", connection);
                    command.ExecuteReader();
                    return Request.CreateResponse(HttpStatusCode.OK, "Item deleted");
                }
                reader.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
            }
        }
    }
}