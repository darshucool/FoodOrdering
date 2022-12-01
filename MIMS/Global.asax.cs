using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MIMS.Models;
using MIMS.Util;
using AutoMapper;
using Autofac;
using Autofac.Integration.Mvc;
using Dinota.Core.Ioc;
using Dinota.DataAccces.Context;
using Dinota.Domain;
using Dinota.Security;

namespace MIMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterDependencies();


            Helpers.MenuBuilder.Setup();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());


            AuthConfig.RegisterAuth();

            RegisterRoutes(RouteTable.Routes);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{handler}.ashx");
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DomainContext>().As<IDomainContext>().AsSelf()
                .InstancePerHttpRequest()
                .WithParameter(DomainContextParamResolver.ParaSelector, DomainContextParamResolver.ValueProvider);


            builder.RegisterType<SecurityContext>().As<ISecurityContext>().AsSelf()
                .InstancePerHttpRequest();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // ReSharper disable RedundantNameQualifier
            builder.RegisterModule(new Dinota.Domain.AutofacModule());
            builder.RegisterModule(new Dinota.DataAccces.AutofacModule());
            builder.RegisterModule(new Dinota.Security.AutofacModule());

            var container = builder.Build();

            var autofacDependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(autofacDependencyResolver);

            MIMS.Configuration.Instance.AutofacDependencyResolver = autofacDependencyResolver;

            Dinota.Core.Configuration.Instance.Container = new AutofacContainerAdapter(Configuration.Instance.GetLifetimeScope);
            Dinota.Domain.Configuration.Instance.Container = new AutofacContainerAdapter(Configuration.Instance.GetLifetimeScope);
            Dinota.DataAccces.Configuration.Instance.Container = new AutofacContainerAdapter(Configuration.Instance.GetLifetimeScope);

            //Mapper.CreateMap<ProjectHierarchy, ProjectHierarchyEditModel>();
            //Mapper.CreateMap<ProjectHierarchy, ProjectHierarchyCreateModel>();
            //Mapper.CreateMap<Project, ProjectSearchModel>();
        }

        public static class DomainContextParamResolver
        {
            public static bool ParaSelector(ParameterInfo parameterInfo, IComponentContext componentContext)
            {
                return true;
            }

            public static object ValueProvider(ParameterInfo parameterInfo, IComponentContext componentContext)
            {
                switch (parameterInfo.Name)
                {
                    case "connectionString":
                        return "Alfasi";
                    case "userName":
                        return HttpContext.Current.User.Identity.Name;
                    default:
                        throw new ArgumentOutOfRangeException(parameterInfo.Name);
                }
            }
        }
    }
}