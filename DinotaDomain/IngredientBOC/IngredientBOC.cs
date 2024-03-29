﻿using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.IngredientBOC
{
    public class IngredientBOC : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public int IngredientUId { set; get; }
        public int UnitId { set; get; }
     
        public decimal Price { get; set; }
      
        public decimal Qty { get; set; }
      public string VoucherNo { get; set; }
        public decimal TotalPrice { get; set; }
        public int SLAFLocationUId { get; set; }
        public bool Active { set; get; }
        public int TransactionType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int? SupplierInvoiceId { get; set; }
        public virtual IngredientInfo.IngredientInfo IngredientInfo { get; set; }
        public virtual MeasurementUnit.MeasurementUnit MeasurementUnit { get; set; }
    }
}
