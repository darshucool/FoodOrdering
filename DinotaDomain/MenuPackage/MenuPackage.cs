using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuPackage
{
    public class MenuPackage : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public int MenuItemId { set; get; }
        public int CombinedMenuItemId { set; get; }
        public bool Active { set; get; }

        public virtual MenuItem.MenuItem MenuItem { get; set; }
    }
}
