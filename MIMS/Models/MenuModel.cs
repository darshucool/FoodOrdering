
using Dinota.Domain.F140Data;
using Dinota.Domain.F140Header;
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuItemDetail;
using Dinota.Domain.MenuMultiOption;
using Dinota.Domain.MenuOption;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.MenuOrderExtraItemDetail;
using Dinota.Domain.MenuOrderHeader;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.MenuOrderOfficer;
using Dinota.Domain.MenuPackage;
using Dinota.Domain.PaymentInfo;
using Dinota.Domain.SetMenuDetail;
using Dinota.Domain.SetMenuHeader;
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
    public class MenuPackageModel {
        public List<MenuPackage> MenuPackageList { get; set; }
    }
    public class MenuItemModel {
        public MenuItem MenuItem { get; set; }
        public List<MenuPackage> MenuPackageList { get; set; }
    }
    public class MenuPackageEditModel {
        public int MenuItemId { get; set; }
        public List<MenuItem> MenuItemList { get; set; }
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
        public decimal MultipleQty { get; set; }
    }
    public class MenuItemDetailModel {
        public int MenuItemId { get; set; }
        public int IngredientInfoId { get; set; }

        public int MenuItemType { get; set; }
        public MenuItem MenuItem { get; set; }
        public IngredientInfo IngredientInfo { get; set; }
        public MenuOrderItemDetail Detail { get; set; }
        public MenuOrderExtraItemDetail ExtraDetail { get; set; }
        public List<MenuItemDetail> MenuItemDetailList { get; set; }
        public List<MenuItemDetailListModel> MenuItemDetailListModelList { get; set; }
    }
    public class MenuItemDetailListModel {
        public MenuItemDetail MenuItemDetail { get; set; }
       
        public decimal CurrentStockQty { get; set; }
        public IngredientInfo IngredientInfo { get; set; }
    }
    public class OrdeerListViewModel { 
        public DateTime EffectiveDate { get; set; }
        public DateTime OrderDate { get; set; }
        public List<MenuOrderHeaderModel> MenuOrderHeaderModelList { get; set; }
        

    }
    public class MenuOrderHeaderModel {
        public DateTime EffectiveDate { get; set; }
        public MenuOrderHeader MenuOrderHeader { get; set; }
        public F140Header F140Header { get; set; }
        public List<MenuOrderItemDetail> MenuOrderItemDetailList { get; set; }
        public List<MenuOrderOfficer> MenuOrderOfficerList{ get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public List<MenuDetailItemOfficerModel> DeliveredMenuOrderList { get; set; } 
      
    }


    public class F140Model
    {
        public F140Header F140Header { get; set; }
        public decimal totalAmount { get; set; }
    }
    public class MessBillModel {
        public decimal CurrentAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsDataSubmit { get; set; }
        public List<MenuOrderHeaderDetailModel> MenuOrders { get; set; }
    }
    public class MenuOrderHeaderDetailModel {
        public MenuOrderHeader MenuOrderHeader { get; set; }
        public decimal MyAmount { get; set; }
        public F140Header F140Header { get; set; }
        public List<MenuOrderItemDetail> MenuOrderItemDetailList { get; set; }
    }
    public class OfficerMenuOrderModel {
        public MenuItem MenuItem { get; set; }
        public IngredientInfo IngredientInfo { get; set; }
        public List<MenuItem> MenuItemList { get; set; }
        public bool IsMenuItem { get; set; }
        public MenuOrderHeader MenuOrderHeader { get; set; }
        public List<MenuOrderItemDetail> MenuOrderItemDetailList { get; set; }
        public List<MenuOrderExtraItemDetail> MenuOrderOtherItemDetailList { get; set; }
        public List<IngredientInfo> IngredientInfoList { get; set; }
        public List<MenuOrderOfficer> MenuOrderOfficerList { get; set; }
        public List<UserAccount> UserAccountList { get; set; }
        public int MenuOrderId { get; set; }
        public int MenuItemId { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
    public class StockCheckModel {
        public string ServiceNo { get; set; }
        public string EffectiveDate { get; set; }
        public string Remark { get; set; }
        public List<IngriendientStockInfo> IngredientInfoList { get; set; }
    }

    public class IngriendientStockInfo
    {
        public string ItemName { get; set; }
        public decimal CurrentStock { get; set; }
        public string Unit { get; set; }
        public string PresentStock { get; set; }
    }

    public class OffcerRecoveryList
    {
        public UserAccount oUserAccount { get; set; }
        public decimal MessBill { get; set; }
    }

    public class SetMenuModel { 
        public SetMenuHeader SetMenuHeader { get; set; }
        public string SetMenuName { get; set; }
        public List<SetMenuDetail> SetMenuDetailList { get; set; }
        public List<MenuItem> MenuItemList { get; set; }
        public List<SetMenuHeader> SetMenuHeaderList { get; set; }

        public int MenuItemId { get; set; }
    }
    public class MenuF140Model {
        public List<F140Data> F140DataList { get; set; }
        public List<MenuOrderOfficer> MenuOrderOfficerList { get; set; }
    }
}