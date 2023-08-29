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
using Dinota.Domain.BOCTransaction;
using Dinota.Domain.StockSheetTransaction;
using Dinota.Domain.F140Header;
using Dinota.Domain.F140Data;
using Dinota.Domain.SetMenuHeader;
using Dinota.Domain.SetMenuDetail;
using Dinota.Domain.MenuItem;


namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class SetMenuController : BaseController
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
        private readonly IngredientBOCService _ingredientBOCService;
        private readonly BOCTransactionService _bOCTransactionService;
        private readonly StockSheetTransactionService _stockSheetTransactionService;
        private readonly F140HeaderService _f140HeaderService;
        private readonly F140DataService _f140DataService;
        private readonly SetMenuHeaderService _setMenuHeaderService;
        private readonly SetMenuDetailService _setMenuDetailService;
        private readonly MenuItemService _menuItemService;


        public SetMenuController(IDomainContext dataContext, SetMenuDetailService setMenuDetailService, 
            SetMenuHeaderService setMenuHeaderService, F140DataService f140DataService, F140HeaderService f140HeaderService, 
            StockSheetTransactionService stockSheetTransactionService, BOCTransactionService bOCTransactionService, 
            IngredientBOCService ingredientBOCService, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, 
            UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, 
            SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, 
            UserTypeService userTypeService, DivisionService divisionService, MenuItemService menuItemService)
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
            _bOCTransactionService = bOCTransactionService;
            _stockSheetTransactionService = stockSheetTransactionService;
            _f140HeaderService = f140HeaderService;
            _f140DataService = f140DataService;
            _setMenuHeaderService = setMenuHeaderService;
            _setMenuDetailService = setMenuDetailService;
            _menuItemService = menuItemService;
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
       
        public ActionResult SetMenuEdit(int id)
        {
            SetMenuHeader oSetMenuHeader = new SetMenuHeader();
            oSetMenuHeader = _setMenuHeaderService.GetByKey(id);
            try
            {
                oSetMenuHeader = _setMenuHeaderService.GetByKey(id);
                
            }
            catch (Exception)
            {

                throw;
            }
            return View(oSetMenuHeader);
        }
       
        [HttpPost]
        public ActionResult SetMenuEdit(FormCollection Form,int id)
        {
            SetMenuHeader oSetMenuHeader = new SetMenuHeader();

            try
            {
                TryUpdateModel(oSetMenuHeader);
                BindMeasurementList();

                oSetMenuHeader = _setMenuHeaderService.GetByKey(id);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                return RedirectToAction("SetMenuList");
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public ActionResult SetMenuRegister()
        {
            SetMenuHeader oSetMenuHeader = new SetMenuHeader();
            try
            {
                TryUpdateModel(oSetMenuHeader);
                //info.Active = true;
            }
            catch (Exception)
            {

                throw;
            }
            return View(oSetMenuHeader);
        }
        [HttpPost]
        public ActionResult SetMenuRegister(FormCollection Form)
        {
            SetMenuHeader oSetMenuHeader = new SetMenuHeader();
            try
            {
                TryUpdateModel(oSetMenuHeader);
                UserAccount account = GetCurrentUser();
           
                oSetMenuHeader.Active = true;
                oSetMenuHeader.LocationUId = account.LocationUId;
                _setMenuHeaderService.Add(oSetMenuHeader);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Saved");
                return RedirectToAction("SetMenuList");
                
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ActionResult SetMenuPackageList(int id)
        {
            SetMenuModel model = new SetMenuModel();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _menuItemService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.SLAFLocationUId == account.LocationUId);
                List<MenuItem> MenuitemList = _menuItemService.GetCollection(filter, p => p.CreationDate).ToList();
                model.MenuItemList = MenuitemList;

                var filterM = _setMenuDetailService.GetDefaultSpecification();
                filterM = filterM.And(p => p.Active == true).And(p => p.SetMenuHeaderId == id);
                List<SetMenuDetail> MenuPackageList = _setMenuDetailService.GetCollection(filterM, p => p.CreationDate).ToList();
                model.SetMenuDetailList = MenuPackageList;
                model.MenuItemId = id;

                SetMenuHeader item = _setMenuHeaderService.GetByKey(id);
                TempData["MenuItem"] = item.SetMenuName;
                TempData["MenuItemId"] = item.UId;

            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AddPackageSetMenu(int MenuId, int MenuItemId)
        {
            try
            {
                UserAccount account = GetCurrentUser();
                SetMenuDetail package = new SetMenuDetail();

                package.MenuItemId = MenuId;
                package.SetMenuHeaderId = MenuItemId;
                package.LocationUId = account.LocationUId;
                package.Active = true;
                _setMenuDetailService.Add(package);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Menu Updated.");
                return RedirectToAction("SetMenuPackageList", new { id = MenuItemId });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public ActionResult RemovePackageSetMenu(int id)
        {
            try
            {
                SetMenuDetail package = _setMenuDetailService.GetByKey(id);
                package.Active = false;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Menu Updated.");
                return RedirectToAction("SetMenuPackageList", new { id = package.SetMenuHeaderId });
            }
            catch (Exception)
            {

                throw;
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
        public ActionResult SetMenuList()
        {
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _setMenuHeaderService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.LocationUId == account.LocationUId);
                List<SetMenuHeader> SetMenuHeaderList = _setMenuHeaderService.GetCollection(filter, p => p.CreationDate).ToList();
                return View(SetMenuHeaderList);
            }
            catch(Exception e)
            {
                throw;
            }

        }


       

    }
}
