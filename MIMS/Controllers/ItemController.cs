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
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MeasurementUnit;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class ItemController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        private readonly DistrictService _districtService;
        private readonly SLAFLocationService _SLAFLocationService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly RoomInfoService _roomInfoService;
        private readonly RoomNoService _roomNoService;
        private readonly IngredientInfoService _ingredientInfoService;
        private readonly MeasurementUnitService _measurementUnitService;

        public ItemController(IDomainContext dataContext, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
            _ingredientInfoService = ingredientInfoService;
            _measurementUnitService = measurementUnitService;
        }
        // [AuthorizeUserAccessLevel()]



        public void BindMeasurementList()
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
        public ActionResult IngredientEdit(int id)
        {
            IngredientInfo info = new IngredientInfo();
            info = _ingredientInfoService.GetByKey(id);
            try
            {
                BindMeasurementList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult IngredientEdit(FormCollection Form,int id)
        {
            IngredientInfo info = new IngredientInfo();
            info = _ingredientInfoService.GetByKey(id);
            try
            {
                TryUpdateModel(info);
                BindMeasurementList();
                
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
        public ActionResult IngredientRegister()
        {
            IngredientInfo info = new IngredientInfo();
            try
            {
                TryUpdateModel(info);
                BindMeasurementList();
                info.Active = true;
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult IngredientRegister(FormCollection Form)
        {
            IngredientInfo info = new IngredientInfo();
            try
            {
                BindMeasurementList();
                TryUpdateModel(info);
                UserAccount account = GetCurrentUser();


                    info.Active = true;
                    info.LocationUId = account.LocationUId;
                    _ingredientInfoService.Add(info);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("IngredientRegister");
                
                
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
        public ActionResult Index()
        {
            var filter = _ingredientInfoService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true);
            List<IngredientInfo> IngredientInfoList = _ingredientInfoService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(IngredientInfoList);
        }
    }
}
