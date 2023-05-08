using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.F140Header
{
    public class F140HeaderMap : EntityTypeConfiguration<Domain.F140Header.F140Header>
    {
        public F140HeaderMap()
        {
            ToTable("F140Header");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuOrder).WithMany().HasForeignKey(f => f.MenuOrderId);
        }
    }
}
