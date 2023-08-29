using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.SetMenuDetail
{
    public class SetMenuDetail : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public int SetMenuHeaderId { set; get; }
        public int MenuItemId { set; get; }
     
        public int LocationUId { set; get; }
        public bool Active { set; get; }
        public virtual SetMenuHeader.SetMenuHeader SetMenuHeader { get; set; }
        public virtual MenuItem.MenuItem MenuItem { get; set; }
    }
}
