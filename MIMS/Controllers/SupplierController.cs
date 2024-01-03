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

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class SupplierController : BaseController
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

        public SupplierController(IDomainContext dataContext, SupplierService supplierService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
        }
        // [AuthorizeUserAccessLevel()]



        public void BindRoomNoList()
        {
            try
            {
                var filter = _roomNoService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _roomNoService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Room");
                ViewData[ViewDataKeys.RoomNoList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public ActionResult Edit(int id)
        {
            Supplier info = new Supplier();
            info = _supplierService.GetByKey(id);
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
            Supplier info = new Supplier();
            info = _supplierService.GetByKey(id);
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
            Supplier info = new Supplier();
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
            Supplier info = new Supplier();
            try
            {
                BindRoomNoList();
                TryUpdateModel(info);
                UserAccount account = GetCurrentUser();
                    info.Active = true;
                    info.LocationUId = account.LocationUId;
                    _supplierService.Add(info);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("Register");
                
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
            var filter = _supplierService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true).And(p=>p.LocationUId== account.LocationUId);
            List<Supplier> SupplierList = _supplierService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(SupplierList);
        }
    }
}
