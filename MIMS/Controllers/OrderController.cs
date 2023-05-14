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
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuOrderHeader;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.MenuOrderOfficer;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class OrderController : BaseController
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
        private readonly MenuItemService _menuItemService; 
        private readonly MenuOrderHeaderService _menuOrderHeaderService;
        private readonly MenuOrderItemDetailService _menuOrderItemDetailService;
        private readonly MenuOrderOfficerService _menuOrderOfficerService;

        public OrderController(IDomainContext dataContext, MenuOrderOfficerService menuOrderOfficerService, MenuOrderItemDetailService menuOrderItemDetailService, MenuOrderHeaderService menuOrderHeaderService, MenuItemService menuItemService, BOCTransactionService bOCTransactionService, IngredientBOCService ingredientBOCService, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
            _menuItemService = menuItemService;
            _menuOrderHeaderService = menuOrderHeaderService;
            _menuOrderItemDetailService = menuOrderItemDetailService;
            _menuOrderOfficerService = menuOrderOfficerService;
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
        public ActionResult OrderList()
        {
            List<MenuOrderHeader> MenuOrderHeaderList = new List<MenuOrderHeader>();
            try
            {
                var filter = _menuOrderHeaderService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true);
                MenuOrderHeaderList = _menuOrderHeaderService.GetCollection(filter, p => p.CreationDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(MenuOrderHeaderList);
        }
        public ActionResult OrderRegister()
        {
            MenuOrderHeader header = new MenuOrderHeader();
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return View(header);
        }
        [HttpPost]
        public ActionResult OrderRegister(FormCollection Form)
        {
            MenuOrderHeader header = new MenuOrderHeader();
            try
            {
                TryUpdateModel(header);
                header.OrderDate = DateTime.Now;
                header.Active = true;
                _menuOrderHeaderService.Add(header);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order created. Add menu items");
                return RedirectToAction("MenuCreate",new { id= header.UId});
            }
            catch (Exception)
            {

                throw;
            }
            return View(header);
        }
        public ActionResult MenuCreate(int id)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                List<MenuItem> MenuItemList = new List<MenuItem>();
                UserAccount account = GetCurrentUser();
                var filter = _menuItemService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.SLAFLocationUId == account.LocationUId);
                MenuItemList = _menuItemService.GetCollection(filter, p => p.CreationDate).ToList();
                model.MenuItemList = MenuItemList;
                model.MenuOrderId = id;
                var filterO = _menuOrderItemDetailService.GetDefaultSpecification();
                filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                model.MenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterO, p => p.CreationDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult MenuOrder(int MenuItemId,int OrderId)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                MenuItem item = _menuItemService.GetByKey(MenuItemId);
                model.MenuItem = item;
                model.MenuOrderId = OrderId;
                
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult MenuOrder(FormCollection Form,int id)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                TryUpdateModel(model);
                int MenuId = int.Parse(Form["MenuItem.UId"].ToString());
                decimal Qty = 0;
                string remark = "";
                DateTime date = DateTime.Now;
                if (Form["menucount"] != null)
                {
                    Qty = decimal.Parse(Form["menucount"].ToString());
                 } 
               
                if (Form["remark"] != null)
                {
                    remark = Form["remark"].ToString();
                }
                MenuOrderItemDetail detail = new MenuOrderItemDetail();
                detail.MenuItemUId = MenuId;
                MenuItem item = _menuItemService.GetByKey(MenuId);
                detail.Qty = Qty;
                detail.MeanuOrderHeaderUId = id;
                detail.Remark = remark;
                detail.Active = true;
                _menuOrderItemDetailService.Add(detail);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage(item.Name+ "added to the order. Add more menu items");
                return RedirectToAction("MenuCreate",new { id });
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public ActionResult OfficerSearch()
        {
            List<UserAccount> UserAccountList = new List<UserAccount>();
            try
            {
                UserAccount account = GetCurrentUser();
               
            }
            catch (Exception)
            {

                throw;
            }
            return View(UserAccountList);   
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
    }
}
