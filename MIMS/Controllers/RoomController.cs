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

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class RoomController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        private readonly DistrictService _districtService;
        private readonly SLAFLocationService _SLAFLocationService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly RoomInfoService _roomInfoService;
        private readonly RoomNoService _roomNoService;

        public RoomController(IDomainContext dataContext, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
        public ActionResult RoomInfoEdit(int id)
        {
            RoomInfo info = new RoomInfo();
            info = _roomInfoService.GetByKey(id);
            try
            {
                BindRoomNoList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult RoomInfoEdit(FormCollection Form,int id)
        {
            RoomInfo info = new RoomInfo();
            info = _roomInfoService.GetByKey(id);
            try
            {
                TryUpdateModel(info);
                BindRoomNoList();
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.UserName == info.ServiceNo).And(p => p.Active == true);
                UserAccount acc = _userAccountService.GetBy(filter);
                if (acc == null)
                {
                    TempData[ViewDataKeys.Message] = new FailMessage("Service No does not exist in User Table");
                    return View(info);
                }
                else
                {
                    info.UserUId = acc.Id;
                    info.Name = acc.Name;
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        public ActionResult RoomInfoRegister()
        {
            RoomInfo info = new RoomInfo();
            try
            {
                TryUpdateModel(info);
                BindRoomNoList();
                info.Active = true;
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult RoomInfoRegister(FormCollection Form)
        {
            RoomInfo info = new RoomInfo();
            try
            {
                BindRoomNoList();
                TryUpdateModel(info);
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p=>p.UserName==info.ServiceNo).And(p=>p.Active==true);
                UserAccount acc = _userAccountService.GetBy(filter);
                if (acc == null)
                {
                    TempData[ViewDataKeys.Message] = new FailMessage("Service No does not exist in User Table");
                    return View(info);
                }
                else
                {
                    info.UserUId = acc.Id;
                    info.Name = acc.Name;
                    info.Active = true;
                    info.LocationUId = 18;
                    _roomInfoService.Add(info);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("RoomInfoRegister");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        public ActionResult Index()
        {
            var filter = _roomInfoService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true);
            List<RoomInfo> RoomInfoList = _roomInfoService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(RoomInfoList);
        }
    }
}
