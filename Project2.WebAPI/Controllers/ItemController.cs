using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Project2.Service;
using Project2.Model;
using Project2.WebAPI.Models;
using System.Threading.Tasks;

namespace Project2.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        ItemService itemService = new ItemService();
        CompanyService companyService = new CompanyService();

        [HttpGet]
        [Route("api/Items/All")]
        public async Task<HttpResponseMessage> GetAllItemsAsync()
        {
            List<Item> items = await itemService.GetAllItemsAsync();
            List<ItemRest> restItems = new List<ItemRest>();
            foreach(Item item in items)
            {
                restItems.Add(new ItemRest(item));
            }
            if (restItems.Count > 0)
            {
                return Request.CreateResponse<List<ItemRest>>(HttpStatusCode.OK, restItems);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "List is empty!");
        }

        [HttpGet]
        [Route("api/Items/All/Clean")]
        public async Task<HttpResponseMessage> GetAllItemsCleanAsync()
        {
            List<Item> items = await itemService.GetAllItemsAsync();
            List<Company> companies = companyService.GetAllCompanies();
            List<CleanItem> cleanItems = new List<CleanItem>();
            foreach(Item item in items)
            {
                foreach(Company company in companies)
                {
                    if(company.Id == item.CompanyId)
                    {
                        cleanItems.Add(new CleanItem(item, company));
                        break;
                    }
                }
            }
            if (items.Count > 0)
            {
                return Request.CreateResponse<List<CleanItem>>(HttpStatusCode.OK, cleanItems);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "List is empty!");
        }

        [HttpGet]
        [Route("api/Items/name")]
        public async Task<HttpResponseMessage> FindByNameAsync([FromBody] string name)
        {
            List<Item> items =await itemService.FindByNameAsync(name);
            List<ItemRest> restItems = new List<ItemRest>();
            foreach(Item item in items)
            {
                restItems.Add(new ItemRest(item));
            }
            if (restItems.Count > 0)
            {
                return Request.CreateResponse<List<ItemRest>>(HttpStatusCode.OK, restItems);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
        }

        [HttpGet]
        [Route("api/Items")]
        public async Task<HttpResponseMessage> FindByIdAsync(Guid id)
        {
            Item item = await itemService.FindByIdAsync(id);
            ItemRest restItem = new ItemRest(item);
            if (restItem is null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
            }
            return Request.CreateResponse<ItemRest>(HttpStatusCode.OK, restItem);
        }

        [HttpPost]
        [Route("api/Items")]
        public async Task<HttpResponseMessage> AddNewItemAsync(ItemRest restItem)
        {
            Item item = new Item();
            item.Set(restItem.Category, restItem.Name, restItem.CompanyId, restItem.Price);
            string responseMessage = await itemService.AddNewItemAsync(item);
            if (responseMessage == "Item added")
            {
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
        }

        [HttpPost]
        [Route("api/Items/clean")]
        public async Task<HttpResponseMessage> AddCleanItemAsync(CleanItem cleanItem)
        {
            Item item = new Item();
            Guid companyId = Guid.Empty;
            List<Company> companies = companyService.GetAllCompanies();
            foreach (Company company in companies)
            {
                if(company.Name==cleanItem.CompanyName)
                {
                    companyId = company.Id;
                }
            }
            if(companyId==Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Company not found!");
            }
            item.Set(cleanItem.Category, cleanItem.Name, companyId, cleanItem.Price);
            string responseMessage = await itemService.AddNewItemAsync(item);
            if (responseMessage == "Item added")
            {
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while adding!");
        }

        [HttpPut]
        [Route("api/Items")]
        public async Task<HttpResponseMessage> UpdateItemAsync(Guid id, ItemRest restItem)
        {
            Item item = new Item();
            item.Set(restItem.Category, restItem.Name, restItem.CompanyId, restItem.Price);
            string updateResponce = await itemService.UpdateItemAsync(id, item);
            if (updateResponce == "Item not found!" || updateResponce == "Company not found!")
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, updateResponce);
            }
            return Request.CreateResponse(HttpStatusCode.OK, updateResponce);
        }

        [HttpPut]
        [Route("api/Items/clean")]
        public async Task<HttpResponseMessage> UpdateCleanItem(Guid id, CleanItem cleanItem)
        {
            Item item = new Item();
            Guid companyId = Guid.Empty;
            List<Company> companies = companyService.GetAllCompanies();
            foreach (Company company in companies)
            {
                if (company.Name == cleanItem.CompanyName)
                {
                    companyId = company.Id;
                }
            }
            item.Set(cleanItem.Category, cleanItem.Name, companyId, cleanItem.Price);
            string updateResponce = await itemService.UpdateItemAsync(id, item);
            if (updateResponce == "Item not found!" || updateResponce == "Company not found!")
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, updateResponce);
            }
            return Request.CreateResponse(HttpStatusCode.OK, updateResponce);
        }

        [HttpDelete]
        [Route("api/Items")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            if (await itemService.DeleteAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Item deleted");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
        }
    }
}