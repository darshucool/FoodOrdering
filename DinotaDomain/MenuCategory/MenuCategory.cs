using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuCategory
{
    public class MenuCategory : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string Name { set; get; }
        public string ImgName { set; get; }
     public int SLAFLocationUId { set; get; }
        public bool Active { set; get; }

        public int MenuTypeId { get; set; }
    }
}
