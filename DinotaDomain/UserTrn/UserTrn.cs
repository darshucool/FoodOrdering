using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.UserTrn
{
    public class UserTrn : Entity
    {
        
        public int UId { get; set; }
      
        public int UserId { set; get; }
     
        public int StatusId { set; get; }
        public DateTime EffectiveDate { set; get; }
        public bool Active { set; get; }

        public virtual UserStatus.UserStatus UserStatus { get; set; }

    }
}
