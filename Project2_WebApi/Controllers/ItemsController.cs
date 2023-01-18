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
        public static List<Item> items;

        [HttpGet]
        [Route("api/Items/All")]
        public HttpResponseMessage GetAllItems()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Item;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(new Item(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),reader.GetDouble(4)));
                    }
                    return Request.CreateResponse<List<Item>>(HttpStatusCode.OK, items);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "List is empty!");
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage GetItem(int id)
        {
            Item requestedItem = items.Find(item => item.Id.Equals(id));
            if (requestedItem != null)
            {
                return Request.CreateResponse<Item>(HttpStatusCode.OK, requestedItem);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found!");
            }
        }

        [HttpPost]
        [Route("api/Items")]
        public HttpResponseMessage AddNewItem(Item newItem)
        {
            if (ItemExists(newItem)) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Item with that Id already exists!");
            }
            items.Add(newItem);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage UpdateItem(int id, Item updatedItem)
        {
            Item requestedItem = items.FirstOrDefault(item => item.Id == id);
            if (requestedItem == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found!");
            }
            requestedItem.Id = 0;
            if (ItemExists(updatedItem))
            {
                requestedItem.Id = id;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Item with that Id already exists!");
            }
            requestedItem.Id = id;
            items[items.FindIndex(item => item == requestedItem)] = updatedItem;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            if (!items.Remove(items.Find(item => item.Id == id)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found!");
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        public bool ItemExists(Item newItem)
        {
            foreach (Item item in items)
            {
                try
                {
                    if (item.Id == newItem.Id)
                    {
                        return true;
                    }
                }
                catch (NullReferenceException)
                {
                    return true;
                }
            }
            return false;
        }
    }
}