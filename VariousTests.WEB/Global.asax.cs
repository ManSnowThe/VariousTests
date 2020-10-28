using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VariousTests.WEB.Util;

namespace VariousTests.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //KernelHolder.Kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(KernelHolder.Kernel));
        }
    }
}
