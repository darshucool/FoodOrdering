using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuOption
{
    public class MenuOption : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public int MenuItemUId { set; get; }
        public string Parameter { set; get; }
     
        public bool Active { set; get; }
    }
}
