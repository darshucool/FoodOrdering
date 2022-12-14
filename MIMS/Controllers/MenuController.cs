using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MIMS.Helpers;

using MIMS.Security;
using Excel = Microsoft.Office.Interop.Excel;
using Dinota.Core.Data;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Core.Specification;
using Dinota.Security;
using System;
using AlfasiWeb.Models;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.Event;
using System.Web;
using System.IO;
using Dinota.Domain.EventParticipation;
using Dinota.Domain.EventParticipationKid;
using Dinota.Domain.EventAttendance;
using System.Diagnostics;
using Dinota.Domain.MenuOption;
using System.Security.Principal;
using Dinota.Domain.MeasurementUnit;
using Dinota.Domain.Town;
using Dinota.Domain.MenuMultiOption;

namespace MIMS.Controllers
{
    //[Authorize]
    // [ExternalUserRedirectAttribute]
    public class MenuController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly MenuCategoryService _menuCategoryService;
        private readonly MenuItemService _menuItemService;
        private readonly MenuOrderService _menuOrderService;
        private readonly EventService _eventService;
        private readonly EventParticipationService _eventParticipationService;
        private readonly EventParticipationKidService _eventParticipationKidService;
        private readonly EventAttendanceService _eventAttendanceService;
        private readonly MenuOptionService _menuOptionService;
        private readonly MeasurementUnitService _measurementUnitService;
        private readonly MenuMultiOptionService _menuMultiOptionService;
        public MenuController(IDomainContext dataContext, MenuMultiOptionService menuMultiOptionService, MeasurementUnitService measurementUnitService, MenuOptionService menuOptionService, EventAttendanceService eventAttendanceService, EventParticipationKidService eventParticipationKidService, EventParticipationService eventParticipationService, EventService eventService, MenuOrderService menuOrderService, MenuItemService menuItemService, MenuCategoryService menuCategoryService, UserAccountService userAccountService)
            : base(dataContext)
        {
            _userAccountService = userAccountService;
            _menuCategoryService = menuCategoryService;
            _menuCategoryService = menuCategoryService;
            _menuItemService = menuItemService;
            _menuOrderService = menuOrderService;
            _eventService = eventService;
            _eventParticipationService = eventParticipationService;
            _eventParticipationKidService = eventParticipationKidService;
            _eventAttendanceService = eventAttendanceService;
            _menuOptionService = menuOptionService;
            _measurementUnitService = measurementUnitService;
            _menuMultiOptionService = menuMultiOptionService;
        }
        //[AuthorizeUserAccessLevel()]
        public ActionResult MenuItemIndex(int id)
        {
            MenuModel model = new MenuModel();
            try
            {
               
                model.oMenuCategory = _menuCategoryService.GetByKey(id);
                List<MenuItemModel> MenuItemModelList = new List<MenuItemModel>();
                var filter = _menuItemService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuCategoryUId == id);
                List<MenuItem> MenuItemList = _menuItemService.GetCollection(filter, p => p.CreationDate).ToList();
                foreach (MenuItem item in MenuItemList)
                {
                    MenuItemModel mod = new MenuItemModel();
                    mod.MenuItem = item;
                    MenuItemModelList.Add(mod);
                }
                model.MenuItemModel = MenuItemModelList;
            }
            catch (Exception)
            {

                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");
            }
            

