using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.SetMenuDetail
{
    public class SetMenuDetailMap : EntityTypeConfiguration<Domain.SetMenuDetail.SetMenuDetail>
    {
        public SetMenuDetailMap()
        {
            ToTable("SetMenuDetail");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.SetMenuHeader).WithMany().HasForeignKey(f => f.SetMenuHeaderId);
            HasRequired(f => f.MenuItem).WithMany().HasForeignKey(f => f.MenuItemId);

        }
    }
}
