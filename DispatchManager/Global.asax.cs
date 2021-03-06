﻿using Autofac;
using Autofac.Integration.Mvc;
using DispatchManager.Services.DispatchManage;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace DispatchManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoFac();
        }

        private void AutoFac()
        {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            builder.RegisterType<Services.DispatchManage.DispatchManager>().As<IDispatchManager>();
            builder.RegisterType<TruckAPI>().As<ITruckAPI>();


            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
