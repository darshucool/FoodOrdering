using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.RoomInfo
{
    public class RoomInfo : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "ServiceNo")]
        public string ServiceNo { set; get; }

        public string Name { set; get; }
        
        public int LocationUId { set; get; }
        public int RoomUId { set; get; }
        public int UserUId { set; get; }
     
        public bool Active { set; get; }
        public virtual UserBase UserBase { get; set; }
        public virtual RoomNo.RoomNo Room { get; set; }

    }
}
