using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MeasurementUnit
{
    public class MeasurementUnit : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Unit")]
        public string Unit { set; get; }
     
        public bool Active { set; get; }


    }
}
