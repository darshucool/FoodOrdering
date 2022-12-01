using System;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;


namespace Dinota.Domain.PageObject
{
    public class PageObject : Entity
    {
        
        public int UId { get; set; }


        [Display(Name = "ObjectName")]
        public string ObjectName { set; get; }


        [Display(Name = "Description")]
        public string Description { set; get; }

        [Display(Name = "ControllerName")]
        public string ControllerName { set; get; }


        [Display(Name = "ParentUId")]
        public int ParentUId { set; get; }

        
        public bool Active { set; get; }
    }
}
