using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.PaymentMethod
{
    public class PaymentMethod : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Method")]
        public string Method { set; get; }
        public bool Active { set; get; }
    }
}
