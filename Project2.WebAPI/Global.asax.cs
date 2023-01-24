using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Project2.Service.Common;
using Project2.Service;
using Project2.Repository;
using Project2.Repository.Common;
using Project2.WebApi.Controllers;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace Project2.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterType<ItemService>().As<IItemService>();
            builder.RegisterType<CompanyService>().As<ICompanyService>();
            builder.RegisterType<ItemRepository>().As<IItemRepository>();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
