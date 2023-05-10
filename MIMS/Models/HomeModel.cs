using Dinota.Domain.BOCTransaction;
using Dinota.Domain.Event;
using Dinota.Domain.EventParticipation;
using Dinota.Domain.EventParticipationKid;
using Dinota.Domain.IngredientBOC;
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuFavorite;
using Dinota.Domain.MenuOrder;
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
        public List<MenuFavorite> MenuFavoriteList { get; set; }
        public int SLAFLocationUId { get; set; }
    }
    public class SessionNaviModel
    {
        public string ObjectName { get; set; }
        public string ControllerName { get; set; }
        public bool IsPermitted { get; set; }
    }
    public class MenuOrderModel {
        public List<MenuOrder> PendingMenuOrderList { get; set; }
        public List<MenuOrder> CompleteMenuOrderList { get; set; }
        public List<MenuOrder> CancelledMenuOrderList { get; set; }
        public List<MenuOrder> DeliveredMenuOrderList { get; set; }
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
    public class StockSummaryModel {
        public IngredientInfo IngredientInfo { get; set; }
        public List<IngredientBOC> IngredientBOCList { get; set; }
        public List<BOCTransaction> BOCTransactionList { get; set; }
    }
}