using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Model.Common;

namespace Project2.Model
{
    public class Item : IItem
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public decimal Price { get; set; }
        public DateTime AdditionTime { get; set; }

        public Item() { Id = Guid.NewGuid(); AdditionTime = DateTime.Now; }

        public Item(string category, string name, decimal price)
        {
            Id = Guid.NewGuid();
            this.Category = category;
            this.Name = name;
            this.Price = price;
            AdditionTime = DateTime.Now;
        }

        public void Set(Guid id, string category, string name, Guid companyId, decimal price, DateTime additionTime)
        {
            this.Id = id;
            this.Category = category;
            this.Name = name;
            this.CompanyId = companyId;
            this.Price = price;
            this.AdditionTime = additionTime;
        }
        public void Set(string category, string name, Guid companyId, decimal price)
        {
            this.Category = category;
            this.Name = name;
            this.CompanyId = companyId;
            this.Price = price;
        }
    }
}