            return View(model);
        }
        public void BindMeasurementUnitList()
        {
            try
            {
                var filter = _measurementUnitService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _measurementUnitService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Unit");
                ViewData[ViewDataKeys.MeasurementUnitListList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());
            }
        }
        public ActionResult MyOrders()
        {
            UserAccount account = GetCurrentUser();
            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }
            MenuOrderModel model = new MenuOrderModel();
            var filter = _menuOrderService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p => p.Status == 10).And(p => p.UserId == account.Id);
            List<MenuOrder> MenuItemList = _menuOrderService.GetCollection(filter, p => p.CreationDate).OrderByDescending(p=>p.CreationDate).ToList();
            model.PendingMenuOrderList = MenuItemList;

            var filterC = _menuOrderService.GetDefaultSpecification();
            filterC = filterC.And(p => p.Active == true).And(p => p.Status == 20).And(p => p.UserId == account.Id);
            List<MenuOrder> CompMenuItemList = _menuOrderService.GetCollection(filterC, p => p.CreationDate).OrderByDescending(p => p.CreationDate).ToList();
            model.CompleteMenuOrderList = CompMenuItemList;

            var filterCan = _menuOrderService.GetDefaultSpecification();
            filterCan = filterCan.And(p => p.Active == true).And(p => p.Status == 30).And(p => p.UserId == account.Id);
            List<MenuOrder> CancelledMenuItemList = _menuOrderService.GetCollection(filterCan, p => p.CreationDate).OrderByDescending(p => p.CreationDate).ToList();
            model.CancelledMenuOrderList = CancelledMenuItemList;

            var filterDeli = _menuOrderService.GetDefaultSpecification();
            filterDeli = filterDeli.And(p => p.Active == true).And(p => p.Status == 40).And(p => p.UserId == account.Id);
            List<MenuOrder> DeliveredMenuItemList = _menuOrderService.GetCollection(filterDeli, p => p.CreationDate).OrderByDescending(p => p.CreationDate).ToList();
            model.DeliveredMenuOrderList = DeliveredMenuItemList;

            return View(model);
        }
        public ActionResult UpdateEventStatus(int id)
        {
              EventParticipateModel model = new EventParticipateModel();
                try
                {
                    EventParticipation oEventParticipation = _eventParticipationService.GetByKey(id);
                    model.EventParticipation = oEventParticipation;
                    UserAccount acc = GetCurrentUser();
                    model.RankUId = acc.RankUId;
                model.UserMode = acc.UserMode.ToString();
                    DateTime date = DateTime.Now;
                    var filter = _eventService.GetDefaultSpecification();
                    filter = filter.And(p => p.Active == true).And(p => p.UId == oEventParticipation.EventUId);
                    Event EventList = _eventService.GetBy(filter);
                    model.Event = EventList;
                    var filterK = _eventParticipationKidService.GetDefaultSpecification();
                    filterK = filterK.And(p => p.Active == true).And(p => p.EventParticipationUId == oEventParticipation.UId);
                    List<EventParticipationKid> EventParticipationKidList = _eventParticipationKidService.GetCollection(filterK, p => p.CreationDate).ToList();
                    model.EventParticipationKidList = EventParticipationKidList;
                    EventParticipateModel mod = new EventParticipateModel();
                   

                       
                }
                catch (Exception)
                {
                    TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                    return RedirectToAction("Login", "Account");
                }
                return View(model);
            }

        [HttpPost]
        public ActionResult UpdateEventStatus(FormCollection Form, int id, string btnSubmit)
        {
            try
            {
                EventParticipation oEventParticipation = _eventParticipationService.GetByKey(id);
                TryUpdateModel(oEventParticipation);
                if (btnSubmit == "CANCEL")
                {
                    return RedirectToAction("EventHome", "Menu");
                }
                else if (btnSubmit == "SUBMIT")
                {
                    var filterK = _eventParticipationKidService.GetDefaultSpecification();
                    filterK = filterK.And(p => p.Active == true).And(p => p.EventParticipationUId == oEventParticipation.UId);
                    List<EventParticipationKid> EventParticipationKidList = _eventParticipationKidService.GetCollection(filterK, p => p.CreationDate).ToList();
                    foreach (EventParticipationKid kid in EventParticipationKidList)
                    { 
                        EventParticipationKid newKid=_eventParticipationKidService.GetByKey(kid.UId);
                        newKid.Active = false;
                        DataContext.SaveChanges();
                    }
                    if (Form["kidrowcount"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["kidrowcount"].ToString()))
                        {
                            int KidCount = int.Parse(Form["kidrowcount"].ToString());
                            if (KidCount > 0)
                            {
                                for (int i = 1; i <= KidCount; i++)
                                {
                                    EventParticipationKid kid = new EventParticipationKid();
                                    kid.EventParticipationUId = oEventParticipation.UId;
                                    if (Form["kidno_" + i] != null)
                                    {
                                        if (!string.IsNullOrEmpty(Form["kidno_" + i].ToString()))
                                        {
                                            kid.KidCount = int.Parse(Form["kidno_" + i].ToString());
                                        }
                                        else
                                        {
                                            kid.KidCount = 0;
                                        }
                                    }
                                    if (Form["kidage_" + i] != null)
                                    {
                                        if (!string.IsNullOrEmpty(Form["kidage_" + i].ToString()))
                                        {
                                            kid.KidAge = int.Parse(Form["kidage_" + i].ToString());
                                        }
                                        else
                                        {
                                            kid.KidAge = 0;
                                        }
                                    }
                                    if (Form["kidveg_" + i] != null)
                                    {
                                        if (!string.IsNullOrEmpty(Form["kidveg_" + i].ToString()))
                                        {
                                            kid.Veg = Form["kidveg_" + i].ToString();
                                        }
                                        else
                                        {
                                            kid.Veg = "";
                                        }
                                    }
                                    if (kid.KidCount > 0 && kid.KidAge > 0)
                                    {
                                        kid.Active = true;
                                        _eventParticipationKidService.Add(kid);
                                        DataContext.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                    EventParticipation oEventParticipationNew = _eventParticipationService.GetByKey(id);
                    if (Form["participatefamily"] != null)
                    {
                        int participatefamily = int.Parse(Form["participatefamily"].ToString());
                        string SpouseVeg = "";
                        if (participatefamily == 1)
                        {
                            participatefamily = int.Parse(Form["participatefamily"].ToString());
                            if (Form["spouseVeg"] != null)
                            {
                                SpouseVeg = Form["spouseVeg"].ToString();
                                if (SpouseVeg == "1")
                                    oEventParticipation.SpouseVeg = false;
                                else
                                    oEventParticipation.SpouseVeg = true;

                            }
                            oEventParticipationNew.IsFamily = true;
                        }
                        else
                        {
                            oEventParticipationNew.IsFamily = false;
                            oEventParticipationNew.SpouseVeg = false;
                        }
                    }
                    else
                    {
                        oEventParticipationNew.IsFamily = false;
                        oEventParticipationNew.SpouseVeg = false;
                    }
                    if (Form["IsVeg"] != null)
                    {
                        int isVeg = int.Parse(Form["IsVeg"].ToString());
                        if (isVeg == 1)
                        {
                            oEventParticipationNew.IsVeg = false;
                        }
                        else
                        {
                            oEventParticipationNew.IsVeg = true;
                        }
                    }
                    else
                    {
                        oEventParticipationNew.IsVeg = false;
                    }
                    if (Form["IsAlcohol"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["IsAlcohol"].ToString()))
                        {
                            int IsAlcohol = int.Parse(Form["IsAlcohol"].ToString());
                            oEventParticipation.IsAlcohol = IsAlcohol;
                        }
                        else
                        {
                            oEventParticipation.IsAlcohol = 0;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsAlcohol = 0;
                    }
                    if (Form["IsSpouseAlcohol"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["IsSpouseAlcohol"].ToString()))
                        {
                            int IsSpouseAlcohol = int.Parse(Form["IsSpouseAlcohol"].ToString());
                            oEventParticipation.IsSpouseAlcohol = IsSpouseAlcohol;
                        }
                        else
                        {
                            oEventParticipation.IsSpouseAlcohol = 0;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsSpouseAlcohol = 0;
                    }
                    if (Form["IsOmt"] != null)
                    {
                        int IsOmt = int.Parse(Form["IsOmt"].ToString());
                        if (IsOmt == 1)
                        {
                            oEventParticipation.IsOmt = true;
                        }
                        else
                        {
                            oEventParticipation.IsOmt = false;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsOmt = false;
                    }
                    if (Form["IsOMTVeg"] != null)
                    {
                        int IsOMTVeg = int.Parse(Form["IsOMTVeg"].ToString());
                        if (IsOMTVeg == 1)
                        {
                            oEventParticipation.IsOMTVeg = false;
                        }
                        else
                        {
                            oEventParticipation.IsOMTVeg = true;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsOMTVeg = false;
                    }
                    if (Form["IsParking"] != null)
                    {
                        int IsParking = int.Parse(Form["IsParking"].ToString());
                        if (IsParking == 1)
                        {
                            oEventParticipation.IsParking = true;
                        }
                        else
                        {
                            oEventParticipation.IsParking = false;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsParking = false;
                    }
                    if (Form["IsTransport"] != null)
                    {
                        int IsTransport = int.Parse(Form["IsTransport"].ToString());
                        if (IsTransport == 1)
                        {
                            oEventParticipation.IsTransport = true;
                        }
                        else
                        {
                            oEventParticipation.IsTransport = false;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsTransport = false;
                    }
                    if (Form["IsChangingRoom"] != null)
                    {
                        int IsChangingRoom = int.Parse(Form["IsChangingRoom"].ToString());
                        if (IsChangingRoom == 1)
                        {
                            oEventParticipation.IsChangingRoom = true;
                        }
                        else
                        {
                            oEventParticipation.IsChangingRoom = false;
                        }
                    }
                    else
                    {
                        oEventParticipation.IsChangingRoom = false;
                    }
                    if (Form["VehicleNo"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["VehicleNo"].ToString()))
                        {
                            string val = Form["VehicleNo"].ToString();
                            oEventParticipation.VehicleNo = val;
                        }
                        else
                        {
                            oEventParticipation.VehicleNo = "";
                        }
                    }
                    else
                    {
                        oEventParticipation.VehicleNo = "";
                    }
                    if (Form["memcount"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["memcount"].ToString()))
                        {
                            string val = Form["memcount"].ToString();
                            oEventParticipationNew.MemCount = int.Parse(val);
                        }
                        else
                        {
                            oEventParticipationNew.MemCount = 1;
                        }
                    }
                    else
                    {
                        oEventParticipationNew.MemCount = 0;
                    }
                    if (Form["Remark"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["Remark"].ToString()))
                        {
                            string val = Form["Remark"].ToString();
                            oEventParticipation.Remark = val;
                        }
                        else
                        {
                            oEventParticipation.Remark = "";
                        }
                    }
                    else
                    {
                        oEventParticipation.Remark = "";
                    }
                    if (Form["child1"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["child1"].ToString()))
                        {
                            string Child1 = Form["child1"].ToString();
                            oEventParticipation.AddField1 = Child1;
                        }
                        else
                        {
                            oEventParticipation.AddField1 = "0";
                        }
                    }
                    else
                    {
                        oEventParticipation.AddField1 = "0";
                    }
                    if (Form["child2"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["child2"].ToString()))
                        {
                            string Child2 = Form["child2"].ToString();
                            oEventParticipation.AddField2 = Child2;
                        }
                        else
                        {
                            oEventParticipation.AddField2 = "0";
                        }
                    }
                    else
                    {
                        oEventParticipation.AddField2 = "0";
                    }
                    if (Form["child3"] != null)
                    {
                        if (!string.IsNullOrEmpty(Form["child3"].ToString()))
                        {
                            string Child3 = Form["child3"].ToString();
                            oEventParticipation.AddField3 = Child3;
                        }
                        else
                        {
                            oEventParticipation.AddField3 = "0";
                        }
                    }
                    else
                    {
                        oEventParticipation.AddField3 = "0";
                    }
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully updated");
                    return RedirectToAction("EventHome", "Menu");
                }
            }
            catch (Exception)
            {

                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult SearchProfile(int id)
        {
            ProfileModel model = new ProfileModel();
            try
            {
                model.EventId = id;
                model.IsSet = false;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateAttendance(FormCollection Form, int id)
        {
            ProfileModel model = new ProfileModel();
            try
            {
                TryUpdateModel(model);
                EventAttendance att = new EventAttendance();
                if (Form["ServiceNo"] != null)
                    att.ServiceNo = Form["ServiceNo"].ToString();
                if (Form["AdultCount"] != null)
                { 
                    if(!string.IsNullOrEmpty(Form["AdultCount"].ToString()))
                    {
                        att.AdultCount = int.Parse(Form["AdultCount"].ToString());
                    }
                }
                if (Form["ChildCount"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["ChildCount"].ToString()))
                    {
                        att.KidCount = int.Parse(Form["ChildCount"].ToString());
                    }
                }
                if (Form["GuestCount"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["GuestCount"].ToString()))
                    {
                        att.GuestCount = int.Parse(Form["GuestCount"].ToString());
                    }
                }
                if (Form["UserId"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["UserId"].ToString()))
                    {
                        att.UserId = int.Parse(Form["UserId"].ToString());
                    }
                }
                
                if (Form["EventParticipationUId"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["EventParticipationUId"].ToString()))
                    {
                        att.EventParticipationUId = int.Parse(Form["EventParticipationUId"].ToString());
                    }
                } 
                if (Form["Name"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["Name"].ToString()))
                    {
                        att.Name = Form["Name"].ToString();
                    }
                }
                att.EventId = id;
                att.ServiceNo = Form["ServiceNo"].ToString();
                att.Active = true;
                _eventAttendanceService.Add(att);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully updated");
                return RedirectToAction("SearchProfile", "Menu",new { id=id});
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        [HttpPost]
        public ActionResult SearchProfile(FormCollection Form,int id)
        {
            ProfileModel model = new ProfileModel();
            try
            {
                int AduleCount = 0;
                int ChildCount = 0;
                int GuestCount = 0;
                model.EventParticipationUId = 0;
                model.UserId = 0;
                TryUpdateModel(model);
                var filter = _eventParticipationService.GetDefaultSpecification();
                filter = filter.And(p=>p.Active==true).And(p=>p.EventUId==id).And(p=>p.UserBase.UserName==model.ServiceNo);
                List<EventParticipation> par = _eventParticipationService.GetCollection(filter,p=>p.CreationDate).ToList();
                if (par.Count > 0)
                {
                    string Name="";
                    string Rank="";

                    EventParticipation ep = par[0];
                    Name = ep.UserBase.Name;
                    if (ep.UserBase.Rank != null)
                    {
                        Rank = ep.UserBase.Rank.Name;
                    }
                    model.Name = Rank + " " + Name;
                   
                    AduleCount = AduleCount + 1;
                    if(ep.IsFamily)
                        AduleCount = AduleCount + 1;
                    var filterK = _eventParticipationKidService.GetDefaultSpecification();
                    filterK = filterK.And(p => p.Active == true).And(p => p.EventParticipationUId == ep.UId);
                    List<EventParticipationKid> KidList = _eventParticipationKidService.GetCollection(filterK, p => p.CreationDate).ToList();
                    foreach (EventParticipationKid k in KidList)
                    {
                        if (k.KidAge <= 11)
                            ChildCount++;
                        else if (k.KidAge >= 12)
                            AduleCount++;
                        else
                            ChildCount++;

                    }
                    if (!string.IsNullOrEmpty(ep.MemCount.ToString()))
                        GuestCount = ep.MemCount;

                    model.UserId = ep.UserId;
                    model.EventParticipationUId = ep.UId;
                }
                model.AdultCount = AduleCount;
                model.ChildCount = ChildCount;
                model.GuestCount = GuestCount;
                
                model.IsSet = true;
                model.EventId = id;
                model.RankUId = 0;
                if (model.UserId == 0)
                {
                    var filterU = _userAccountService.GetDefaultSpecification();
                    filterU = filterU.And(p=>p.Active==true).And(p=>p.UserName==model.ServiceNo);
                    UserAccount acc = _userAccountService.GetBy(filterU);
                    if (acc != null)
                    {
                        model.RankUId = acc.RankUId;
                        model.UserId = acc.Id;
                        string R="";
                        if (acc.Rank != null)
                        {
                            R = acc.Rank.Name;
                        }
                        model.Name = R+" "+acc.Name;
                    }
                    
                }
                if (model.AdultCount == 0)
                    model.AdultCount = 1;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public ActionResult SendWhatsApp()
        {
            try
            {
                System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=94717744745&text=Hii");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public ActionResult MenuOrderList()
        {
            
            MenuOrderModel model = new MenuOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _menuOrderService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.Status == 10).And(p=>p.SLAFLocationUId==account.LocationUId);
                List<MenuOrder> MenuItemList = _menuOrderService.GetCollection(filter, p => p.CreationDate).OrderBy(p => p.OrderDate).ToList();
                model.PendingMenuOrderList = MenuItemList;

                var filterC = _menuOrderService.GetDefaultSpecification();
                filterC = filterC.And(p => p.Active == true).And(p => p.Status == 20).And(p => p.SLAFLocationUId == account.LocationUId);
                List<MenuOrder> CompMenuItemList = _menuOrderService.GetCollection(filterC, p => p.CreationDate).OrderBy(p => p.OrderDate).ToList();
                model.CompleteMenuOrderList = CompMenuItemList;

                var filterCan = _menuOrderService.GetDefaultSpecification();
                filterCan = filterCan.And(p => p.Active == true).And(p => p.Status == 30).And(p => p.SLAFLocationUId == account.LocationUId);
                List<MenuOrder> CancelledMenuItemList = _menuOrderService.GetCollection(filterCan, p => p.CreationDate).OrderBy(p => p.OrderDate).ToList();
                model.CancelledMenuOrderList = CancelledMenuItemList;

                var filterDeli = _menuOrderService.GetDefaultSpecification();
                filterDeli = filterDeli.And(p => p.Active == true).And(p => p.Status == 40).And(p => p.SLAFLocationUId == account.LocationUId);
                List<MenuOrder> DeliveredMenuItemList = _menuOrderService.GetCollection(filterDeli, p => p.CreationDate).OrderByDescending(p => p.OrderDate).ToList();
                model.DeliveredMenuOrderList = DeliveredMenuItemList;
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }
        public ActionResult HQKOT()
        {
            MenuOrderModel model = new MenuOrderModel();
            
            var filterC = _menuOrderService.GetDefaultSpecification();
            filterC = filterC.And(p => p.Active == true).And(p => p.Status == 20).And(p=>p.SLAFLocationUId==1);
            List<MenuOrder> CompMenuItemList = _menuOrderService.GetCollection(filterC, p => p.CreationDate).OrderBy(p => p.OrderDate).ToList();
            model.CompleteMenuOrderList = CompMenuItemList;

            
            return View(model);
        }
        public ActionResult OrderMenu(int id)
        {
            MenuClientOrderModel model = new MenuClientOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                model.LocationId = account.LocationUId;
                model.MenuItem = _menuItemService.GetByKey(id);
                var filter = _menuOptionService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuItemUId == id);
                model.MenuOptionList = _menuOptionService.GetCollection(filter, p => p.CreationDate).ToList();

                List<MenuMultiOption> MenuMultiOptionList = new List<MenuMultiOption>();
                var filterOpt = _menuMultiOptionService.GetDefaultSpecification();
                filterOpt = filterOpt.And(p => p.Active == true).And(p => p.MenuItemUId == id);
                model.MenuMultiOptionList = _menuMultiOptionService.GetCollection(filterOpt, p => p.CreationDate).ToList();
               
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult OrderMenu(FormCollection Form, int id)
        {
            try
            {
                MenuItem item = _menuItemService.GetByKey(id);
                int MenuItemId = id;
                UserAccount account = GetCurrentUser();
                if (account == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                MenuOrder oMenuOrder = new MenuOrder();
                oMenuOrder.MenuItemUId = MenuItemId;
                oMenuOrder.OptionType1 = 0;
                oMenuOrder.OptionType2 = 0;
                if (Form["OptionParameter"] != null)
                {
                    if(!string.IsNullOrEmpty(Form["OptionParameter"].ToString()))
                    {
                        oMenuOrder.OptionType1 = int.Parse(Form["OptionParameter"].ToString());
                    }
                    
                }
                if (Form["OptionMultiParameter"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["OptionMultiParameter"].ToString()))
                    {
                        oMenuOrder.OptionType2 = int.Parse(Form["OptionMultiParameter"].ToString());
                    }

                }
                if (Form["menucount"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["menucount"].ToString()))
                    {
                        oMenuOrder.Qty = decimal.Parse(Form["menucount"].ToString());
                    }
                    else
                    {
                        oMenuOrder.Qty = 1;
                    }
                }
                oMenuOrder.Remark = "";
                if (Form["remark"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["remark"].ToString()))
                    {
                        oMenuOrder.Remark = Form["remark"].ToString();
                    }
                    else
                    {
                        oMenuOrder.Remark = "";
                    }
                }
                oMenuOrder.Location = "";
                if (Form["delilocation"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["delilocation"].ToString()))
                    {
                        oMenuOrder.Location = Form["delilocation"].ToString();
                    }
                    else
                    {
                        oMenuOrder.Location = "";
                    }
                }
                if (Form["roomno"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["roomno"].ToString()))
                    {
                        oMenuOrder.LocationRef = Form["roomno"].ToString();
                    }
                    else
                    {
                        oMenuOrder.LocationRef = "";
                    }
                }
                else
                {
                    oMenuOrder.LocationRef = "";
                }
                oMenuOrder.OrderDate = DateTime.Now;
                if (Form["delidate"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["delidate"].ToString()))
                    {
                        oMenuOrder.OrderDate = DateTime.Parse(Form["delidate"].ToString());
                        if (oMenuOrder.OrderDate < DateTime.Now.Date)
                        {
                            TempData[ViewDataKeys.Message] = new FailMessage("Order Date should be today or future date.");
                            return RedirectToAction("MenuItemIndex", new { id = id });
                        }
                    }
                    else
                    {
                        oMenuOrder.OrderDate = DateTime.Now;
                    }
                }
                oMenuOrder.Time = "";
                if (Form["delitime"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["delitime"].ToString()))
                    {
                        oMenuOrder.Time = Form["delitime"].ToString();
                    }
                    else
                    {
                        oMenuOrder.Time = "";
                    }
                }
                oMenuOrder.SLAFLocationUId = account.LocationUId;
                oMenuOrder.UserId = account.Id;
                oMenuOrder.Status = 10;
                oMenuOrder.Active = true;
                oMenuOrder.MenuItemUId = MenuItemId;
                _menuOrderService.Add(oMenuOrder);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully added to the Order");
                return RedirectToAction("MenuItemIndex", new { id = item.MenuCategoryUId });
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        //[HttpPost]
        //public ActionResult OrderMenu(FormCollection Form, int id)
        //{
        //    try
        //    {
        //        MenuCategory oMenuCategory = _menuCategoryService.GetByKey(id);
        //        int MenuItemId = 0;
        //        UserAccount account = GetCurrentUser();
        //        if (account == null)
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }
        //        MenuOrder oMenuOrder = new MenuOrder();
        //        if (Form["menuitemid"] != null)
        //            MenuItemId = int.Parse(Form["menuitemid"].ToString());
        //        if (oMenuCategory.MenuTypeId == 1)
        //        {
        //            if (Form["vegrad"] != null)
        //                oMenuOrder.IsVeg = bool.Parse(Form["vegrad"].ToString());
        //            if (Form["dietrad"] != null)
        //                oMenuOrder.IsDiet = bool.Parse(Form["dietrad"].ToString());

        //        }
        //        else if (oMenuCategory.MenuTypeId == 2)
        //        {
        //            if (Form["eggrad"] != null)
        //                oMenuOrder.IsEgg = bool.Parse(Form["eggrad"].ToString());
        //            if (Form["vegrad"] != null)
        //                oMenuOrder.WithVeg = bool.Parse(Form["vegrad"].ToString());

        //        }
        //        else if (oMenuCategory.MenuTypeId == 3)
        //        {
        //            if (Form["optrad"] != null)
        //                oMenuOrder.BiteOpt = int.Parse(Form["optrad"].ToString());

        //        }
        //        else if (oMenuCategory.MenuTypeId == 4)
        //        {
        //            if (MenuItemId == 12 || MenuItemId == 13 || MenuItemId == 15)
        //            {
        //                oMenuOrder.BiteOpt = 0;
        //            }
        //            else
        //            {
        //                if (Form["optrad"] != null)
        //                    oMenuOrder.BiteOpt = int.Parse(Form["optrad"].ToString());
        //            }

        //        }
        //        oMenuOrder.Qty = 1;
        //        if (Form["menucount"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(Form["menucount"].ToString()))
        //            {
        //                oMenuOrder.Qty = int.Parse(Form["menucount"].ToString());
        //            }
        //            else
        //            {
        //                oMenuOrder.Qty = 1;
        //            }
        //        }
        //        oMenuOrder.Remark = "";
        //        if (Form["remark"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(Form["remark"].ToString()))
        //            {
        //                oMenuOrder.Remark = Form["remark"].ToString();
        //            }
        //            else
        //            {
        //                oMenuOrder.Remark = "";
        //            }
        //        }
        //        oMenuOrder.Location = "";
        //        if (Form["delilocation"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(Form["delilocation"].ToString()))
        //            {
        //                oMenuOrder.Location = Form["delilocation"].ToString();
        //            }
        //            else
        //            {
        //                oMenuOrder.Location = "";
        //            }
        //        }
        //        if (Form["roomno"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(Form["roomno"].ToString()))
        //            {
        //                oMenuOrder.LocationRef = Form["roomno"].ToString();
        //            }
        //            else
        //            {
        //                oMenuOrder.LocationRef = "";
        //            }
        //        }
        //        else {
        //            oMenuOrder.LocationRef = "";
        //        }
        //        oMenuOrder.OrderDate = DateTime.Now;
        //        if (Form["delidate"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(Form["delidate"].ToString()))
        //            {
        //                oMenuOrder.OrderDate = DateTime.Parse(Form["delidate"].ToString());
        //                if (oMenuOrder.OrderDate < DateTime.Now.Date)
        //                {
        //                    TempData[ViewDataKeys.Message] = new FailMessage("Order Date should be today or future date.");
        //                    return RedirectToAction("MenuItemIndex", new { id = id });
        //                }
        //            }
        //            else
        //            {
        //                oMenuOrder.OrderDate = DateTime.Now;
        //            }
        //        }
        //        oMenuOrder.Time = "";
        //        if (Form["delitime"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(Form["delitime"].ToString()))
        //            {
        //                oMenuOrder.Time = Form["delitime"].ToString();
        //            }
        //            else
        //            {
        //                oMenuOrder.Time = "";
        //            }
        //        }
        //        oMenuOrder.SLAFLocationUId = account.LocationUId;
        //        oMenuOrder.UserId = account.Id;
        //        oMenuOrder.Status = 10;
        //        oMenuOrder.Active = true;
        //        oMenuOrder.MenuItemUId = MenuItemId;
        //        _menuOrderService.Add(oMenuOrder);
        //        DataContext.SaveChanges();
        //        TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully added to the Order");
        //        return RedirectToAction("MenuItemIndex", new { id = id });
        //    }
        //    catch (Exception)
        //    {
        //        TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
        //        return RedirectToAction("Login", "Account");  
        //    }
        //    return View();
        //}
        [HttpPost]
        public ActionResult MenuOrderConfirm(FormCollection Form, int id)
        {
            try
            {
                MenuOrder oMenuOrder = _menuOrderService.GetByKey(id);
                UserAccount account = GetCurrentUser();
                if (account == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                oMenuOrder.Status = 20;
                DataContext.SaveChanges();
                string s = "";
                string MobNo = oMenuOrder.UserBase.Telephone1;
                if (!string.IsNullOrEmpty(oMenuOrder.UserBase.Telephone1))
                {
                    s = MobNo;
                    s = s.Substring(0, 1);
                }
                if (s == "0")
                {
                    MobNo = MobNo.Remove(0, 1);
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order successfully accepted");
                if (!string.IsNullOrEmpty(oMenuOrder.UserBase.Telephone1))
                {
                    string menu = oMenuOrder.MenuItem.Name;
                    //menu = menu.Replace(" ","%20");
                    if (!string.IsNullOrEmpty(MobNo))
                    {
                        //TempData["WhatMsg"] = "http://api.whatsapp.com/send?phone=" + MobNo + "%&%&";
                          TempData["WhatMsg1"]="http://api.whatsapp.com/send?phone=" + MobNo + "";
                          TempData["WhatMsg2"] = "text=Your order " + menu + " has been accpeted and being processed.";
                    }
                    //Process.Start(new ProcessStartInfo("http://api.whatsapp.com/send?phone=" + MobNo + "&text='Your%20order%20" + menu + "%20has%20been%20accpeted%20and%20being%20processed.'") { UseShellExecute = true });
                    //System.Diagnostics.Process.Start("","http://api.whatsapp.com/send?phone=" + MobNo + "&text='Your%20order%20" + menu + "%20has%20been%20accpeted%20and%20being%20processed.'");
                }
                return RedirectToAction("MenuOrderList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
        [HttpPost]
        public ActionResult MenuOrderDelivery(FormCollection Form, int id)
        {
            try
            {
                MenuOrder oMenuOrder = _menuOrderService.GetByKey(id);
                UserAccount account = GetCurrentUser();
                if (account == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                oMenuOrder.Status = 40;
                DataContext.SaveChanges();
                string s = "";
                string MobNo = oMenuOrder.UserBase.Telephone1;
                if (!string.IsNullOrEmpty(oMenuOrder.UserBase.Telephone1))
                {
                    s = MobNo;
                    s = s.Substring(0, 1);
                }
                if (s == "0")
                {
                    MobNo = MobNo.Remove(0, 1);
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order successfully accepted");
                if (!string.IsNullOrEmpty(oMenuOrder.UserBase.Telephone1))
                {
                    string menu = oMenuOrder.MenuItem.Name;
                    //menu = menu.Replace(" ", "%20");
                    string loc = oMenuOrder.Location;
                    //loc = loc.Replace(" ", "%20");
                    if (!string.IsNullOrEmpty(MobNo))
                    {
                        //TempData["WhatMsg1"] = "http://api.whatsapp.com/send?phone=" + MobNo + "\"" + "&text=Your%20order%20" + menu + "%20has%20been%20prepared%20and%20delivered%20to%20" + loc + "";
                        TempData["WhatMsg1"] = "http://api.whatsapp.com/send?phone=" + MobNo + "";
                        TempData["WhatMsg2"] = "text=Your%20order%20" + menu + "%20has%20been%20prepared%20and%20delivered%20to%20" + loc + "";
                        //System.Diagnostics.Process.Start("");
                    }
                }
                    return RedirectToAction("MenuOrderList");
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");  
            }
            return View();
        }
        [HttpPost]
        public ActionResult CancelOrder(FormCollection Form)
        {
            try
            {
                int menuOrderId = 0;
                if (Form["menuorderid"] != null)
                    menuOrderId = int.Parse(Form["menuorderid"].ToString());
                MenuOrder oMenuOrder = _menuOrderService.GetByKey(menuOrderId);
                UserAccount account = GetCurrentUser();
                if (account == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                oMenuOrder.Status = 30;
                oMenuOrder.Remark = Form["reasontxt"].ToString();
                DataContext.SaveChanges();
                string s = "";
                string MobNo = oMenuOrder.UserBase.Telephone1;
                if (!string.IsNullOrEmpty(oMenuOrder.UserBase.Telephone1))
                {
                    s = MobNo;
                    s = s.Substring(0,1);
                }
                if (s == "0")
                {
                    MobNo = MobNo.Remove(0, 1);
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order successfully Cancelled");
                if (!string.IsNullOrEmpty(oMenuOrder.UserBase.Telephone1))
                {
                    string menu = oMenuOrder.MenuItem.Name;
                    menu = menu.Replace(" ", "%20");
                    System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=94" + MobNo + "&text=Your%20order%20" + menu + "%20has%20been%20cancelled");
                }
                    return RedirectToAction("MenuOrderList");
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");  
            }
            return View();
        }
        private UserAccount GetCurrentUser()
        {
            UserAccount userAccount = new UserAccount();
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(s => s.UserName == userName).And(p => p.Active == true);
                userAccount = _userAccountService.GetBy(filter);

            }
            catch (Exception)
            {

            }
            return userAccount;

        }
        public ActionResult EventHome()
        {
            List<EventParticipateModel> list = new List<EventParticipateModel>();
            try
            {
                UserAccount account = GetCurrentUser();
                TempData["usermode"] = account.UserMode.ToString();
                DateTime date = DateTime.Now;
                var filter = _eventService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p=>p.SLAFLocationUId==account.LocationUId);
                filter = filter.And(p => p.EffectiveDate >= date.Date);
                List<Event> EventList = _eventService.GetCollection(filter, p => p.CreationDate).ToList();
                foreach (Event e in EventList)
                {
                    EventParticipateModel mod = new EventParticipateModel();
                    mod.RankUId = account.RankUId;
                    mod.UserMode = account.UserMode.ToString();
                    var filterE = _eventParticipationService.GetDefaultSpecification();
                    filterE = filterE.And(p => p.Active == true).And(p => p.UserId == account.Id).And(p => p.EventUId == e.UId);
                    List<EventParticipation> oEventParticipationList = _eventParticipationService.GetCollection(filterE, p => p.CreationDate).ToList();
                    if (oEventParticipationList.Count == 0)
                    {
                        mod.Event = e;
                        mod.IsParticipated = false;
                        mod.ParticipationSubmit = false;
                        mod.EventParticipation = new EventParticipation();
                    }
                    else
                    {
                        mod.Event = e;
                        if (oEventParticipationList[0].IsParticipating)
                            mod.IsParticipated = true;
                        else
                            mod.IsParticipated = false;
                        mod.ParticipationSubmit = true;
                        mod.EventParticipation = oEventParticipationList[0];
                    }
                    list.Add(mod);
                }
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login","Account");                
            }
            return View(list);
        }
        public ActionResult AddEvent()
        {
            Event oEvent = new Event();
            return View(oEvent);
        }
        public ActionResult EventIndex()
        {
            List<Event> list = new List<Event>();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _eventService.GetDefaultSpecification();
                filter = filter.And(p=>p.Active==true).And(p=>p.SLAFLocationUId== account.LocationUId);
                list = _eventService.GetCollection(filter, p => p.CreationDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(list);
        } 
        public ActionResult AttendanceList(int id)
        {
            List<EventAttendance> list = new List<EventAttendance>();
            try
            {
                var filter = _eventAttendanceService.GetDefaultSpecification();
                filter = filter.And(p=>p.Active==true).And(p=>p.EventId==id);
                list = _eventAttendanceService.GetCollection(filter, p => p.CreationDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult EventDetail(int id)
        {
            List<EventParticipateModel> list = new List<EventParticipateModel>();
            try
            {
                var filter = _eventParticipationService.GetDefaultSpecification();
                filter = filter.And(p=>p.Active==true).And(p=>p.EventUId==id).And(p=>p.IsParticipating==true);
                List<EventParticipation>  listeve = _eventParticipationService.GetCollection(filter, p => p.CreationDate).ToList();
                foreach (EventParticipation eve in listeve)
                {
                    EventParticipateModel mod = new EventParticipateModel();
                    mod.EventParticipation = eve;
                    var filterK = _eventParticipationKidService.GetDefaultSpecification();
                    filterK = filterK.And(p => p.Active == true).And(p => p.EventParticipationUId == eve.UId);
                    List<EventParticipationKid> listkideve = _eventParticipationKidService.GetCollection(filterK, p => p.CreationDate).ToList();
                    mod.EventParticipationKidList = listkideve;
                    list.Add(mod);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult AddEvent(FormCollection Form, HttpPostedFileBase uploadfile)
        {
            Event oEvent = new Event();
            try
            {
                UserAccount account = GetCurrentUser();
                TryUpdateModel(oEvent);
                oEvent.Active = true;
                oEvent.SLAFLocationUId = account.LocationUId;
                _eventService.Add(oEvent);
                DataContext.SaveChanges();
                using (MemoryStream ms = new MemoryStream())
                {
                    uploadfile.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    Event ImgEvent = new Event();
                    ImgEvent = _eventService.GetByKey(oEvent.UId);
                    ImgEvent.Img = array;
                    DataContext.SaveChanges();
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Event successfully added");
                return RedirectToAction("AddEvent");
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");      
            }
            return View(oEvent);
        }
        [HttpPost]
        public ActionResult CancelEvent(FormCollection Form)
        {
            try
            {
                EventParticipation oEventParticipation = new EventParticipation();
                int EventId = int.Parse(Form["eventuid"].ToString());
                int notparticipating = int.Parse(Form["notparticipating"].ToString());
                if (notparticipating == 1)
                {
                    UserAccount account = GetCurrentUser();
                    oEventParticipation.EventUId = EventId;
                    oEventParticipation.UserId = account.Id;
                    oEventParticipation.Active = true;
                    oEventParticipation.IsParticipating = false;
                    oEventParticipation.IsFamily = false;
                    oEventParticipation.MemCount = 0;
                    _eventParticipationService.Add(oEventParticipation);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Event Participation status successfully updated");
                }
                else {
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Event Participation status not updated");
                }
                return RedirectToAction("EventHome");
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");      
            }
            return View();
        }
        [HttpPost]
        public ActionResult ParticipateEvent(FormCollection Form)
        {
            try
            {
                EventParticipation oEventParticipation = new EventParticipation();
                int EventId = int.Parse(Form["eventuid"].ToString());
                UserAccount account = GetCurrentUser();
                oEventParticipation.EventUId = EventId;
                oEventParticipation.UserId = account.Id;
                oEventParticipation.Active = true;
                oEventParticipation.IsParticipating = true;
                if (Form["participatefamily"] != null)
                {
                    int participatefamily = int.Parse(Form["participatefamily"].ToString());
                    string SpouseVeg = ""; 
                    if (participatefamily == 1)
                    {
                        participatefamily = int.Parse(Form["participatefamily"].ToString());
                        if (Form["spouseVeg"] != null)
                        {
                            SpouseVeg = Form["spouseVeg"].ToString();
                            if (SpouseVeg=="1")
                                oEventParticipation.SpouseVeg = false;
                            else
                                oEventParticipation.SpouseVeg = true;
                            
                        }
                        oEventParticipation.IsFamily = true;
                    }
                    else
                    {
                        oEventParticipation.IsFamily = false;
                            oEventParticipation.SpouseVeg=false;
                    }
                }
                else
                {
                    oEventParticipation.IsFamily = false;
                    oEventParticipation.SpouseVeg = false;
                }
                if (Form["IsVeg"] != null)
                {
                    int isVeg = int.Parse(Form["IsVeg"].ToString());
                    if (isVeg == 1)
                    {
                        oEventParticipation.IsVeg = false;
                    }
                    else
                    {
                        oEventParticipation.IsVeg = true;
                    }
                }
                else
                {
                    oEventParticipation.IsVeg = false;
                }
                if (Form["IsAlcohol"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["IsAlcohol"].ToString()))
                    {
                        int IsAlcohol = int.Parse(Form["IsAlcohol"].ToString());
                        oEventParticipation.IsAlcohol = IsAlcohol;
                    }
                    else
                    {
                        oEventParticipation.IsAlcohol = 0;
                    }
                }
                else
                {
                    oEventParticipation.IsAlcohol = 0;
                }
                if (Form["IsSpouseAlcohol"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["IsSpouseAlcohol"].ToString()))
                    {
                        int IsSpouseAlcohol = int.Parse(Form["IsSpouseAlcohol"].ToString());
                        oEventParticipation.IsSpouseAlcohol = IsSpouseAlcohol;
                    }
                    else
                    {
                        oEventParticipation.IsSpouseAlcohol = 0;
                    }
                }
                else
                {
                    oEventParticipation.IsSpouseAlcohol = 0;
                }
                
                if (Form["IsOmt"] != null)
                {
                    int IsOmt = int.Parse(Form["IsOmt"].ToString());
                    if (IsOmt == 1)
                    {
                        oEventParticipation.IsOmt = true;
                    }
                    else
                    {
                        oEventParticipation.IsOmt = false;
                    }
                }
                else
                {
                    oEventParticipation.IsOmt = false;
                }
                if (Form["IsOMTVeg"] != null)
                {
                    int IsOMTVeg = int.Parse(Form["IsOMTVeg"].ToString());
                    if (IsOMTVeg == 1)
                    {
                        oEventParticipation.IsOMTVeg = false;
                    }
                    else
                    {
                        oEventParticipation.IsOMTVeg = true;
                    }
                }
                else
                {
                    oEventParticipation.IsOmt = false;
                }
                if (Form["IsParking"] != null)
                {
                    int IsParking = int.Parse(Form["IsParking"].ToString());
                    if (IsParking == 1)
                    {
                        oEventParticipation.IsParking = true;
                    }
                    else
                    {
                        oEventParticipation.IsParking = false;
                    }
                }
                else
                {
                    oEventParticipation.IsParking = false;
                }
                if (Form["IsTransport"] != null)
                {
                    int IsTransport = int.Parse(Form["IsTransport"].ToString());
                    if (IsTransport == 1)
                    {
                        oEventParticipation.IsTransport = true;
                    }
                    else
                    {
                        oEventParticipation.IsTransport = false;
                    }
                }
                else
                {
                    oEventParticipation.IsTransport = false;
                }
                if (Form["IsChangingRoom"] != null)
                {
                    int IsChangingRoom = int.Parse(Form["IsChangingRoom"].ToString());
                    if (IsChangingRoom == 1)
                    {
                        oEventParticipation.IsChangingRoom = true;
                    }
                    else
                    {
                        oEventParticipation.IsChangingRoom = false;
                    }
                }
                else
                {
                    oEventParticipation.IsChangingRoom = false;
                }
                if (Form["VehicleNo"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["VehicleNo"].ToString()))
                    {
                        string val = Form["VehicleNo"].ToString();
                        oEventParticipation.VehicleNo = val;
                    }
                    else
                    {
                        oEventParticipation.VehicleNo ="";
                    }
                }
                else
                {
                    oEventParticipation.VehicleNo = "";
                }
                if (Form["VehicleType"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["VehicleType"].ToString()))
                    {
                        string val = Form["VehicleType"].ToString();
                        oEventParticipation.VehicleType = val;
                    }
                    else
                    {
                        oEventParticipation.VehicleType = "";
                    }
                }
                else
                {
                    oEventParticipation.VehicleType = "";
                }
                if (Form["memcount"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["memcount"].ToString()))
                    {
                        string val = Form["memcount"].ToString();
                        oEventParticipation.MemCount = int.Parse(val);
                    }
                    else
                    {
                        oEventParticipation.MemCount = 1;
                    }
                }
                else
                {
                    oEventParticipation.MemCount = 0;
                }
                if (Form["Remark"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["Remark"].ToString()))
                    {
                        string val = Form["Remark"].ToString();
                        oEventParticipation.Remark = val;
                    }
                    else
                    {
                        oEventParticipation.Remark = "";
                    }
                }
                else
                {
                    oEventParticipation.Remark = "";
                }
                if (Form["child1"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["child1"].ToString()))
                    {
                        string Child1 = Form["child1"].ToString();
                        oEventParticipation.AddField1 = Child1;
                    }
                    else
                    {
                        oEventParticipation.AddField1 = "0";
                    }
                }
                else
                {
                    oEventParticipation.AddField1 = "0";
                }
                if (Form["child2"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["child2"].ToString()))
                    {
                        string Child2 = Form["child2"].ToString();
                        oEventParticipation.AddField2 = Child2;
                    }
                    else
                    {
                        oEventParticipation.AddField2 = "0";
                    }
                }
                else
                {
                    oEventParticipation.AddField2 = "0";
                }
                if (Form["child3"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["child3"].ToString()))
                    {
                        string Child3 = Form["child3"].ToString();
                        oEventParticipation.AddField3 = Child3;
                    }
                    else
                    {
                        oEventParticipation.AddField3 = "0";
                    }
                }
                else
                {
                    oEventParticipation.AddField3 = "0";
                }
                _eventParticipationService.Add(oEventParticipation);
                DataContext.SaveChanges();

                if (Form["kidrowcount"] != null)
                {
                    if (!string.IsNullOrEmpty(Form["kidrowcount"].ToString()))
                    {
                        int KidCount = int.Parse(Form["kidrowcount"].ToString());
                        if (KidCount > 0)
                        {
                            for(int i=1;i<= KidCount;i++)
                            {
                                EventParticipationKid kid = new EventParticipationKid();
                                kid.EventParticipationUId = oEventParticipation.UId;
                                if (Form["kidno" + i] != null)
                                {
                                    if (!string.IsNullOrEmpty(Form["kidno" + i].ToString()))
                                    {
                                        kid.KidCount = int.Parse(Form["kidno" + i].ToString());
                                    }
                                    else
                                    {
                                        kid.KidCount = 0;
                                    }
                                }
                                if (Form["kidage" + i] != null)
                                {
                                    if (!string.IsNullOrEmpty(Form["kidage" + i].ToString()))
                                    {
                                        kid.KidAge = int.Parse(Form["kidage" + i].ToString());
                                    }
                                    else
                                    {
                                        kid.KidAge = 0;
                                    }
                                }
                                if (Form["kidveg" + i] != null)
                                {
                                    if (!string.IsNullOrEmpty(Form["kidveg" + i].ToString()))
                                    {
                                        kid.Veg = Form["kidveg" + i].ToString();
                                    }
                                    else
                                    {
                                        kid.Veg = "";
                                    }
                                }
                                if (kid.KidCount > 0&& kid.KidAge > 0)
                                {
                                    kid.Active = true;
                                    _eventParticipationKidService.Add(kid);
                                    DataContext.SaveChanges();
                                }
                            }
                        }
                    }
                }

                TempData[ViewDataKeys.Message] = new SuccessMessage("Event Participation status successfully updated");
                return RedirectToAction("EventHome");
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");      
            }
            return View();
        }
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public ActionResult DataTransfer(int? archive)
        //{
        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:/FuelExcel/FuelExcel/do/part2.xlsx");
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;
        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;
        //    int UID = 43709;
        //    bool insertNew = false;
        //    int PastHouseRefId = 0;
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        if (i > 0)
        //        {
        //            VehicleInfo oVehicleInfo = new VehicleInfo();
        //            if (xlRange.Cells[i, 1].Value != null)
        //            {
        //                string v = xlRange.Cells[i, 1].Value.ToString();
        //                int Val = int.Parse(v);
        //                oVehicleInfo.SLAFLocationUId = Val;
        //            }
        //            if (xlRange.Cells[i, 2].Value != null)
        //            {
        //                oVehicleInfo.ServiceNo = xlRange.Cells[i, 2].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.ServiceNo = "";
        //            }

        //            if (xlRange.Cells[i, 3].Value != null)
        //            {
        //                int RankID = int.Parse(xlRange.Cells[i, 3].Value.ToString());
        //                oVehicleInfo.RankUId = RankID;
        //            }
        //            else
        //            {
        //                oVehicleInfo.RankUId = 0;
        //            }
        //            if (xlRange.Cells[i, 4].Value != null)
        //            {
        //                oVehicleInfo.Name = xlRange.Cells[i, 4].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.Name = "";
        //            }
        //            if (xlRange.Cells[i, 5].Value != null)
        //            {
        //                oVehicleInfo.VehicleNo = xlRange.Cells[i, 5].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.VehicleNo = "";
        //            }
        //            if (xlRange.Cells[i, 6].Value != null)
        //            {
        //                oVehicleInfo.VehicleOwnership = xlRange.Cells[i,6].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.VehicleOwnership = "";
        //            }



        //            if (xlRange.Cells[i,7].Value != null)
        //            {
        //                int VehicleCategoryUId = int.Parse(xlRange.Cells[i, 7].Value.ToString());
        //                oVehicleInfo.VehicleCategoryUId = VehicleCategoryUId;
        //            }
        //            else
        //            {
        //                oVehicleInfo.VehicleCategoryUId = 0;
        //            }
        //            oVehicleInfo.DistrictUId = 1;
        //            oVehicleInfo.TownUId = "RMA";
        //            oVehicleInfo.Status = 1;
        //            oVehicleInfo.IsOld = false;

        //            oVehicleInfo.Active = true;
        //            int VehicleExist = new CustomDataBaseManger().CheckVehicleSserviceNoExist(oVehicleInfo.ServiceNo);
        //            if (VehicleExist > 0)
        //            {
        //                int hasHefuelDrawwed = new CustomDataBaseManger().CheckVehicleFuelDrawExist(oVehicleInfo.ServiceNo);
        //                if (hasHefuelDrawwed == 0)
        //                {
        //                    var filter = _vehicleInfoService.GetDefaultSpecification();
        //                    filter = filter.And(p => p.Active == true).And(p=>p.ServiceNo== oVehicleInfo.ServiceNo);
        //                    List<VehicleInfo> VehicleInfoList = _vehicleInfoService.GetCollection(filter, p => p.CreationDate).ToList();
        //                    foreach(VehicleInfo inV in  VehicleInfoList)
        //                    {
        //                        VehicleInfo v = _vehicleInfoService.GetByKey(inV.UId);
        //                        v.Active = false;
        //                        DataContext.SaveChanges();
        //                    }
        //                    _vehicleInfoService.Add(oVehicleInfo);
        //                    DataContext.SaveChanges();
        //                }
        //            }
        //            else {
        //                _vehicleInfoService.Add(oVehicleInfo);
        //                DataContext.SaveChanges();
        //            }


        //        }

        //        // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
        //        //}

        //        //add useful things here!   
        //        //}
        //    }
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    return View();
        //}
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public ActionResult DataDeletion(int? archive)
        //{
        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:/FuelExcel/FuelExcel/do/delete.xlsx");
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;
        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;
        //    int UID = 43709;
        //    bool insertNew = false;
        //    int PastHouseRefId = 0;
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        if (i > 0)
        //        {
        //            VehicleInfo oVehicleInfo = new VehicleInfo();
        //            string ServiceNo = "";
        //            if (xlRange.Cells[i, 1].Value != null)
        //            {
        //                ServiceNo = xlRange.Cells[i, 1].Value.ToString();

        //            }
        //            if (!string.IsNullOrEmpty(ServiceNo))
        //            {
        //                var filter = _vehicleInfoService.GetDefaultSpecification();
        //                filter = filter.And(p => p.Active == true).And(p => p.ServiceNo == ServiceNo);
        //                List<VehicleInfo> VehicleInfoList = _vehicleInfoService.GetCollection(filter, p => p.CreationDate).ToList();
        //                if (VehicleInfoList.Count > 0)
        //                {
        //                    foreach (VehicleInfo inV in VehicleInfoList)
        //                    {
        //                        VehicleInfo v = _vehicleInfoService.GetByKey(inV.UId);
        //                        v.Status = 2;
        //                        DataContext.SaveChanges();
        //                    }
        //                }
        //                else
        //                {
        //                    string s = "";
        //                }
        //            }


        //        }

        //        // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
        //        //}

        //        //add useful things here!   
        //        //}
        //    }
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    return View();
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RecentOrder()
        {
            List<MenuOrder> MenuOrderList = new List<MenuOrder>();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _menuOrderService.GetDefaultSpecification();
                filter = filter.And(p=>p.Active==true).And(p=>p.UserId== account.Id);
                MenuOrderList = _menuOrderService.GetCollection(filter, p => p.CreationDate).OrderByDescending(p => p.CreationDate).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return View(MenuOrderList);
        }
        public ActionResult TopPicks()
        {
            List<MenuOrder> MenuOrderList = new List<MenuOrder>();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _menuOrderService.GetDefaultSpecification().And(p=>p.SLAFLocationUId==account.LocationUId);
                filter = filter.And(p => p.Active == true).And(p=>p.Status==40);
                MenuOrderList = _menuOrderService.GetCollection(filter, p => p.CreationDate).OrderByDescending(p => p.CreationDate).Take(8).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return View(MenuOrderList);
        }

        public ActionResult MenuList()
        {
            UserAccount account = GetCurrentUser();
            List<MenuItem> MenuItemList = new List<MenuItem>();
            var filter = _menuItemService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p=>p.SLAFLocationUId==account.LocationUId);
            MenuItemList = _menuItemService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(MenuItemList);
        }
        public ActionResult RemoveOption(int id)
        {
            MenuOption opt = _menuOptionService.GetByKey(id);
            try
            {
                opt.Active = false;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Menu Updated.");
                return RedirectToAction("MenuDetail", new { id= opt.MenuItemUId});
            }
            catch (Exception)
            {

                throw;
            }
            return View(opt);
        }
        public ActionResult RemoveMultiOption(int id)
        {
            MenuMultiOption opt = _menuMultiOptionService.GetByKey(id);
            try
            {
                opt.Active = false;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Menu Updated.");
                return RedirectToAction("MenuDetail", new { id= opt.MenuItemUId});
            }
            catch (Exception)
            {

                throw;
            }
            return View(opt);
        }
        public ActionResult MenuDetail(int id)
        {
            MenuItem item = new MenuItem();
            try
            {
                BindMeasurementUnitList();
                item = _menuItemService.GetByKey(id);
                List<MenuOption> MenuOptionList = new List<MenuOption>();
                var filter = _menuOptionService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuItemUId == id);
                MenuOptionList = _menuOptionService.GetCollection(filter, p => p.CreationDate).ToList();
                TempData["MenuOptionList"] = MenuOptionList;

                List<MenuMultiOption> MenuMultiOptionList = new List<MenuMultiOption>();
                var filterOpt = _menuMultiOptionService.GetDefaultSpecification();
                filterOpt = filterOpt.And(p => p.Active == true).And(p => p.MenuItemUId == id);
                MenuMultiOptionList = _menuMultiOptionService.GetCollection(filterOpt, p => p.CreationDate).ToList();
                TempData["MenuMultiOptionList"] = MenuMultiOptionList;
            }
            catch (Exception)
            {

                throw;
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult MenuDetail(FormCollection Form,int id)
        {
            MenuItem item = new MenuItem();
            try
            {
                item = _menuItemService.GetByKey(id);
                TryUpdateModel(item);
                DataContext.SaveChanges();
                if (Form["menuparam"] != null)
                {
                    string option = Form["menuparam"].ToString();
                    if (!string.IsNullOrEmpty(option))
                    {
                        MenuOption mOpt = new MenuOption();
                        mOpt.MenuItemUId = id;
                        mOpt.Active = true;
                        mOpt.Parameter = option;
                        _menuOptionService.Add(mOpt);
                        DataContext.SaveChanges();
                    }
                }
                if (Form["multimenuparam"] != null)
                {
                    string option = Form["multimenuparam"].ToString();
                    if (!string.IsNullOrEmpty(option))
                    {
                        MenuMultiOption multiOpt = new MenuMultiOption();
                        multiOpt.MenuItemUId = id;
                        multiOpt.Active = true;
                        multiOpt.Parameter = option;
                        _menuMultiOptionService.Add(multiOpt);
                        DataContext.SaveChanges();
                    }
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully updated");
                return RedirectToAction("MenuList");
            }
            catch (Exception)
            {

                throw;
            }
            return View(item);
        }
    }
}
