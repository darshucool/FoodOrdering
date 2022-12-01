using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Dinota.DataAccces.Login
{
    public class LoginMap : EntityTypeConfiguration<Security.Login.Login>
    {
        public LoginMap()
        {
            ToTable("User");

            HasKey(login => login.Id);

            //Ignore(login => login.FullName);

            Property(login => login.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
