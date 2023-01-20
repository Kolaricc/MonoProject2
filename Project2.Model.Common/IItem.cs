using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Model.Common
{
    public interface IItem
    {
        Guid Id { get; set; }
        string Category { get; set; }
        string Name { get; set; }
        Guid CompanyId { get; set; }
        decimal Price { get; set; }

    }
}
