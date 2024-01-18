using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.UserPermissionGroup
{
    public class UserPermissionGroup : Entity
    {
        
        public int UId { get; set; }
       public int UserTypeUId { get; set; }
        public string UserArea { get; set; }
        public bool IsPermitted { get; set; }
        public bool Active { get; set; }
    }
}
