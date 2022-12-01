
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class ProfileModel
    {
        public string NIC { get; set; }
        public string HouseNo { get; set; }
        public string Name { get; set; }
    }
    public class ProfileEdit {
        public string MobileNo { get; set; }
        public string LandlineNo { get; set; }
        public DateTime DOB { get; set; }
        public byte[] Img { get; set; }
        public bool IsMEP { get; set; }
        public bool IsOrganiser { get; set; }
        public bool IsParty { get; set; }
        public bool IsSamurdhi { get; set; }
    }
    public class ProfileDetailModel {
        public string DistrictName { get; set; }
        public string GramaniladariName { get; set; }
        public string KottashaName { get; set; }
        public string HouseNo { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
    }
}