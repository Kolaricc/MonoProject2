using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Text;
using System.Threading.Tasks;
using Project2.Repository.Common;

namespace Project2.Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ItemRepository>().As<IItemRepository>();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
        }
    }
}
