using System.Data.Entity.ModelConfiguration;
using Dinota.Security.Role;

namespace Dinota.DataAccces.Role
{
    public class RoleMap : EntityTypeConfiguration<Security.Role.Role>
    {
        public RoleMap()
        {
            HasKey(role => role.Id);

            Ignore(role => role.CreationDate);
            Ignore(role => role.LastModifiedBy);
            Ignore(role => role.LastModifiedDate);
            Ignore(role => role.ExpiryDate);

            Map<ReadableRole>(role => role.Requires("TypeEnum").HasValue<byte>(1));
            Map<WritableRole>(role => role.Requires("TypeEnum").HasValue<byte>(2));

            HasMany(role => role.UserGroups).WithMany(group => group.Roles)
                .Map(m =>
                {
                    m.ToTable("GroupRole");
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("GroupId");
                });
        }

    }
}
