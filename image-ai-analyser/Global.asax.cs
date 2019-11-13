using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using image_ai_analyser.App_Start;

namespace image_ai_analyser
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ContainerConfig.RegisterContainer();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
