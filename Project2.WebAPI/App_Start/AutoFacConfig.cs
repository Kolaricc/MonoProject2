using Autofac;
using Autofac.Integration.WebApi;
using Project2.Repository;
using Project2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Project2.WebAPI.App_Start
{
    public static class AutoFacConfig
    {
        public static void Configurate()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}