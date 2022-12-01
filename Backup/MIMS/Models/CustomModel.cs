
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class CustomModel
    {
    }
    public class UserProfileSummry {
        public string Name { get; set; }
        public string Appointment { get; set; }
        public byte[] ImagData { get; set; }
    }
    public class CustomServiceProfile
    {
        public int UId { get; set; }
        public int RefUId { get; set; }
        public int RankUId { get; set; }
        public int DistrictUId { get; set; }
        public int ForceTypeUId { get; set; }
        public string ServiceNo { get; set; }
        public string TeleNo { get; set; }
        public string NIC { get; set; }
        public string RankName { get; set; }
        public string ServicePercent { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public string Type { get; set; }
        public int IsExist { get; set; }
    }
   
}