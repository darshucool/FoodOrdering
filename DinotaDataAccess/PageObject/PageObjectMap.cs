using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Dinota.DataAccces.PageObject
{
    public class PageObjectMap : EntityTypeConfiguration<Domain.PageObject.PageObject>
    {
        public PageObjectMap()
        {
            ToTable("PageObject");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           
        
        }
    }
}
