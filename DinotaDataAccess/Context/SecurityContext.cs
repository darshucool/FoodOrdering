using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Dinota.DataAccces.FunctionalArea;
using Dinota.DataAccces.Group;
using Dinota.DataAccces.Login;
using Dinota.DataAccces.Role;
using Dinota.DataAccces.Tracking;
using Dinota.Security;

namespace Dinota.DataAccces.Context
{
    public class SecurityContext : DomainContext, ISecurityContext
    {
        public SecurityContext()
            : base("Alfasi", "System")
        {
        }

        public SecurityContext(string connectionString) : base(connectionString, "System")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new LoginMap());
            modelBuilder.Configurations.Add(new FunctionalAreaMap());
            modelBuilder.Configurations.Add(new TrackingMap());
        }
    }
}
