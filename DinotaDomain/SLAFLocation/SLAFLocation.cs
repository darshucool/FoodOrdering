using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.SLAFLocation
{
    public class SLAFLocation : Entity
    {
        
        public int UId { get; set; }
        public string Name { get; set; }
        
        public string MessName { get; set; }
        public bool Active { set; get; }


    }
}
