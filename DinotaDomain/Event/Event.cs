using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.Event
{
    public class Event : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public string EventName { set; get; }
        public byte[] Img { set; get; }
        public string Description { get; set; }
        public bool Active { set; get; }
        public int SLAFLocationUId { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
