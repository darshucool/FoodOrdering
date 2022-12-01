using MIMS.Security.SiteMap;
using Autofac;
using Autofac.Integration.Mvc;

namespace MIMS
{
    internal class Configuration
    {
        private static Configuration _configuration;

        private Configuration()
        {
            
        }

        public SiteMap TopMenu { get; set; }

        public SiteMap LoginMenu { get; set; }

        public static Configuration Instance
        {
            get
            {
                return _configuration ?? (_configuration = new Configuration());
            }
        }

        public ILifetimeScope Container
        {
            get
            {
                return GetLifetimeScope();
            }
        }

        public AutofacDependencyResolver AutofacDependencyResolver { get; set; }

        public ILifetimeScope GetLifetimeScope()
        {
            return AutofacDependencyResolver.RequestLifetimeScope;
        }

        public static string ConnectionString
        {
            get
            {
                string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["Alfasi"].ConnectionString;
                return connectionstring;
            }
        }


    }
}