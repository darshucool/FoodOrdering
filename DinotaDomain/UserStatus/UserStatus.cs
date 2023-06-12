using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.UserStatus
{
    public class UserStatus : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string UserStatusName { set; get; }
        public byte[] Img { set; get; }
        public string Description { get; set; }
        public bool Active { set; get; }
        public int SLAFLocationUId { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
