using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2_WebApi
{
    public class CleanItem
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }

        public CleanItem() {  }

        public CleanItem(string category, string name, decimal price)
        {
            this.Category = category;
            this.Name = name;
            this.Price = price;
        }

        public void SetItem(string category, string name, string companyName, decimal price)
        {
            this.Category = category;
            this.Name = name;
            this.CompanyName = companyName;
            this.Price = price;
        }
    }
}