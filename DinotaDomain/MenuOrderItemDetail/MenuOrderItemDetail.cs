using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuOrderItemDetail
{
    public class MenuOrderItemDetail : Entity
    {
        
        public int UId { get; set; }
        public int MeanuOrderHeaderUId { set; get; }
       
        public int MenuItemUId { set; get; }
     
        public decimal  Qty { set; get; }
        public int MeasurementUnit { set; get; }
        public string Remark { set; get; }
        public bool Active { set; get; }
        public virtual MenuOrderHeader.MenuOrderHeader MenuOrderHeader { get; set; }
        public virtual MenuItem.MenuItem MenuItem { get; set; }
    }
}
