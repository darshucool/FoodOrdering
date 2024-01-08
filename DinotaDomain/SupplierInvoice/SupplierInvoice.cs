using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.SupplierInvoice
{
    public class SupplierInvoice : Entity
    {
        
        public int UId { get; set; }

      
        public int SupplierUId { set; get; }
        public DateTime EffectiveDate { set; get; }
        public decimal Tax { set; get; }
        public decimal Discount { set; get; }
        public decimal SubTotal { set; get; }
        public decimal GrandTotal { set; get; }
        public int LocationUId { get; set; }
        public int Status { get; set; }

        public bool Active { set; get; }
       
    public virtual Supplier.Supplier Supplier { get; set; }
    }
}
