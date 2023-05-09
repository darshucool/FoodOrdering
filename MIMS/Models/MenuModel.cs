
using Dinota.Domain.F140Header;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuItemDetail;
using Dinota.Domain.MenuMultiOption;
using Dinota.Domain.MenuOption;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.MenuPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class MenuModel
    {
        public MenuCategory oMenuCategory { get; set; }
        public List<MenuItemModel> MenuItemModel { get; set; }
    }
    public class MenuItemModel {
        public MenuItem MenuItem { get; set; }
        public List<MenuPackage> MenuPackageList { get; set; }
    }
    public class MenuClientOrderModel { 
        public MenuItem MenuItem { get; set; }

        public int LocationId { get; set; }
        public List<MenuOption> MenuOptionList { get; set; }
        public List<MenuMultiOption> MenuMultiOptionList { get; set; }
    }
    public class MenuBOCModel {
        public int MenuOrderId { get; set; }
        public List<MenuItemDetailModel> MenuItemList { get; set; }

    }
    public class MenuItemDetailModel {
        public int MenuItemId { get;set;}
        public MenuItem MenuItem { get; set; }
        public List<MenuItemDetail> MenuItemDetailList { get; set; }
    }

    public class F140Model
    {
        public F140Header F140Header { get; set; }
        public decimal totalAmount { get; set; }
    }
    public class MessBillModel { 
        public decimal CurrentAmount { get; set; }
        public List<MenuOrder> MenuOrders { get; set; }
    }
}