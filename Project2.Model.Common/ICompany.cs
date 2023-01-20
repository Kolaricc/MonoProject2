using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Model.Common
{
    public interface ICompany
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
    }
}
