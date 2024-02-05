using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MIMS.Helpers;
using MIMS.Security;
using Dinota.Core.Data;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Core.Specification;
using Dinota.Security;
using System;
using AlfasiWeb.Models;
using Dinota.Domain.Division;
using Dinota.Domain.UserType;
using Dinota.Domain.District;
using Dinota.Domain.SLAFLocation;
using Dinota.Domain.FuelType;
using Dinota.Domain.RoomInfo;
using Dinota.Domain.RoomNo;
using Dinota.Domain.UserStatus;
using Dinota.Domain.OfficerRequest;
using Dinota.Domain.MenuItem;
using Dinota.Domain.PaymentMethod;
using Dinota.Domain.MenuOrderOfficer;
using AlfasiWeb;
using Dinota.Domain.MenuOrderHeader;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.Rank;
using Dinota.Domain.BarRecovery;
using Dinota.Domain.SubscriptionFee;
using Dinota.Domain.UserTrn;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class OfficerController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        private readonly DistrictService _districtService;
        private readonly SLAFLocationService _SLAFLocationService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly RoomInfoService _roomInfoService;
        private readonly RoomNoService _roomNoService;
        private readonly UserStatusService _userStatusService;
        private readonly MenuItemService _menuItemService;
        private readonly OfficerRequestService _officerRequestService;
        private readonly PaymentMethodService _paymentMethodService;
        private readonly MenuOrderOfficerService _menuOrderOfficerService;
        private readonly MenuOrderHeaderService _menuOrderHeaderService;
        private readonly MenuOrderItemDetailService _menuOrderItemDetailService;
        private readonly BarRecoveryService _barRecoveryService;
        private readonly SubscriptionFeeService _subscriptionFeeService;
        private readonly RankService _rankService;
        private readonly UserTrnService _userTrnService;

        public OfficerController(IDomainContext dataContext, UserTrnService userTrnService, SubscriptionFeeService subscriptionFeeService, BarRecoveryService barRecoveryService, MenuOrderItemDetailService menuOrderItemDetailService, 
            MenuOrderHeaderService menuOrderHeaderService, MenuOrderOfficerService menuOrderOfficerService, 
            PaymentMethodService paymentMethodService, MenuItemService menuItemService, OfficerRequestService officerRequestService, 
            UserStatusService userStatusService, UserAccountService userAccountService, RoomNoService roomNoService, 
            RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, 
            DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService, RankService rankService)
            : base(dataContext)
        {
            _divisionService = divisionService;
            _userTypeService = userTypeService;
            _districtService = districtService;
            _fuelTypeService = fuelTypeService;
            _SLAFLocationService = SLAFLocationService;
            _roomInfoService = roomInfoService;
            _roomNoService = roomNoService;
            _userAccountService = userAccountService;
            _userStatusService = userStatusService;
            _officerRequestService = officerRequestService;
            _menuItemService =menuItemService;
            _paymentMethodService = paymentMethodService;
            _menuOrderOfficerService = menuOrderOfficerService;
            _menuOrderHeaderService = menuOrderHeaderService;
            _menuOrderItemDetailService =menuOrderItemDetailService;
            _rankService = rankService;
            _barRecoveryService = barRecoveryService;
            _subscriptionFeeService = subscriptionFeeService;
            _userTrnService = userTrnService;
        }
        // [AuthorizeUserAccessLevel()]

        public void BindMainMealItemList(int accountid)
        {
            try
            {
                var filter = _menuItemService.GetDefaultSpecification().And(s => s.Active == true).And(p=>p.IsCombine==true).And(p=>p.SLAFLocationUId== accountid);
                var TypeList = _menuItemService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Name");
                ViewData[ViewDataKeys.MenuItemList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public void BindPaymentMethodList()
        {
            try
            {
                var filter = _paymentMethodService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _paymentMethodService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Method");
                ViewData[ViewDataKeys.PaymentMethodList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

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
        public ActionResult StatusHistory(int id)
        {
            List<UserTrn> UserTrnList = new List<UserTrn>();
            var filter = _userTrnService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p => p.Active == true);
            UserTrnList = _userTrnService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(UserTrnList);
        }
        public ActionResult OfficerList()
        {
            UserAccount account = GetCurrentUser();
            var filter = _userAccountService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true).And(p=>p.LocationUId== account.LocationUId);
            List<UserAccount> UserAccountList = _userAccountService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(UserAccountList);
        }
        public void BindUserStatusList()
        {
            try
            {
                var filter = _userStatusService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _userStatusService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Status");
                ViewData[ViewDataKeys.UserStatusList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public void BindRankList()
        {
            try
            {
                var filter = _rankService.GetDefaultSpecification().And(s => s.Active == true); ;
                var RankList = _rankService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(RankList, "UId", "Name");
                ViewData[ViewDataKeys.RankList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public ActionResult UpdateStatus(int id)
        {
            UserAccount account = _userAccountService.GetByKey(id);
            BindUserStatusList();
            BindRankList();
            return View(account);
        }
        [HttpPost]
        public ActionResult UpdateStatus(FormCollection Form,int id)
        {
            int LivingStatus = 0;

            UserAccount account = _userAccountService.GetByKey(id);
            LivingStatus = account.LivingStatus;
            BindUserStatusList();
            TryUpdateModel(account);
            
            DataContext.SaveChanges();
            if (account.LivingStatus != LivingStatus) {
                UserTrn trn = new UserTrn();
                trn.EffectiveDate = DateTime.Now;
                trn.Active = true;
                trn.StatusId = account.LivingStatus;
                trn.UserId = account.Id;
                _userTrnService.Add(trn);
                DataContext.SaveChanges();
            }
           
            
            TempData[ViewDataKeys.Message] = new SuccessMessage("Officer Status successfully updated");

            return RedirectToAction("OfficerList");
            return View(account);
        }

        public ActionResult UpdateProfile(int id)
        {
            UserAccount account = _userAccountService.GetByKey(id);
            BindUserStatusList();
            return View(account);
        }
        [HttpPost]
        public ActionResult UpdateProfile(FormCollection Form, int id)
        {
            try
            {
                UserAccount account = _userAccountService.GetByKey(id);
                BindUserStatusList();
                TryUpdateModel(account);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer Details successfully updated");

                return RedirectToAction("Profile", "Home");
                return View(account);

            }
            catch (Exception)
            {
                return RedirectToAction("Profile", "Home");
            }
            
        }

        public ActionResult CreateRequest()
        {
            OfficerRequest profile = new OfficerRequest();
            return View(profile);
        }
        public ActionResult AddOfficer()
        {
            OfficerAddProfile profile = new OfficerAddProfile();
            return View(profile);
        }
        [HttpPost]
        public ActionResult AddOfficer(FormCollection Form)
        {
            OfficerAddProfile profile = new OfficerAddProfile();
            try
            {
                UserAccount user = GetCurrentUser();
                TryUpdateModel(profile);
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.ServiceNo == profile.ServiceNo).And(p => p.Active == true);
                UserAccount account = _userAccountService.GetBy(filter);
                if (account != null)
                {
                    UserAccount updateacc = _userAccountService.GetByKey(account.Id);
                    updateacc.LocationUId = user.LocationUId;
                    DataContext.SaveChanges();
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer successfully added");

                return RedirectToAction("OfficerList");
            }
            catch (Exception)
            {

                throw;
            }
            return View(profile);
        }
        public ActionResult AddOfficerRequest(int id)
        {
            OfficerRequest profile = new OfficerRequest();
            UserAccount account = _userAccountService.GetByKey(id);
            BindMainMealItemList(account.LocationUId);
            BindPaymentMethodList();
            if (id > 0)
            {
               account = _userAccountService.GetByKey(id);
               profile.UserId = account.Id;
               profile.UserAccount = account;
            }
           

            return View(profile);
        }

        public ActionResult AddOfficerWarningOut(int id)
        {
            return View();
        }
        public ActionResult OfficerRequesMessList(int id)
        {
            List<OfficerRequest> profile = new List<OfficerRequest>();
            UserAccount account = GetCurrentUser();
            var filter = _officerRequestService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p=>p.UserId==id);
            profile = _officerRequestService.GetCollection(filter, p => p.CreationDate).ToList();
            TempData["ÜserId"] = account.Id;
            return View(profile);
        }
        public ActionResult OfficerRequestList()
        {
            List<OfficerRequest> profile = new List<OfficerRequest>();
            UserAccount account = new UserAccount();
            var filter = _officerRequestService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true);
            profile = _officerRequestService.GetCollection(filter, p => p.CreationDate).ToList();

            return View(profile);
        }
        [HttpPost]
        public ActionResult AddOfficerRequest(FormCollection Form,int id)
        {
            OfficerRequest profile = new OfficerRequest();
            try
            {
                UserAccount user = _userAccountService.GetByKey(id);
                TryUpdateModel(profile);
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.Id == id).And(p => p.Active == true);
                UserAccount account = _userAccountService.GetBy(filter);
                if (account != null)
                {
                    account.LocationUId = user.LocationUId;
                    profile.Active = true;
                    profile.UserId = id;
                    profile.Status = 10;
                    _officerRequestService.Add(profile);
                    DataContext.SaveChanges();
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Record successfully added");

                return RedirectToAction("OfficerRequesMessList", new { id= id });
            }
            catch (Exception)
            {

                throw;
            }
            return View(profile);
        }
        public ActionResult WarningOut(int id)
        {
            OfficerRequest oOfficerRequest = new OfficerRequest();
            try
            {
                oOfficerRequest = _officerRequestService.GetByKey(id);

            }
            catch (Exception)
            {

                throw;
            }
            return View(oOfficerRequest);
        }
        [HttpPost]
        public ActionResult WarningOut(FormCollection Form,int id)
        {
            OfficerRequest oOfficerRequest = new OfficerRequest();
            try
            {
                oOfficerRequest = _officerRequestService.GetByKey(id);
                TryUpdateModel(oOfficerRequest);
                DataContext.SaveChanges();

                TempData[ViewDataKeys.Message] = new SuccessMessage("Record successfully added");

                return RedirectToAction("OfficerList");
            }
            catch (Exception)
            {

                throw;
            }
            return View(oOfficerRequest);
        }
        public ActionResult OfficerRecoveryList()
        {
            OfficerMessBillModel model = new OfficerMessBillModel();
            List<OffcerRecoveryList> ModelList = new List<OffcerRecoveryList>();
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                UserAccount account = GetCurrentUser();
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p=>p.LocationUId==account.LocationUId);
                List<UserAccount> UserAccountList = _userAccountService.GetCollection(filter, p => p.CreationDate).OrderBy(p => p.RankUId).ToList();
                int i = 0;
                List<string> SubcritionHeading = new List<string>();
                foreach (UserAccount acc in UserAccountList)
                {
                    i++;
                    decimal Amount = 0;
                    OffcerRecoveryList off = new OffcerRecoveryList();
                    off.oUserAccount = acc;
                    var filterOS = _menuOrderOfficerService.GetDefaultSpecification();
                    filterOS = filterOS.And(p => p.Active == true).And(p => p.UserId == acc.Id).And(p => p.MenuOrderHeader.Status == (int)DataStruct.MenuOrderItemStatus.Delivered).And(p => p.MenuOrderHeader.OrderDate >= firstDayOfMonth).And(p => p.MenuOrderHeader.OrderDate <= lastDayOfMonth);
                    List<MenuOrderOfficer> MenuOrderOfficerList = _menuOrderOfficerService.GetCollection(filterOS, p => p.CreationDate).ToList();
                    foreach (MenuOrderOfficer head in MenuOrderOfficerList)
                    {
                        MenuOrderHeaderDetailModel det = new MenuOrderHeaderDetailModel();
                        MenuOrderHeader header = _menuOrderHeaderService.GetByKey(head.MeanuOrderHeaderUId);
                        var filterO = _menuOrderOfficerService.GetDefaultSpecification();
                        filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == head.MeanuOrderHeaderUId);
                        List<MenuOrderOfficer> TotalOfficerList = _menuOrderOfficerService.GetCollection(filterO, p => p.CreationDate).ToList();
                        if (TotalOfficerList.Count == 1)
                        {
                            Amount += header.F140TotalAmt;
                        }
                        else if (TotalOfficerList.Count > 1)
                        {
                            decimal TotAmt = header.F140TotalAmt;
                            decimal IndivisualAmt = TotAmt / TotalOfficerList.Count;
                            Amount += IndivisualAmt;
                        }

                    }
                    off.MessBill = Amount;
                    var filterB = _barRecoveryService.GetDefaultSpecification();
                    filterB = filterB.And(p => p.Active == true).And(p => p.Year == firstDayOfMonth.Year).And(p => p.Month == firstDayOfMonth.Month).And(p => p.UserId == acc.Id);
                    BarRecovery recovery = _barRecoveryService.GetBy(filterB);
                    if (recovery != null)
                    {
                        off.Barbill = recovery.CreditAmnt;
                    }
                    else
                    {
                        off.Barbill = 0;
                    }
                    off.WelfareshopBill = 0;
                    var filterS = _subscriptionFeeService.GetDefaultSpecification();
                    filterS = filterS.And(p => p.Active == true).And(p => p.LocationId == account.LocationUId);
                    List<SubscriptionFee> SubscriptionFeeList = _subscriptionFeeService.GetCollection(filterS, p => p.CreationDate).ToList();
                    List<decimal> SubcritionAmount = new List<decimal>();

                    if (i == 1)
                    {


                        foreach (SubscriptionFee feecat in SubscriptionFeeList)
                        {
                            string s = feecat.Category;
                            decimal amt = feecat.Fee;
                            SubcritionHeading.Add(s);
                            SubcritionAmount.Add(amt);
                        }
                    }
                    else
                    {
                        foreach (SubscriptionFee feecat in SubscriptionFeeList)
                        {
                            string s = feecat.Category;
                            decimal amt = feecat.Fee;
                            SubcritionAmount.Add(amt);
                        }
                    }

                    off.Subscription = SubscriptionFeeList.Sum(p => p.Fee);
                    off.SubcritionAmount = SubcritionAmount;
                    ModelList.Add(off);
                    
                }
                model.SubcritionHeading = SubcritionHeading;
                model.OffcerRecoveryList = ModelList;

            }
            catch (Exception)
            {
                throw;
            }
            return View(model); 
        }

        public ActionResult OfficerRecoveryListNew()
        {
            List<OffcerRecoveryList> ModelList = new List<OffcerRecoveryList>();
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                UserAccount account = GetCurrentUser();
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.LocationUId >= 0).And(p => p.UserMode == null);
                List<UserAccount> UserAccountList = _userAccountService.GetCollection(filter, p => p.CreationDate).OrderBy(p => p.RankUId).ToList();
                foreach (UserAccount acc in UserAccountList)
                {
                    decimal Amount = 0;
                    OffcerRecoveryList off = new OffcerRecoveryList();                    
                    var filterOS = _menuOrderOfficerService.GetDefaultSpecification();
                    filterOS = filterOS.And(p => p.Active == true).And(p => p.UserId == acc.Id).And(p => p.MenuOrderHeader.LocationUId == account.LocationUId)
                                        .And(p => p.MenuOrderHeader.Status == (int)DataStruct.MenuOrderItemStatus.Delivered)
                                        .And(p => p.MenuOrderHeader.OrderDate >= firstDayOfMonth).And(p => p.MenuOrderHeader.OrderDate <= lastDayOfMonth);
                    List<MenuOrderOfficer> MenuOrderOfficerList = _menuOrderOfficerService.GetCollection(filterOS, p => p.CreationDate).ToList();
                    foreach (MenuOrderOfficer head in MenuOrderOfficerList)
                    {
                        MenuOrderHeaderDetailModel det = new MenuOrderHeaderDetailModel();
                        MenuOrderHeader header = _menuOrderHeaderService.GetByKey(head.MeanuOrderHeaderUId);
                        var filterO = _menuOrderOfficerService.GetDefaultSpecification();
                        filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == head.MeanuOrderHeaderUId);
                        List<MenuOrderOfficer> TotalOfficerList = _menuOrderOfficerService.GetCollection(filterO, p => p.CreationDate).ToList();
                        if (TotalOfficerList.Count == 1)
                        {
                            Amount += header.F140TotalAmt;
                        }
                        else if (TotalOfficerList.Count > 1)
                        {
                            decimal TotAmt = header.F140TotalAmt;
                            decimal IndivisualAmt = TotAmt / TotalOfficerList.Count;
                            Amount += IndivisualAmt;
                        }

                    }
                    off.oUserAccount = acc;
                    off.MessBill = Amount;
                    if (acc.LocationUId == account.LocationUId)
                    {
                        ModelList.Add(off);
                    }
                    else if (Amount > 0)
                    {
                        ModelList.Add(off);
                    }
                    else 
                    {
                        //TempData[ViewDataKeys.Message] = new SuccessMessage("Check the list " + acc.UserName);
                        //return RedirectToAction("OfficerRecoveryListNew");
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return View(ModelList);
        }
    }
}
