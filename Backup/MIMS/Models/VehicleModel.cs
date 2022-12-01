
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class VehicleModel
    {
        public string VehicleNo { get; set; }
        public string ServiceNo { get; set; }
        public decimal TotalPumped { get; set; }
        public decimal Remaining { get; set; }
    }
}