using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.BarRecovery
{
    public class BarRecovery : Entity
    {
        
        public int UId { get; set; }

        [Required]

        public int? UserId { get; set; }
        public string ServiceNo { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal CreditAmnt { set; get; }
        public decimal CashAmnt { set; get; }
        public decimal VisaMasterAmnt { set; get; }
        public int LocationUId { get; set; }

        public bool Active { set; get; }

        public virtual UserBase UserBase { get; set; }
    }
}
