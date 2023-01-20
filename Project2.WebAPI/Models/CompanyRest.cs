using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Model;

namespace Project2.WebAPI.Models
{
    public class CompanyRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CompanyRest() { }

        public CompanyRest(Company company)
        {
            this.Id = company.Id;
            this.Name = company.Name;
            this.Email = company.Email;
        }
    }
}