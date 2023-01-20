using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Model;

namespace Project2.WebAPI.Models
{
    public class CleanItem
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }

        public CleanItem() { }

        public CleanItem(Item item, Company company)
        {
            this.Category = item.Category;
            this.Name = item.Name;
            this.CompanyName = company.Name;
            this.Price = item.Price;
        }
    }
}