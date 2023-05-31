using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.BOCTransaction
{
    public class BOCTransaction : Entity
    {
        
        public int UId { get; set; }
       
      
        public int IngriedientBOCUId { set; get; }
       
        public decimal PresentStock { set; get; }
      
        public decimal IssueStock { get; set; }
      
        public decimal RemainingStock { get; set; }
        public bool Active { set; get; }
        public DateTime EffectiveDate { get; set; }
        public int MenuOrderUId { get; set; }

        public virtual MenuOrderHeader.MenuOrderHeader MenuOrderHeader { get; set; }
        public virtual IngredientBOC.IngredientBOC IngredientBOC { get; set; }
    }
}
