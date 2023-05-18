using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuOrderItemDetail
{
    public class MenuOrderItemDetailMap : EntityTypeConfiguration<Domain.MenuOrderItemDetail.MenuOrderItemDetail>
    {
        public MenuOrderItemDetailMap()
        {
            ToTable("MenuOrderItemDetail");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuItem).WithMany().HasForeignKey(f => f.MenuItemUId);
            HasRequired(f => f.MenuOrderHeader).WithMany().HasForeignKey(f => f.MeanuOrderHeaderUId);
            


        }
    }
}
