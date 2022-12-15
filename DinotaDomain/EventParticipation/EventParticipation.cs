using Dinota.Core.Data;
using Dinota.Domain.User;
using System;
using System.ComponentModel.DataAnnotations;


namespace Dinota.Domain.EventParticipation
{
    public class EventParticipation : Entity
    {
        
        public int UId { get; set; }
       
        [Display(Name = "Name")]
        public int EventUId { set; get; }
        public int UserId { set; get; }
        public int MemCount { set; get; }
        public bool IsFamily { set; get; }
        public int IsAlcohol { set; get; }
        public int IsSpouseAlcohol { set; get; }
        public bool IsOmt { set; get; }
        public bool IsOMTVeg { set; get; }
        public bool IsParking { set; get; }
        public bool IsVeg { set; get; }
        public bool? SpouseVeg { set; get; }
        public bool Active { set; get; }
        public string VehicleNo { get; set; }
        public bool IsParticipating { get; set; }
        public string VehicleType { set; get; }
        public bool IsTransport { set; get; }
        public bool IsChangingRoom { set; get; }
        public string Remark { get; set; }
        public string AddField1 { get; set; }
        public string AddField2 { get; set; }
        public string AddField3 { get; set; }
        public virtual UserBase UserBase { get; set; }
        public int NoVegType { set; get; }
        public int SpouseNonVegType { set; get; }
        public int GuestNonVegType { set; get; }
    }
}
