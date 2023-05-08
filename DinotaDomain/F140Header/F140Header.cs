using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.F140Header
{
    public class F140Header : Entity
    {
        
        public int UId { get; set; }

          [Required]
        [Display(Name = "Name")]
        public int UserId { set; get; }
        public int MenuOrderId { set; get; }
        
        public DateTime EffectiveDate { set; get; }
        public bool Active { set; get; }
       
        public virtual MenuOrder.MenuOrder MenuOrder { get; set; }
    }
}
