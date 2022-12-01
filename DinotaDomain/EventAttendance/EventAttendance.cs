using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.EventAttendance
{
    public class EventAttendance : Entity
    {
        
        public int UId { get; set; }
       
        public int EventId { set; get; }
        public int EventParticipationUId { set; get; }
        public int KidCount { set; get; }
        public int AdultCount { set; get; }
        public int GuestCount { set; get; }  
        public int UserId { set; get; }
        public string Name { get; set; }
        public string ServiceNo { set; get; }
        public bool Active { set; get; }
        public virtual UserBase UserBase { get; set; }
    }
}
