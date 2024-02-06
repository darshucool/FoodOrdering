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
using Dinota.Domain.IngredientBOC;
using AlfasiWeb;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class ItemBOCController : BaseController
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
        private readonly IngredientBOCService _ingredientBOCService;
        private readonly MeasurementUnitService _measurementUnitService;

        public ItemBOCController(IDomainContext dataContext, IngredientBOCService ingredientBOCService, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
            _ingredientBOCService = ingredientBOCService;
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
        public void BindItemList()
        {
            try
            {
                var filter = _ingredientInfoService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _ingredientInfoService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "ItemName");
                ViewData[ViewDataKeys.IngredientInfoList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
       
        public ActionResult IngredientBOCEdit(int id)
        {
            IngredientBOC info = new IngredientBOC();
            info = _ingredientBOCService.GetByKey(id);
            try
            {
                TempData["Unit"] = info.MeasurementUnit.Unit;
                TempData["ItemName"] = info.IngredientInfo.ItemName;
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        [HttpPost]
        public ActionResult IngredientBOCEdit(FormCollection Form,int id)
        {
            IngredientBOC info = new IngredientBOC();
            info = _ingredientBOCService.GetByKey(id);
            try
            {
                TempData["Unit"] = info.MeasurementUnit.Unit;
                TempData["ItemName"] = info.IngredientInfo.ItemName;
                TryUpdateModel(info);

                info.TotalPrice = info.Qty * info.Price;
                DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                    return RedirectToAction("BOCList",new{ id= info.IngredientUId});
                
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);
        }
        public ActionResult IngredientBOCRegister(int id)
        {
            IngredientBOC oIngredientBOC = new IngredientBOC();
            try
            {
                IngredientInfo info = _ingredientInfoService.GetByKey(id);
                oIngredientBOC.IngredientUId = id;
                oIngredientBOC.TransactionType = (int)DataStruct.BOCTransactionType.BOC;
                oIngredientBOC.UnitId = info.MeasurementUnitUId;
                TempData["Unit"] = info.MeasurementUnit.Unit;
                TempData["ItemName"] = info.ItemName;
            }
            catch (Exception)
            {

                throw;
            }
            return View(oIngredientBOC);
        }

        [HttpPost]
        public ActionResult IngredientBOCRegister(FormCollection Form)
        {
            IngredientBOC oIngredientBOC = new IngredientBOC();
            try
            {

                TryUpdateModel(oIngredientBOC);



                oIngredientBOC.Active = true;
                oIngredientBOC.TotalPrice = oIngredientBOC.Qty * oIngredientBOC.Price;
                oIngredientBOC.TransactionType = (int)DataStruct.BOCTransactionType.BOC;
                _ingredientBOCService.Add(oIngredientBOC);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                return RedirectToAction("IngredientBOCRegister", new { oIngredientBOC.IngredientUId });


            }
            catch (Exception)
            {

                throw;
            }
            return View(oIngredientBOC);
        }

        public ActionResult IngredientCRVRegister(int id)
        {
            IngredientBOC oIngredientBOC = new IngredientBOC();
            try
            {
                IngredientInfo info = _ingredientInfoService.GetByKey(id);
                oIngredientBOC.IngredientUId = id;
                oIngredientBOC.TransactionType = (int)DataStruct.BOCTransactionType.CRV;
                oIngredientBOC.UnitId = info.MeasurementUnitUId;
                TempData["Unit"] = info.MeasurementUnit.Unit;
                TempData["ItemName"] = info.ItemName;
            }
            catch (Exception)
            {

                throw;
            }
            return View(oIngredientBOC);
        }
        
        [HttpPost]
        public ActionResult IngredientCRVRegister(FormCollection Form)
        {
            IngredientBOC oIngredientBOC = new IngredientBOC();
            try
            {

                TryUpdateModel(oIngredientBOC);



                oIngredientBOC.Active = true;
                oIngredientBOC.TotalPrice = oIngredientBOC.Qty * oIngredientBOC.Price;
                oIngredientBOC.TransactionType = (int)DataStruct.BOCTransactionType.CRV;
                _ingredientBOCService.Add(oIngredientBOC);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                return RedirectToAction("IngredientCRVRegister", new { oIngredientBOC.IngredientUId });


            }
            catch (Exception)
            {

                throw;
            }
            return View(oIngredientBOC);
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
            UserAccount account = GetCurrentUser();
            var filter = _ingredientInfoService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true).And(p=>p.LocationUId== account.LocationUId);
            List<IngredientInfo> IngredientList = _ingredientInfoService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(IngredientList);
        }
        public ActionResult BOCList(int id)
        {
            var filter = _ingredientBOCService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p=>p.IngredientUId== id);
            List<IngredientBOC> IngredientList = _ingredientBOCService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(IngredientList);
        }

        public ActionResult ItemRemainingBOCValue()
        {
            return View();
        }
    }
}
