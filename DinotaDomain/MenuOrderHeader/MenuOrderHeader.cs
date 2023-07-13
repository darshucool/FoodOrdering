using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuOrderHeader
{
    public class MenuOrderHeader : Entity
    {
        
        public int UId { get; set; }

          [Required]
     
        public DateTime EffectiveDate { set; get; }
        public DateTime OrderDate { set; get; }
      public int LocationUId { get; set; }
        public string Time { set; get; }
        public string Location { set; get; }
        public bool Active { set; get; }
        public int Status { get; set; }
        public int OfficerCount { get; set; }
        public int MenuHeaderType { get; set; }
        public decimal F140TotalAmt { get; set; }
        public int PaymentMethod { get; set; }
    }
}
