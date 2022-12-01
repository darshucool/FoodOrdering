using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.RoomNo
{
    public class RoomNo : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "RoomNo")]
        public string Room { set; get; }
     
        public bool Active { set; get; }


    }
}
