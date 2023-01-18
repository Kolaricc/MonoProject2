using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2_WebApi
{
    public class Item
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public double Price { get; set; }

        public Item(int id, string category, string name, string maker, double price)
        {
            this.Id = id;
            this.Category = category;
            this.Name = name;
            this.Maker = maker;
            this.Price = price;
        }
    }
}