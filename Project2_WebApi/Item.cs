using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2_WebApi
{
    public class Item
    {
        public int id { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string maker { get; set; }
        public double price { get; set; }

        public Item(int id, string category, string name, string maker, double price)
        {
            this.id = id;
            this.category = category;
            this.name = name;
            this.maker = maker;
            this.price = price;
        }
    }
}