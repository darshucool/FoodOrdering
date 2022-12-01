using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.FuelType
{
    public class FuelType : Entity
    {
        
        public int UId { get; set; }
        public string Name { get; set; }
        public bool Active { set; get; }


    }
}
