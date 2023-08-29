using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.SetMenuHeader
{
    public class SetMenuHeader : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string SetMenuName { set; get; }
     
        public int LocationUId { set; get; }
        public bool Active { set; get; }
        //public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
    }
}
