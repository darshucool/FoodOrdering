using System.Data.Entity.ModelConfiguration;

namespace Dinota.DataAccces.FunctionalArea
{
    public class FunctionalAreaMap : EntityTypeConfiguration<Security.FunctionalArea.FunctionalArea>
    {
        public FunctionalAreaMap()
        {
            HasKey(functionalArea => functionalArea.Id);

            Ignore(functionalArea => functionalArea.CreationDate);
            Ignore(functionalArea => functionalArea.LastModifiedBy);
            Ignore(functionalArea => functionalArea.LastModifiedDate);
            Ignore(functionalArea => functionalArea.ExpiryDate);

            HasMany(functionalArea => functionalArea.Roles)
                .WithRequired(role => role.FunctionalArea)
                .HasForeignKey(role => role.FunctionalAreaId);
        }
    }
}
