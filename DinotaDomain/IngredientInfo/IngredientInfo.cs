using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.IngredientInfo
{
    public class IngredientInfo : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public int ItemTypeId { set; get; }
        public string ItemName { set; get; }
        public int MeasurementUnitUId { set; get; }
     
        public int LocationUId { set; get; }
        public bool Active { set; get; }
        public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
    }
}
