using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.OfficerRequest
{
    public class OfficerRequest : Entity
    {
        
        public int UId { get; set; }

          [Required]
        [Display(Name = "User")]
        public int UserId { set; get; }
        public DateTime? FromDate { set; get; }
        public int? FromMeal { set; get; }
        public DateTime? ToDate { set; get; }
        public int? ToMeal { set; get; }
      
        public int? PaymentMethod { get; set; }
        public bool Active { set; get; }

       public virtual UserAccount UserAccount { get; set; }
        public virtual MenuItem.MenuItem FromMealInfo { get; set; }
        public virtual MenuItem.MenuItem ToMealInfo { get; set; }
        public virtual PaymentMethod.PaymentMethod PaymentMethodInfo { get; set; }
    }
}
