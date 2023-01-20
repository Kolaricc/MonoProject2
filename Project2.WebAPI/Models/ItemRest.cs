using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Model;

namespace Project2.WebAPI.Models
{
    public class ItemRest
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public decimal Price { get; set; }

        public ItemRest() { Id = Guid.NewGuid(); }

        public ItemRest(Item item)
        {
            this.Id = item.Id;
            this.Category = item.Category;
            this.Name = item.Name;
            this.CompanyId = item.CompanyId;
            this.Price = item.Price;
        }
    }
}