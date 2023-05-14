using Dinota.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.MenuOrderOfficer
{
    public class MenuOrderOfficer : Entity
    {
        
        public int UId { get; set; }
       
       
        public int MeanuOrderHeaderUId { set; get; }
     
        public int UserId { set; get; }
        public bool Active { set; get; }

    }
}
