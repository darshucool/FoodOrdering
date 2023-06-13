using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.UserStatus
{
    public class UserStatus : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string Status { set; get; }
        public bool Active { set; get; }
    }
}
