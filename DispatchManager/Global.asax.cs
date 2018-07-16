using Autofac;
using Autofac.Integration.Mvc;
using DispatchManager.Services.TruckManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace DispatchManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
              AutoFac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          


        }

        private void AutoFac() {

            var builder = new ContainerBuilder(); 
            builder.RegisterControllers(typeof(MvcApplication).Assembly); 
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider(); 
            builder.RegisterModule<AutofacWebTypesModule>(); 
            builder.RegisterSource(new ViewRegistrationSource()); 
            builder.RegisterFilterProvider();
            builder.RegisterType<TruckManager>().As<ITruckManager>();
            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
 

        }
    }
}
