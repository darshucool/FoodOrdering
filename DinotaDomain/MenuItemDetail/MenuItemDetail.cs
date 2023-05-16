using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuItemDetail
{
    public class MenuItemDetail : Entity
    {
        
        public int UId { get; set; }
       
      
        public int MenuItemId { set; get; }
        public decimal PortionQty { set; get; }
     public int PortionMeasurementUId { set; get; }
        public int IngriedientUId { set; get; }
        public int IngriedientMeasurementUId { set; get; }
        public int SLAFLocationUId { set; get; } 
        public decimal IngriedientQty { set; get; }
        public bool Active { set; get; }
        public virtual MenuItem.MenuItem MenuItem { get; set; }
        public virtual IngredientInfo.IngredientInfo IngredientInfo { get; set; }
        public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
        public virtual MeasurementUnit.MeasurementUnit MenuItemMeasurementUnit { get; set; }
    }
}
