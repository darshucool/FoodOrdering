using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuOrderExtraItemDetail
{
    public class MenuOrderExtraItemDetail : Entity
    {
        
        public int UId { get; set; }
        public int MeanuOrderHeaderUId { set; get; }
       
        public int OtherItemUId { set; get; }
        public int Status { get; set; }
       
        public decimal  Qty { set; get; }
        public int MeasurementUnit { set; get; }
        public string Remark { set; get; }
        public bool Active { set; get; }
        public virtual MenuOrderHeader.MenuOrderHeader MenuOrderHeader { get; set; }
        public virtual IngredientInfo.IngredientInfo IngredientInfo { get; set; }
    }
}
