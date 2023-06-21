using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.OfficerRequest
{
    public class OfficerRequestMap : EntityTypeConfiguration<Domain.OfficerRequest.OfficerRequest>
    {
        public OfficerRequestMap()
        {
            ToTable("OfficerRequest");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.UserAccount).WithMany().HasForeignKey(f => f.UserId);
            HasRequired(f => f.ToMealInfo).WithMany().HasForeignKey(f => f.ToMeal);
            HasRequired(f => f.FromMealInfo).WithMany().HasForeignKey(f => f.FromMeal);
            HasRequired(f => f.PaymentMethodInfo).WithMany().HasForeignKey(f => f.PaymentMethod);
        }
    }
}
