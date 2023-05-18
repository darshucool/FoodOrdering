﻿using Dinota.Domain.BOCTransaction;
using Dinota.Domain.Event;
using Dinota.Domain.EventParticipation;
using Dinota.Domain.EventParticipationKid;
using Dinota.Domain.IngredientBOC;
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuFavorite;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.MenuOrderOfficer;
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
        public List<MenuOrderItemDetail> PendingMenuOrderList { get; set; }
        public List<MenuOrderItemDetail> CompleteMenuOrderList { get; set; }
        public List<MenuOrderItemDetail> CancelledMenuOrderList { get; set; }
        public List<MenuOrderItemDetail> DeliveredMenuOrderList { get; set; }

        public List<MenuOrderViewModel> PendingViewMenuOrderList { get; set; }
        public List<MenuOrderViewModel> CompleteViewMenuOrderList { get; set; }
        public List<MenuOrderViewModel> CancelledViewMenuOrderList { get; set; }
        public List<MenuOrderViewModel> DeliveredViewMenuOrderList { get; set; }
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
    public class StockSummaryModel {
        public IngredientInfo IngredientInfo { get; set; }
        public List<IngredientBOC> IngredientBOCList { get; set; }
        public List<BOCTransaction> BOCTransactionList { get; set; }
    }
}