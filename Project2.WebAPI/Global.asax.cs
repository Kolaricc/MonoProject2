using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Project2.WebAPI.App_Start;

namespace Project2.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoFacConfig.Configurate();
        }
    }
}
