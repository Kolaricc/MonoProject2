using Project2.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Model
{
    public class Company : ICompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Company() { Id = Guid.NewGuid(); }

        public Company(string name, string email)
        {
            Id = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
        }

        public void SetCompany(Guid id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
        }
    }
}
