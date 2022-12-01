using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuFavorite
{
    public class MenuFavorite : Entity
    {
        
        public int UId { get; set; }
       
      
        public int MenuItemUId { set; get; }
        public int UserId { set; get; }
        public bool Active { set; get; }

        public virtual MenuItem.MenuItem MenuItem { get; set; }
    }
}
