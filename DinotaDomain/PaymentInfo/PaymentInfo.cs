using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.PaymentInfo
{
    public class PaymentInfo : Entity
    {
        
        public int UId { get; set; }

        public int MenuOrderHeaderId { set; get; }
        public decimal PaymentAmount { set; get; }
        public int PaymentMethodId { set; get; }
        public string PaymentReference { set; get; }
        public DateTime PaymentDate { get; set; }
        public int SLAFLocationId { set; get; }
        public bool Active { set; get; }


    }
}
