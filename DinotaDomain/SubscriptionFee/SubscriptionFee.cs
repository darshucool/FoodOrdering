using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.SubscriptionFee
{
    public class SubscriptionFee : Entity
    {
        
        public int UId { get; set; }

          [Required]
        [Display(Name = "Name")]
        public string Category { set; get; }
        public decimal Fee { set; get; }
        public int LocationId { set; get; }
        public bool Active { set; get; }


    }
}
