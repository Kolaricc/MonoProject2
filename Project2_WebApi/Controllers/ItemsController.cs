using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project2_WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        public static List<Item> items = new List<Item> { new Item(1, "fruit", "apple", "", 2.22), new Item(2, "dairy", "milk", "zbregov", 4.99), new Item(3, "misc", "Chefs knife", "Samura", 199.99) };

        [HttpGet]
        [Route("api/Items/All")]
        public HttpResponseMessage GetAllItems()
        {
            if (items != null)
            {
                return Request.CreateResponse<List<Item>>(HttpStatusCode.OK, items);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "List is empty!");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetItem(int id)
        {
            Item requestedItem = items.Find(item => item.id.Equals(id));
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
            Item requestedItem = items.FirstOrDefault(item => item.id == id);
            if (requestedItem == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found!");
            }
            requestedItem.id = 0;
            if (ItemExists(updatedItem))
            {
                requestedItem.id = id;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Item with that Id already exists!");
            }
            requestedItem.id = id;
            items[items.FindIndex(item => item == requestedItem)] = updatedItem;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            if (!items.Remove(items.Find(item => item.id == id)))
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
                    if (item.id == newItem.id)
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