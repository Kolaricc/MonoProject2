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
        public HttpResponseMessage GetAllItems()
        {
            List<Company> companies = companyService.GetAllCompanies();
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
        public HttpResponseMessage FindName([FromBody] string name)
        {
            List<Company> companies = companyService.FindByName(name);
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
        public HttpResponseMessage FindById(Guid id)
        {
            Company company = companyService.FindById(id);
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
        public HttpResponseMessage AddNewCompany(CompanyRest restCompany)
        {
            Company company = new Company(restCompany.Name, restCompany.Email);
            companyService.AddNewCompany(company);
            return Request.CreateResponse(HttpStatusCode.OK, "Company added");
        }

        [HttpPut]
        [Route("api/Company/change")]
        public HttpResponseMessage UpdateCompany(Guid id, CompanyRest restCompany)
        {
            Company company = new Company(restCompany.Name, restCompany.Email);
            if (companyService.UpdateCompany(id,company))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Company successfully changed");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Company not found!");
        }

        [HttpDelete]
        [Route("api/Company/delete")]
        public HttpResponseMessage Delete(Guid id)
        {
            if (companyService.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Item deleted");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found!");
        }
    }
}