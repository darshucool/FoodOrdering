using Dinota.Domain.MenuCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class HomeModel
    {
        public int VehicleCount { get; set; }
        public decimal DrawQty { get; set; }
        public int VehicleDrawCount { get; set; }
        public List<MenuCategory> MenuCategoryList { get; set; }
      
    }
    public class SessionNaviModel
    {
        public string ObjectName { get; set; }
        public string ControllerName { get; set; }
        public bool IsPermitted { get; set; }
    }
    
}