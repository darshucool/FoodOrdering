using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.StockSheetTransaction
{
    public class StockSheetTransaction : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "EffectiveDate")]
        public DateTime EffectiveDate { set; get; }
        public int IngredientUId { set; get; }
        public decimal BOCAmount { get; set; }
        public decimal IssueAmount { get; set; }
        public bool Active { set; get; }

        public virtual IngredientInfo.IngredientInfo IngredientInfo { get; set; }
    }
}
