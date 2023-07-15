﻿using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuItem
{
    public class MenuItem : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string Name { set; get; }
        public string ImgName { set; get; }
        public string Description { get; set; }
        public int MenuCategoryUId { set; get; }
        public int SLAFLocationUId { set; get; } 
        public int MeasurementUnitId { set; get; }
        public bool Active { set; get; }
        public decimal GasChargePercent { get; set; }
        public bool IsCombine { set; get; }
        public int MenuTypeId { set; get; }
        public decimal PortionQty { get; set; }
        public virtual MenuCategory.MenuCategory MenuCategory { get; set; }
        public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
    }
}
