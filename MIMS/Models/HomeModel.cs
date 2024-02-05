using Dinota.Domain.AlertNotify;
using Dinota.Domain.BOCTransaction;
using Dinota.Domain.Event;
using Dinota.Domain.EventParticipation;
using Dinota.Domain.EventParticipationKid;
using Dinota.Domain.IngredientBOC;
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuFavorite;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.MenuOrderOfficer;
using Dinota.Domain.UserArea;
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
        public List<MenuOrder> MenuOrderList { get; set; }
        public List<MenuOrder> PastOrderList { get; set; }
        public List<AlertNotify> AlertNotifyList { get; set; }
        public List<MenuFavorite> MenuFavoriteList { get; set; }
        public int SLAFLocationUId { get; set; }
    }
    public class SessionNaviModel
    {
        public string ObjectName { get; set; }
        public string ControllerName { get; set; }
        public bool IsPermitted { get; set; }
    }
    public class MenuOrderModel
    {
        public List<MenuDetailItemOfficerModel> PendingMenuOrderList { get; set; }
        public List<MenuDetailItemOfficerModel> CompleteMenuOrderList { get; set; }
        public List<MenuDetailItemOfficerModel> CancelledMenuOrderList { get; set; }
        public List<MenuDetailItemOfficerModel> DeliveredMenuOrderList { get; set; }

        public List<MenuOrderViewModel> PendingViewMenuOrderList { get; set; }
        public List<MenuOrderViewModel> CompleteViewMenuOrderList { get; set; }
        public List<MenuOrderViewModel> CancelledViewMenuOrderList { get; set; }
        public List<MenuOrderViewModel> DeliveredViewMenuOrderList { get; set; }
    }

    public class AcceptedMenuOrderModel
    {
        public List<MenuDetailItemOfficerModel> CompleteMenuOrderList { get; set; }

        public List<MenuOrderViewModel> CompleteViewMenuOrderList { get; set; }

    }

    public class MenuDetailItemOfficerModel {
        public MenuOrderItemDetail MenuOrderItemDetail { get; set; }
        public List<MenuOrderOfficer> MenuOrderOfficerList { get; set; }
    }
    public class MenuOrderViewModel {
        public MenuOrderItemDetail PendingOrder { get; set; }
        public List<MenuOrderOfficer> MenuOrderOfficerList { get; set; }
    }
    public class EventParticipateModel
    {
        public Event Event { get; set; }
        public int RankUId { get; set; }
        public string UserMode { get; set; }
        public bool IsParticipated { get; set; }
        public bool ParticipationSubmit { get; set; }
        public EventParticipation EventParticipation { get; set; }
        public List<EventParticipationKid> EventParticipationKidList { get; set; }
    }
    public class ItemDetailModel { 
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int IngridientId { get; set; }
        public StockSummaryModel StockSummaryModel { get; set; }
    }
    public class StockSummaryModel {
        public IngredientInfo IngredientInfo { get; set; }
        public List<IngredientBOC> IngredientBOCList { get; set; }
        public List<BOCTransaction> BOCTransactionList { get; set; }
    }
    public class MenuSearchModel {
        public List<MenuItem> MenuItemList { get; set; }
    }

    public class UserPermissionModel { 
        public List<UserArea> UserAreaList { get; set; }

    }
}