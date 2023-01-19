﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2_WebApi
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public decimal Price { get; set; }

        public Item() { Id = Guid.NewGuid(); }

        public Item(string category, string name, decimal price)
        {
            Id = Guid.NewGuid();
            this.Category = category;
            this.Name = name;
            this.Price = price;
        }

        public void SetItem(Guid id, string category, string name, Guid companyId, decimal price)
        {
            this.Id = id;
            this.Category = category;
            this.Name = name;
            this.CompanyId = companyId;
            this.Price = price;
        }
    }
}