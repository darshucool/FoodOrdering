using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MainCourseMealHeader
{
    public class MainCourseMealHeader : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string MealName { set; get; }
        public int MealId { set; get; }
        public int MenuItemId { set; get; }


        public int LocationUId { set; get; }
        public bool Active { set; get; }
        //public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
    }
}
