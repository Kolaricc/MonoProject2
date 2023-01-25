using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Solution.Common
{
    public class Sorting
    {
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }

        public Sorting(string orderBy, string orderDirection)
        {
            this.OrderBy = orderBy;
            this.OrderDirection = orderDirection;
        }
    }
}
