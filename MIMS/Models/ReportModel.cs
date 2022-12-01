

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class ReportModel
    {
        public DateTime date { get; set; }
        public int SLAFLocationUId { get; set; }
    }
    public class FuelDrawModel {
        public int SLAFLocationUId { get; set; }
        public string ServiceNo { get; set; }
        public string VehicleNo { get; set; }
        public string NICNo { get; set; }
        public string Contact { get; set; }
        public bool IsUpdate { get; set; }
        
        public bool IsEnable { get; set; }
        public string dateInfo { get; set; }
    }
   
}