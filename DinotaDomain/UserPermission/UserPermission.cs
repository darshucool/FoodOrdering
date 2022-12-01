using System;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;


namespace Dinota.Domain.UserPermission
{
    public class UserPermission : Entity
    {
        
        public int UId { get; set; }

       
        [Display(Name = "ObjectId")]
        public int ObjectId { set; get; }


        [Display(Name = "UserTypeUId")]
        public int UserTypeUId { set; get; }

      
        [Display(Name = "IsPermitted")]
        public bool IsPermitted { set; get; }

        
        public bool Active { set; get; }
    }
}
