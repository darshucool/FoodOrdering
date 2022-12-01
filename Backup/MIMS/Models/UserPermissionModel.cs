using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dinota.Security.Group;
using Dinota.Security.FunctionalArea;
using Dinota.Domain.PageObject;
using Dinota.Domain.UserType;

namespace Dinota.Models
{
    
    public class PermissionModel
    {
        public int UserTypeId { set; get; }

        public UserType UserType { set; get; }
        public List<PageObject> ParentObjectList { set; get; }
        public List<PageObject> PageObjectList { set; get; }
    }
}