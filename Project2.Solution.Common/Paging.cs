using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Solution.Common
{
    public class Paging
    {
        public ushort PageSize { get; set; }
        public ushort PageNumber { get; set; }

        public Paging(ushort pageSize, ushort pageNumber)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int GetOffset()
        {
            return PageSize * (PageNumber - 1);
        }
    }
}
