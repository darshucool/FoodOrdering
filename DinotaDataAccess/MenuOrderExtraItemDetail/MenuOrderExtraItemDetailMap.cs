using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuOrderExtraItemDetail
{
    public class MenuOrderExtraItemDetailMap : EntityTypeConfiguration<Domain.MenuOrderExtraItemDetail.MenuOrderExtraItemDetail>
    {
        public MenuOrderExtraItemDetailMap()
        {
            ToTable("MenuOrderExtraItemDetail");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.IngredientInfo).WithMany().HasForeignKey(f => f.OtherItemUId);
            HasRequired(f => f.MenuOrderHeader).WithMany().HasForeignKey(f => f.MeanuOrderHeaderUId);
            


        }
    }
}
