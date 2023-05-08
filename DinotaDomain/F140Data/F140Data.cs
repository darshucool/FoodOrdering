using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.F140Data
{
    public class F140Data : Entity
    {
        
        public int UId { get; set; }

        
        public int F140HeaderUId { set; get; }
        public int MenuItemId { set; get; }
        public int IngridientUId { set; get; }
        public decimal Qty { set; get; }
        public int MeasurementUnitId { set; get; }
        public int SLAFLocationId { set; get; }
        public decimal Amount { set; get; }
        public bool Active { set; get; }
        
        public virtual MenuItem.MenuItem MenuItem { get; set; }
        public virtual IngredientInfo.IngredientInfo IngredientInfo { get; set; }

        public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
    }
}
