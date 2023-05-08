﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuItemDetail
{
    public class MenuItemDetailMap : EntityTypeConfiguration<Domain.MenuItemDetail.MenuItemDetail>
    {
        public MenuItemDetailMap()
        {
            ToTable("MenuItemDetail");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.IngredientInfo).WithMany().HasForeignKey(f => f.IngriedientUId);
            HasRequired(f => f.MeasurementUnit).WithMany().HasForeignKey(f => f.IngriedientMeasurementUId);
        }
    }
}