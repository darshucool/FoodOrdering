using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Dinota.DataAccces.UserPermission
{
    public class UserPermissionMap : EntityTypeConfiguration<Domain.UserPermission.UserPermission>
    {
        public UserPermissionMap()
        {
            ToTable("UserPermission");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           
        
        }
    }
}
