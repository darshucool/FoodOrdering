using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;


namespace Dinota.DataAccces.Group
{
    public class GroupMap : EntityTypeConfiguration<Security.Group.Group>
    {
        public GroupMap()
        {
            HasKey(group => group.Id);

            Property(group => group.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(group => group.Logins).WithMany(login => login.Groups)
                .Map(m =>
                {
                    m.ToTable("UserGroup");
                    m.MapLeftKey("GroupId");
                    m.MapRightKey("UserId");
                });
        }

    }
}
