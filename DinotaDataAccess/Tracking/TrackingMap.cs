using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Dinota.DataAccces.Tracking
{
    public class TrackingMap : EntityTypeConfiguration<Security.Tracking.Tracking>
    {
        public TrackingMap()
        {
            ToTable("UserLog");

            HasKey(tracking => tracking.Uid);

            Ignore(tracking => tracking.LastModifiedBy);
            Ignore(tracking => tracking.LastModifiedDate);
            Ignore(tracking => tracking.ExpiryDate);

            Property(tracking => tracking.Uid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(tracking => tracking.User)
                .WithMany()
                .HasForeignKey(tracking => tracking.UserId);

        }
    }
}
