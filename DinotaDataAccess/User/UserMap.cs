using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Dinota.Domain.User;

namespace Dinota.DataAccces.User
{
    public class UserMap : EntityTypeConfiguration<UserBase>
    {
        public UserMap()
        {
            ToTable("User");

            HasKey(user => user.Id);
            HasRequired(f => f.UserType).WithMany().HasForeignKey(f => f.UserTypeId);
            HasRequired(f => f.Division).WithMany().HasForeignKey(f => f.DivisionId);
            HasRequired(f => f.Rank).WithMany().HasForeignKey(f => f.RankUId);
            HasRequired(f => f.SLAFLocation).WithMany().HasForeignKey(f => f.LocationUId);
            HasRequired(f => f.UserStatus).WithMany().HasForeignKey(f => f.LivingStatus);
            Property(user => user.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(user => user.IsFabUser).HasColumnName("DoFabricate");

            //Ignore(user => user.FullName);
            //Ignore(user => user.Name);

            Map<Domain.AdminUser.AdminUser>(adminuser => adminuser.Requires("TypeEnum").HasValue<byte>(0));
            Map<UserAccount>(user => user.Requires("TypeEnum").HasValue<byte>(1));
          
        }
    }
}
