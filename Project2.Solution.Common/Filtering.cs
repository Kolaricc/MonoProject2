using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Solution.Common
{
    public class Filtering
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal PriceMax { get; set; }
        public decimal PriceMin { get; set; }

        public Filtering()
        {
            Category = null;
            Name = null;
            PriceMax = 9999;
            PriceMin = 0;
        }

        public string ToSQLFilter()
        {
            StringBuilder builder = new StringBuilder("Where ");
            if (!string.IsNullOrEmpty(Name)) 
            {
                builder.Append("Name Like '%");
                builder.Append(Name);
                builder.Append("%' And ");
            }
            if (!string.IsNullOrEmpty(Category)) 
            {
                builder.Append("Category Like '%");
                builder.Append(Category);
                builder.Append("%' And ");
            }
            builder.Append("Price between ");
            builder.Append(PriceMin);
            builder.Append(" And ");
            builder.Append(PriceMax);
            return builder.ToString();
        }
    }
}
