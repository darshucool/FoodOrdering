using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.UserArea
{
    public class UserAreaMap : EntityTypeConfiguration<Domain.UserArea.UserArea>
    {
        public UserAreaMap()
        {
            ToTable("UserArea");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
