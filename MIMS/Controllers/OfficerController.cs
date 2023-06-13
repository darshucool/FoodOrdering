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

        public OfficerController(IDomainContext dataContext, UserStatusService userStatusService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
        }
        // [AuthorizeUserAccessLevel()]



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
        public ActionResult UpdateStatus(int id)
        {
            UserAccount account = _userAccountService.GetByKey(id);
            BindUserStatusList();
            return View(account);
        }
        [HttpPost]
        public ActionResult UpdateStatus(FormCollection Form,int id)
        {
            UserAccount account = _userAccountService.GetByKey(id);
            BindUserStatusList();
            TryUpdateModel(account);
            DataContext.SaveChanges();
            TempData[ViewDataKeys.Message] = new SuccessMessage("Officer Status successfully updated");

            return RedirectToAction("OfficerList");
            return View(account);
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
                    account.LocationUId = user.LocationUId;
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
    }
}
