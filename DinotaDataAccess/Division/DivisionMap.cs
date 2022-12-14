using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.Division
{
    public class DivisionMap : EntityTypeConfiguration<Domain.Division.Division>
    {
        public DivisionMap()
        {
            ToTable("Division");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
