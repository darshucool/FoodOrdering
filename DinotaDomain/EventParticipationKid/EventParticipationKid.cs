using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.EventParticipationKid
{
    public class EventParticipationKid : Entity
    {
        
        public int UId { get; set; }
       
        public int EventParticipationUId { set; get; }
        public int KidCount { set; get; }
        public int KidAge { set; get; }
        public string Veg { set; get; }
        public bool Active { set; get; }

    }
}
