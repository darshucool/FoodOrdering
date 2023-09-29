using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.EventCount
{
    public class EventCount : Entity
    {
        
        public int UId { get; set; }
      
        public int Count { set; get; } 
        public DateTime EffectiveDate { set; get; }
        public int Type { set; get; }
        //public int LocationId { set; get; }
        public bool Active { set; get; }
        //public virtual SLAFLocation.SLAFLocation SLAFLocation { get; set; }

    }
}
