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
using Dinota.Domain.Supplier;
using Dinota.Domain.SubscriptionFee;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class SubscriptionController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        private readonly DistrictService _districtService;
        private readonly SLAFLocationService _SLAFLocationService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly RoomInfoService _roomInfoService;
        private readonly RoomNoService _roomNoService;
        private readonly SupplierService _supplierService;
        private readonly SubscriptionFeeService _subscriptionFeeService;

        public SubscriptionController(IDomainContext dataContext, SubscriptionFeeService subscriptionFeeService, SupplierService supplierService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
            _supplierService = supplierService;
            _subscriptionFeeService = subscriptionFeeService;
        }
        // [AuthorizeUserAccessLevel()]



       
        public ActionResult Edit(int id)
        {
            SubscriptionFee info = new SubscriptionFee();
            info = _subscriptionFeeService.GetByKey(id);
            try
            {
                //BindRoomNoList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection Form,int id)
        {
            SubscriptionFee info = new SubscriptionFee();
            info = _subscriptionFeeService.GetByKey(id);
            try
            {
                TryUpdateModel(info);
                //BindRoomNoList();
                
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("Index");
                
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        public ActionResult Register()
        {
            SubscriptionFee info = new SubscriptionFee();
            try
            {
                TryUpdateModel(info);
                //BindRoomNoList();
                info.Active = true;
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
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
        [HttpPost]
        public ActionResult Register(FormCollection Form)
        {
            SubscriptionFee info = new SubscriptionFee();
            try
            {

                TryUpdateModel(info);
                UserAccount account = GetCurrentUser();
                    info.Active = true;
                    info.LocationId = account.LocationUId;
                    _subscriptionFeeService.Add(info);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("Index");
                
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        public ActionResult Index()
        {
            UserAccount account = GetCurrentUser();
            var filter = _subscriptionFeeService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true).And(p=>p.LocationId== account.LocationUId);
            List<SubscriptionFee> SubscriptionFeeList = _subscriptionFeeService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(SubscriptionFeeList);
        }
    }
}
