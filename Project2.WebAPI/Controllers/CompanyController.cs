 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project2.Service;
using Project2.Model;
using Project2.WebAPI.Models;
using Project2.Service.Common;
using System.Threading.Tasks;

namespace Project2.WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        [Route("api/Company/All")]
        public async Task<HttpResponseMessage> GetAllItems()
        {
            List<Company> companies = await companyService.GetAllCompaniesAsync();
            List<CompanyRest> restCompanies = new List<CompanyRest>();
            foreach(Company company in companies)
            {
                restCompanies.Add(new CompanyRest(company));
            }
            if (restCompanies.Count != 0)
            {
                return Request.CreateResponse<List<CompanyRest>>(HttpStatusCode.OK, restCompanies);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "List is empty!");
            }
        }

        [HttpGet]
        [Route("api/Company/name")]
        public async Task<HttpResponseMessage> FindName([FromBody] string name)
        {
            List<Company> companies = await companyService.FindByNameAsync(name);
            List<CompanyRest> restCompanies = new List<CompanyRest>();
            foreach (Company company in companies)
            {
                restCompanies.Add(new CompanyRest(company));
            }
            if (companies.Count != 0) 
            {
                return Request.CreateResponse<List<CompanyRest>>(HttpStatusCode.OK, restCompanies);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Company not found!");
            }
        }

        [HttpGet]
        [Route("api/Company")]
        public async Task<HttpResponseMessage> FindById(Guid id)
        {
            Company company = await companyService.FindByIdAsync(id);
            CompanyRest restCompany = new CompanyRest(company);
            if (restCompany is null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Company not found!");
            }
            else
            {
                return Request.CreateResponse<CompanyRest>(HttpStatusCode.OK, restCompany);
            }
        }

        [HttpPost]
        [Route("api/Company")]
        public async Task<HttpResponseMessage> AddNewCompany(CompanyRest restCompany)
        {
            Company company = new Company(restCompany.Name, restCompany.Email);
            await companyService.AddNewCompanyAsync(company);
            return Request.CreateResponse(HttpStatusCode.OK, "Company added");
        }

        [HttpPut]
        [Route("api/Company/change")]
        public async Task<HttpResponseMessage> UpdateCompany(Guid id, CompanyRest restCompany)
        {
            Company company = new Company(restCompany.Name, restCompany.Email);
            if (await companyService.UpdateCompanyAsync(id,company))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Company successfully changed");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Company not found!");
        }

        [HttpDelete]
        [Route("api/Company/delete")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            if (await companyService.DeleteAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Item deleted");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
        }
    }
}