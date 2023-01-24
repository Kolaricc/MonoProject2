using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project2.Service.Common;

namespace Project2.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ItemService>().As<IItemService>();
            builder.RegisterType<CompanyService>().As<ICompanyService>();
        }
    }
}
