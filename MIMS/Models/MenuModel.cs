
using Dinota.Domain.F140Header;
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuItemDetail;
using Dinota.Domain.MenuMultiOption;
using Dinota.Domain.MenuOption;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.MenuOrderHeader;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.MenuOrderOfficer;
using Dinota.Domain.MenuPackage;
using Dinota.Domain.User;
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
        public MenuOrderItemDetail Detail { get; set; }
        public List<MenuItemDetail> MenuItemDetailList { get; set; }
        public List<MenuItemDetailListModel> MenuItemDetailListModelList { get; set; }
    }
    public class MenuItemDetailListModel {
        public MenuItemDetail MenuItemDetail { get; set; }
        public decimal CurrentStockQty { get; set; }
        public IngredientInfo IngredientInfo { get; set; }
    }
    public class MenuOrderHeaderModel { 
        public MenuOrderHeader MenuOrderHeader { get; set; }
        public F140Header F140Header { get; set; }
        public decimal TotalAmount { get; set; }
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
    public class OfficerMenuOrderModel {
        public MenuItem MenuItem { get; set; }
        public IngredientInfo IngredientInfo { get; set; }
        public List<MenuItem> MenuItemList { get; set; }
        public MenuOrderHeader MenuOrderHeader { get; set; }
        public List<MenuOrderItemDetail> MenuOrderItemDetailList { get; set; }
        public List<IngredientInfo> IngredientInfoList { get; set; }
        public List<MenuOrderOfficer> MenuOrderOfficerList { get; set; }
        public List<UserAccount> UserAccountList { get; set; }
        public int MenuOrderId { get; set; }
        public int MenuItemId { get; set; }
    }
}