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
using Dinota.Domain.MenuPackage;
using Dinota.Domain.MenuItemDetail;
using Dinota.Domain.F140Header;
using Dinota.Domain.F140Data;
using AlfasiWeb;
using Dinota.Domain.MenuOrderExtraItemDetail;
using System.Media;
using Dinota.Domain.PaymentInfo;

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
        private readonly MenuOrderExtraItemDetailService _menuOrderExtraItemDetailService;
        private readonly MenuOrderOfficerService _menuOrderOfficerService;
        private readonly MenuPackageService _menuPackageService;
        private readonly MenuItemDetailService _menuItemDetailService;
        private readonly F140HeaderService _f140HeaderService;
        private readonly F140DataService _f140DataService;
        private readonly PaymentInfoService _paymentInfoService;

        public OrderController(IDomainContext dataContext, PaymentInfoService paymentInfoService, MenuOrderExtraItemDetailService menuOrderExtraItemDetailService, F140DataService f140DataService, F140HeaderService f140HeaderService, MenuItemDetailService menuItemDetailService, MenuPackageService menuPackageService, MenuOrderOfficerService menuOrderOfficerService, MenuOrderItemDetailService menuOrderItemDetailService, MenuOrderHeaderService menuOrderHeaderService, MenuItemService menuItemService, BOCTransactionService bOCTransactionService, IngredientBOCService ingredientBOCService, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
            _menuPackageService = menuPackageService;
            _menuItemDetailService = menuItemDetailService;
            _f140HeaderService = f140HeaderService;
            _f140DataService = f140DataService;
            _menuOrderExtraItemDetailService = menuOrderExtraItemDetailService;
            _paymentInfoService = paymentInfoService;
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

        
        [HttpPost]
        public ActionResult OrderList(FormCollection Form)
        {
            OrdeerListViewModel model = new OrdeerListViewModel();
            List<MenuOrderHeaderModel> MenuOrderHeaderModelList = new List<MenuOrderHeaderModel>();
            try
            {
                UserAccount account = GetCurrentUser();
                TryUpdateModel(model);
                DateTime FromDate = model.EffectiveDate.Date;
                DateTime ToDate = model.EffectiveDate.Date.AddDays(1).AddTicks(-1);
                var filter = _menuOrderHeaderService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p=>p.OrderDate>= FromDate).And(p=>p.OrderDate<= ToDate).And(p=>p.LocationUId== account.LocationUId);
                List<MenuOrderHeader> MenuOrderHeaderList = _menuOrderHeaderService.GetCollection(filter, p => p.CreationDate).OrderByDescending(p=>p.OrderDate).Take(100).ToList();
                foreach (MenuOrderHeader order in MenuOrderHeaderList.OrderBy(p => p.OrderDate))
                {
                    MenuOrderHeaderModel mod = new MenuOrderHeaderModel();
                    mod.MenuOrderHeader = order;

                    if (order.Status > (int)DataStruct.MenuOrderItemStatus.Accepted)
                    {
                        var filter140 = _f140HeaderService.GetDefaultSpecification();
                        filter140 = filter140.And(p => p.Active == true).And(p => p.MenuOrderId == order.UId);
                        List<F140Header> oF140HeaderList = _f140HeaderService.GetCollection(filter140, p => p.CreationDate).OrderByDescending(p => p.UId).ToList();
                        if (oF140HeaderList.Count > 0)
                        {
                            mod.F140Header = oF140HeaderList[0];
                            var filterD = _f140DataService.GetDefaultSpecification();
                            int id = oF140HeaderList[0].UId;
                            filterD = filterD.And(p => p.Active == true).And(p => p.F140HeaderUId == id);
                            List<F140Data> F140DataList = _f140DataService.GetCollection(filterD, p => p.CreationDate).ToList();
                            mod.TotalAmount = F140DataList.Sum(p => p.Amount);
                        }
                        var filterOD = _menuOrderItemDetailService.GetDefaultSpecification();
                        filterOD = filterOD.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == order.UId);
                        List<MenuOrderItemDetail> MenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterOD, p => p.CreationDate).ToList();
                        mod.MenuOrderItemDetailList = MenuOrderItemDetailList;
                        var filterO = _menuOrderOfficerService.GetDefaultSpecification();
                        filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == order.UId);
                        mod.MenuOrderOfficerList = _menuOrderOfficerService.GetCollection(filterO, p => p.CreationDate).ToList();

                    }
                    MenuOrderHeaderModelList.Add(mod);
                }
                model.MenuOrderHeaderModelList = MenuOrderHeaderModelList;

            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public ActionResult FinalizePayment(int id)
        {
            MenuOrderHeaderModel mod = new MenuOrderHeaderModel();
            try
            {

                MenuOrderHeader oMenuOrderHeader = _menuOrderHeaderService.GetByKey(id);
                mod.MenuOrderHeader = oMenuOrderHeader;
                var filter = _paymentInfoService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuOrderHeaderId == id);
                PaymentInfo info = _paymentInfoService.GetBy(filter);
                if (info != null)
                    mod.PaymentInfo = info;
                else
                    mod.PaymentInfo = new PaymentInfo();
            }
            catch (Exception)
            {

                throw;
            }
            return View(mod);
        }
        [HttpPost]
        public ActionResult FinalizePayment(FormCollection Form,int id)
        {
            MenuOrderHeaderModel mod = new MenuOrderHeaderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                MenuOrderHeader oMenuOrderHeader = _menuOrderHeaderService.GetByKey(id);
                mod.MenuOrderHeader = oMenuOrderHeader;
                var filter = _paymentInfoService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuOrderHeaderId == id);
                PaymentInfo info = _paymentInfoService.GetBy(filter);
                PaymentInfo payment = new PaymentInfo();
                if (info == null)
                {
                    payment = info;
                }
                TryUpdateModel(mod);
               
                payment = mod.PaymentInfo;
                payment.Active = true;
                payment.PaymentMethodId = int.Parse(Form["PaymentMethodId"].ToString());
                payment.SLAFLocationId = account.LocationUId;
                payment.MenuOrderHeaderId = id;
                if (info == null)
                {
                    _paymentInfoService.Add(payment);
                    DataContext.SaveChanges();
                }
                else
                {
                    DataContext.SaveChanges();
                }
                MenuOrderHeader MenuOrderHeader = _menuOrderHeaderService.GetByKey(id);
                MenuOrderHeader.PaymentMethod =payment.PaymentMethodId;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Payment Information successfully Saved");
                return RedirectToAction("OrderList","Order", new { id = id });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult OrderList()
        {
            OrdeerListViewModel model = new OrdeerListViewModel();
            List<MenuOrderHeaderModel> MenuOrderHeaderModelList = new List<MenuOrderHeaderModel>();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _menuOrderHeaderService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p=>p.LocationUId== account.LocationUId);
                List<MenuOrderHeader> MenuOrderHeaderList = _menuOrderHeaderService.GetCollection(filter, p => p.CreationDate).OrderByDescending(p=>p.OrderDate).Take(80).ToList();
                foreach (MenuOrderHeader order in MenuOrderHeaderList.OrderBy(p => p.OrderDate))
                {

                    MenuOrderHeaderModel mod = new MenuOrderHeaderModel();
                    var filtero = _menuOrderOfficerService.GetDefaultSpecification();
                    filtero = filtero.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == order.UId);
                    List<MenuOrderOfficer> MenuOrderOfficerList = _menuOrderOfficerService.GetCollection(filtero, p => p.CreationDate).ToList();
                    mod.MenuOrderOfficerList = MenuOrderOfficerList;
                    mod.MenuOrderHeader = order;
                    if (order.Status > (int)DataStruct.MenuOrderItemStatus.Accepted)
                    {
                        
                        var filter140 = _f140HeaderService.GetDefaultSpecification();
                        filter140 = filter140.And(p => p.Active == true).And(p => p.MenuOrderId == order.UId);
                        List<F140Header> oF140HeaderList = _f140HeaderService.GetCollection(filter140, p => p.CreationDate).OrderByDescending(p => p.UId).ToList();
                        if (oF140HeaderList.Count > 0)
                        {
                            mod.F140Header = oF140HeaderList[0];
                            var filterD = _f140DataService.GetDefaultSpecification();
                            int id = oF140HeaderList[0].UId;
                            filterD = filterD.And(p => p.Active == true).And(p => p.F140HeaderUId == id);
                            List<F140Data> F140DataList = _f140DataService.GetCollection(filterD, p => p.CreationDate).ToList();
                            mod.TotalAmount = F140DataList.Sum(p => p.Amount);
                        }
                        var filterOD = _menuOrderItemDetailService.GetDefaultSpecification();
                        filterOD = filterOD.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == order.UId);
                        List<MenuOrderItemDetail> MenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterOD, p => p.CreationDate).ToList();
                        mod.MenuOrderItemDetailList = MenuOrderItemDetailList;
                    }
                    MenuOrderHeaderModelList.Add(mod);
                }
                model.MenuOrderHeaderModelList = MenuOrderHeaderModelList;

            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
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
                UserAccount account = GetCurrentUser();
                TryUpdateModel(header);
                header.OrderDate = header.EffectiveDate;
                header.Active = true;
                header.Status = 10;
                header.PaymentMethod = (int)DataStruct.PaymentMethod.Credit;
                header.OfficerCount = 0;
                header.LocationUId = account.LocationUId;
                _menuOrderHeaderService.Add(header);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order created. Add menu items");
                return RedirectToAction("MenuCreate", new { id = header.UId });
            }
            catch (Exception)
            {

                throw;
            }
            return View(header);
        }
       [HttpPost]
        public ActionResult UpdateDate(FormCollection Form,int id)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                TryUpdateModel(model);
               MenuOrderHeader MenuOrderHeader = _menuOrderHeaderService.GetByKey(id);
                MenuOrderHeader.OrderDate = model.EffectiveDate;
                MenuOrderHeader.EffectiveDate = model.EffectiveDate;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Date successfully have been updated");
                return RedirectToAction("MenuCreate", "Order", new {id=id });
            }
            catch (Exception)
            {

                throw;
            }
            return View();    
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
                model.MenuOrderHeader = _menuOrderHeaderService.GetByKey(id);
                model.EffectiveDate = model.MenuOrderHeader.OrderDate;
                var filterO = _menuOrderItemDetailService.GetDefaultSpecification();
                filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                model.MenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterO, p => p.CreationDate).ToList();

                var filterOI = _menuOrderExtraItemDetailService.GetDefaultSpecification();
                filterOI = filterOI.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                model.MenuOrderOtherItemDetailList = _menuOrderExtraItemDetailService.GetCollection(filterOI, p => p.CreationDate).ToList();

                var filterI = _ingredientInfoService.GetDefaultSpecification();
                filterI = filterI.And(p => p.Active == true);
                List<IngredientInfo> IngredientInfoList = _ingredientInfoService.GetCollection(filterI, p => p.CreationDate).ToList();
                model.IngredientInfoList = IngredientInfoList;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult MenuOrder(int MenuItemId, int OrderId, int OrderMenuType)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                if (OrderMenuType == 1)
                {
                    MenuItem item = _menuItemService.GetByKey(MenuItemId);
                    model.MenuItem = item;
                    model.IsMenuItem = true;
                }
                else if (OrderMenuType == 2)
                {
                    IngredientInfo oIngredientInfo = _ingredientInfoService.GetByKey(MenuItemId);
                    model.IngredientInfo = oIngredientInfo;
                    model.IsMenuItem = false;
                }
                model.MenuOrderId = OrderId;

            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult MenuOrder(FormCollection Form, int id)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
               
                TryUpdateModel(model);
                int MenuId = 0;
                if (model.IsMenuItem)
                {
                    MenuId = int.Parse(Form["MenuItem.UId"].ToString());
                }
                else
                {
                    MenuId = int.Parse(Form["IngredientInfo.UId"].ToString());
                }

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


                if (model.IsMenuItem)
                {
                    MenuOrderItemDetail detail = new MenuOrderItemDetail();
                    detail.MenuItemUId = MenuId;
                    MenuItem item = _menuItemService.GetByKey(MenuId);
                    detail.Qty = Qty;
                    detail.MeanuOrderHeaderUId = id;
                    detail.Remark = remark;
                    detail.Active = true;

                    detail.Status = (int)DataStruct.MenuOrderItemStatus.Pending;
                    _menuOrderItemDetailService.Add(detail);
                }
                else
                {
                    MenuOrderExtraItemDetail detail = new MenuOrderExtraItemDetail();
                    IngredientInfo info = _ingredientInfoService.GetByKey(MenuId);
                    detail.OtherItemUId = MenuId;
                    detail.Qty = Qty;
                    detail.MeanuOrderHeaderUId = id;
                    detail.Remark = remark;
                    detail.Active = true;
                    detail.Status = (int)DataStruct.MenuOrderItemStatus.Pending;
                    _menuOrderExtraItemDetailService.Add(detail);
                }

                DataContext.SaveChanges();

                
                TempData[ViewDataKeys.Message] = new SuccessMessage("Item added to the order. Add more menu items");
                return RedirectToAction("MenuCreate", new { id });
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }

        public ActionResult ProcessOrder(int id)
        {
            MenuBOCModel model = new MenuBOCModel();
            try
            {
                List<MenuItemDetail> MasterItemList = new List<MenuItemDetail>();
                List<MenuItemDetailModel> MenuItemIngridientList = new List<MenuItemDetailModel>();
                MenuOrderHeader order = _menuOrderHeaderService.GetByKey(id);
                var filterMD = _menuOrderItemDetailService.GetDefaultSpecification();
                filterMD = filterMD.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                List<MenuOrderItemDetail> MenuOrderMenuList = _menuOrderItemDetailService.GetCollection(filterMD, p => p.CreationDate).ToList();
                int i = 0;
                foreach (MenuOrderItemDetail item in MenuOrderMenuList)
                {
                    i++;
                    if (item.MenuItem.IsCombine)
                    {
                        var filterM = _menuPackageService.GetDefaultSpecification();
                        filterM = filterM.And(p => p.Active == true).And(p => p.MenuItemId == item.MenuItem.UId);
                        List<MenuPackage> MenuPackageList = _menuPackageService.GetCollection(filterM, p => p.CreationDate).ToList();
                        foreach (MenuPackage pack in MenuPackageList)
                        {
                            MenuItemDetailModel detail = new MenuItemDetailModel();
                            detail.MenuItemId = pack.MenuItemId;
                            detail.MenuItem = pack.MenuItem;
                            detail.Detail = item;
                            detail.MenuItemType = 1;
                            var filter = _menuItemDetailService.GetDefaultSpecification();
                            filter = filter.And(p => p.Active == true).And(p => p.MenuItemId == pack.MenuItem.UId);
                            List<MenuItemDetail> MenuItemDetailSubList = _menuItemDetailService.GetCollection(filter, p => p.CreationDate).ToList();
                            List<MenuItemDetailListModel> MenuItemDetailListModelList = new List<MenuItemDetailListModel>();
                            foreach (MenuItemDetail det in MenuItemDetailSubList)
                            {
                                MenuItemDetailListModel itemdetail = new MenuItemDetailListModel();
                                var filterBOC = _ingredientBOCService.GetDefaultSpecification();
                                filterBOC = filterBOC.And(p => p.Active == true).And(p => p.IngredientUId == det.IngriedientUId);
                                decimal TotalStock = _ingredientBOCService.GetCollection(filterBOC, p => p.CreationDate).Sum(p => p.Qty);
                                itemdetail.CurrentStockQty = TotalStock;
                                itemdetail.MenuItemDetail = det;
                                MenuItemDetailListModelList.Add(itemdetail);
                            }
                            detail.MenuItemDetailListModelList = MenuItemDetailListModelList;
                            detail.MenuItemDetailList = MenuItemDetailSubList;
                            MenuItemIngridientList.Add(detail);
                        }
                    }
                    else
                    {
                        MenuItemDetailModel detail = new MenuItemDetailModel();
                        detail.MenuItemId = item.MenuItem.UId;
                        detail.MenuItem = item.MenuItem;
                        detail.Detail = item;
                        detail.MenuItemType = 1;
                        var filter = _menuItemDetailService.GetDefaultSpecification();
                        filter = filter.And(p => p.Active == true).And(p => p.MenuItemId == item.MenuItem.UId);
                        MasterItemList = _menuItemDetailService.GetCollection(filter, p => p.CreationDate).ToList();
                        List<MenuItemDetailListModel> MenuItemDetailListModelList = new List<MenuItemDetailListModel>();
                        foreach (MenuItemDetail det in MasterItemList)
                        {
                            if (item.Qty == 0)
                                item.Qty = 1;
                            if (i == 1)
                            {
                                if (det.MenuItem.PortionQty > 0)
                                {
                                    model.MultipleQty = item.Qty / det.MenuItem.PortionQty;
                                }
                                
                            }
                            MenuItemDetailListModel itemdetail = new MenuItemDetailListModel();
                            var filterBOC = _ingredientBOCService.GetDefaultSpecification();
                            filterBOC = filterBOC.And(p => p.Active == true).And(p => p.IngredientUId == det.IngriedientUId);
                            decimal TotalStock = _ingredientBOCService.GetCollection(filterBOC, p => p.CreationDate).Sum(p => p.Qty);
                            itemdetail.CurrentStockQty = TotalStock;
                            itemdetail.MenuItemDetail = det;
                            MenuItemDetailListModelList.Add(itemdetail);
                        }
                        detail.MenuItemDetailListModelList = MenuItemDetailListModelList;
                        detail.MenuItemDetailList = MasterItemList;
                        MenuItemIngridientList.Add(detail);
                    }
                }
                
                model.MenuOrderId = id;
                model.MenuItemList = MenuItemIngridientList;
            }
             catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ProcessOrder(FormCollection Form, int id)
        {
            MenuBOCModel model = new MenuBOCModel();
            try
            {
                decimal F140TotalAmount = 0;
                F140Header header = new F140Header();
                header.MenuOrderId = id;
                header.EffectiveDate = DateTime.Now;

                header.Active = true;
                _f140HeaderService.Add(header);
                DataContext.SaveChanges();
                List<MenuItemDetail> MasterItemList = new List<MenuItemDetail>();
                List<MenuItemDetailModel> MenuItemIngridientList = new List<MenuItemDetailModel>();
                var filterMD = _menuOrderItemDetailService.GetDefaultSpecification();
                filterMD = filterMD.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                List<MenuOrderItemDetail> MenuOrderMenuList = _menuOrderItemDetailService.GetCollection(filterMD, p => p.CreationDate).ToList();
                foreach (MenuOrderItemDetail item in MenuOrderMenuList)
                {

                    model.MenuOrderId = id;
                    if (item.MenuItem.IsCombine)
                    {
                        var filterM = _menuPackageService.GetDefaultSpecification();
                        filterM = filterM.And(p => p.Active == true).And(p => p.MenuItemId == item.MenuItem.UId);
                        List<MenuPackage> MenuPackageList = _menuPackageService.GetCollection(filterM, p => p.CreationDate).ToList();
                        foreach (MenuPackage pack in MenuPackageList)
                        {
                            MenuItemDetailModel detail = new MenuItemDetailModel();
                            detail.MenuItemId = pack.MenuItemId;
                            detail.MenuItem = pack.MenuItem;
                            var filter = _menuItemDetailService.GetDefaultSpecification();
                            filter = filter.And(p => p.Active == true).And(p => p.MenuItemId == pack.MenuItem.UId);
                            List<MenuItemDetail> MenuItemDetailSubList = _menuItemDetailService.GetCollection(filter, p => p.CreationDate).ToList();
                            foreach (MenuItemDetail it in MenuItemDetailSubList)
                            {
                                F140Data data = new F140Data();
                                data.Active = true;
                                data.F140HeaderUId = header.UId;
                                data.MenuItemId = pack.MenuItem.UId;
                                data.Amount = 0;
                                data.IngridientUId = it.IngriedientUId;
                                decimal AssignedQty = 0;
                                if (Form["menuingqty_" + it.IngriedientUId + "" + it.UId] != null)
                                {
                                    string Amt = Form["menuingqty_" + it.IngriedientUId + "" + it.UId].ToString();
                                    AssignedQty = decimal.Parse(Form["menuingqty_" + it.IngriedientUId + "" + it.UId].ToString());
                                    data.Qty = AssignedQty;
                                }
                                else
                                {
                                    data.Qty = it.IngriedientQty;
                                }
                                var filterBOC = _ingredientBOCService.GetDefaultSpecification();
                                filterBOC = filterBOC.And(p => p.Active == true).And(p => p.IngredientUId == it.IngriedientUId).And(p => p.Qty > 0);
                                List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filterBOC, p => p.EffectiveDate).ToList();
                                decimal TotAmount = 0;
                                foreach (IngredientBOC FEntry in IngredientBOCList.OrderBy(p => p.EffectiveDate))
                                {
                                    if (AssignedQty > 0)
                                    {
                                        if (FEntry.Qty > AssignedQty)
                                        {
                                            BOCTransaction tran = new BOCTransaction();
                                            tran.IngriedientBOCUId = FEntry.UId;
                                            tran.MenuOrderUId = id;
                                            tran.PresentStock = FEntry.Qty;
                                            tran.IssueStock = AssignedQty;
                                            decimal RemStock = FEntry.Qty - AssignedQty;
                                            tran.RemainingStock = RemStock;
                                            tran.Active = true;
                                            tran.EffectiveDate = DateTime.Now;
                                            _bOCTransactionService.Add(tran);
                                            DataContext.SaveChanges();

                                            IngredientBOC UpdateBOC = _ingredientBOCService.GetByKey(FEntry.UId);
                                            UpdateBOC.Qty = tran.RemainingStock;
                                            DataContext.SaveChanges();

                                            TotAmount += AssignedQty * FEntry.Price;
                                            AssignedQty = 0;
                                        }
                                        else
                                        {
                                            BOCTransaction tran = new BOCTransaction();
                                            tran.IngriedientBOCUId = FEntry.UId;
                                            tran.MenuOrderUId = id;
                                            tran.PresentStock = FEntry.Qty;
                                            AssignedQty = AssignedQty - FEntry.Qty;
                                            tran.IssueStock = AssignedQty;

                                            tran.RemainingStock = 0;
                                            tran.Active = true;
                                            tran.EffectiveDate = DateTime.Now;
                                            _bOCTransactionService.Add(tran);
                                            DataContext.SaveChanges();

                                            IngredientBOC UpdateBOC = _ingredientBOCService.GetByKey(FEntry.UId);
                                            UpdateBOC.Qty = tran.RemainingStock;
                                            DataContext.SaveChanges();
                                            TotAmount += FEntry.Qty * FEntry.Price;
                                        }
                                    }
                                }
                                data.Amount = TotAmount;
                                F140TotalAmount += TotAmount;
                                data.SLAFLocationId = 18;
                                data.MeasurementUnitId = it.IngriedientMeasurementUId;
                                _f140DataService.Add(data);
                                DataContext.SaveChanges();
                            }
                        }



                    }
                    else
                    {
                        MenuItemDetailModel detail = new MenuItemDetailModel();
                        detail.MenuItemId = item.MenuItem.UId;
                        detail.MenuItem = item.MenuItem;
                        var filter = _menuItemDetailService.GetDefaultSpecification();
                        filter = filter.And(p => p.Active == true).And(p => p.MenuItemId == item.MenuItem.UId);
                        MasterItemList = _menuItemDetailService.GetCollection(filter, p => p.CreationDate).ToList();
                        foreach (MenuItemDetail it in MasterItemList)
                        {
                            F140Data data = new F140Data();
                            data.Active = true;
                            data.F140HeaderUId = header.UId;
                            data.MenuItemId = item.MenuItem.UId;
                            data.Amount = 0;
                            data.IngridientUId = it.IngriedientUId;
                            decimal AssignedQty = 0;
                            if (Form["menuingqty_" + it.IngriedientUId + "" + it.UId] != null)
                            {
                                string Amt = Form["menuingqty_" + it.IngriedientUId + "" + it.UId].ToString();
                                AssignedQty = decimal.Parse(Amt);
                                data.Qty = AssignedQty;
                            }
                            else
                            {
                                data.Qty = it.IngriedientQty;
                            }
                            var filterBOC = _ingredientBOCService.GetDefaultSpecification();
                            filterBOC = filterBOC.And(p => p.Active == true).And(p => p.IngredientUId == it.IngriedientUId).And(p => p.Qty > 0);
                            List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filterBOC, p => p.EffectiveDate).ToList();
                            decimal TotAmount = 0;
                            foreach (IngredientBOC FEntry in IngredientBOCList.OrderBy(p => p.EffectiveDate))
                            {
                                if (AssignedQty > 0)
                                {
                                    if (FEntry.Qty > AssignedQty)
                                    {
                                        BOCTransaction tran = new BOCTransaction();
                                        tran.IngriedientBOCUId = FEntry.UId;
                                        tran.MenuOrderUId = id;
                                        tran.PresentStock = FEntry.Qty;
                                        tran.IssueStock = AssignedQty;
                                        decimal RemStock = FEntry.Qty - AssignedQty;
                                        tran.RemainingStock = RemStock;
                                        tran.Active = true;
                                        tran.EffectiveDate = DateTime.Now;
                                        _bOCTransactionService.Add(tran);
                                        DataContext.SaveChanges();

                                        IngredientBOC UpdateBOC = _ingredientBOCService.GetByKey(FEntry.UId);
                                        UpdateBOC.Qty = tran.RemainingStock;
                                        DataContext.SaveChanges();

                                        TotAmount += AssignedQty * FEntry.Price;
                                        AssignedQty = 0;
                                    }
                                    else
                                    {
                                        BOCTransaction tran = new BOCTransaction();
                                        tran.IngriedientBOCUId = FEntry.UId;
                                        tran.MenuOrderUId = id;
                                        tran.PresentStock = FEntry.Qty;
                                        AssignedQty = AssignedQty - FEntry.Qty;
                                        tran.IssueStock = AssignedQty;

                                        tran.RemainingStock = 0;
                                        tran.Active = true;
                                        tran.EffectiveDate = DateTime.Now;
                                        _bOCTransactionService.Add(tran);
                                        DataContext.SaveChanges();

                                        IngredientBOC UpdateBOC = _ingredientBOCService.GetByKey(FEntry.UId);
                                        UpdateBOC.Qty = tran.RemainingStock;
                                        DataContext.SaveChanges();
                                        TotAmount += FEntry.Qty * FEntry.Price;
                                    }
                                }

                            }
                            data.Amount = TotAmount;
                            F140TotalAmount += TotAmount;
                            data.SLAFLocationId = 18;
                            data.MeasurementUnitId = it.IngriedientMeasurementUId;
                            _f140DataService.Add(data);
                            DataContext.SaveChanges();
                        }


                    }
                }
                MenuOrderHeader orderheader = _menuOrderHeaderService.GetByKey(id);
                orderheader.Status = (int)DataStruct.MenuOrderItemStatus.Delivered;
                orderheader.F140TotalAmt = F140TotalAmount;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order successfully created.");

                return RedirectToAction("OrderList");

                model.MenuItemList = MenuItemIngridientList;
            }
            catch (Exception)
            {

                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RemoveIngridient(int ItemDetailId, int MenuOrderId)
        {
            try
            {
                MenuItemDetail detail = _menuItemDetailService.GetByKey(ItemDetailId);
                detail.Active = false;
                DataContext.SaveChanges();

                return RedirectToAction("ProcessOrder", new { id = MenuOrderId });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult AddOfficer(int officerId, int orderId)
        {
            try
            {
                UserAccount account = _userAccountService.GetByKey(officerId);
                MenuOrderOfficer off = new MenuOrderOfficer();
                off.UserId = officerId;
                off.Active = true;
                off.MeanuOrderHeaderUId = orderId;
                _menuOrderOfficerService.Add(off);
                DataContext.SaveChanges();

                MenuOrderHeader header = _menuOrderHeaderService.GetByKey(orderId);
                int OfficerCount = header.OfficerCount;
                OfficerCount = OfficerCount + 1;
                header.OfficerCount = OfficerCount;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer is successfully added.");

                return RedirectToAction("OfficerList", new { id = orderId });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult AddOfficerToOrder(int officerId, int orderId)
        {
            try
            {
                UserAccount account = _userAccountService.GetByKey(officerId);
                MenuOrderOfficer off = new MenuOrderOfficer();
                off.UserId = officerId;
                off.Active = true;
                off.MeanuOrderHeaderUId = orderId;
                _menuOrderOfficerService.Add(off);
                DataContext.SaveChanges();

                MenuOrderHeader header = _menuOrderHeaderService.GetByKey(orderId);
                int OfficerCount = header.OfficerCount;
                OfficerCount = OfficerCount + 1;
                header.OfficerCount = OfficerCount;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer successfully added.");

                return RedirectToAction("AddOfficerList", new { id = orderId });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult RemoveOfficer(int officerId, int orderId)
        {
            try
            {
                MenuOrderOfficer officer = _menuOrderOfficerService.GetByKey(officerId);
                officer.Active = false;
                DataContext.SaveChanges();

                MenuOrderHeader header = _menuOrderHeaderService.GetByKey(orderId);
                int officerCount = header.OfficerCount;
                officerCount = officerCount -1;
                header.OfficerCount = officerCount;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer removed successfully.");

                return RedirectToAction("OfficerList", new { id = orderId });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult RemoveOfficerFromOrder(int officerId, int orderId)
        {
            try
            {
                MenuOrderOfficer officer = _menuOrderOfficerService.GetByKey(officerId);
                officer.Active = false;
                DataContext.SaveChanges();

                MenuOrderHeader header = _menuOrderHeaderService.GetByKey(orderId);
                int officerCount = header.OfficerCount;
                officerCount = officerCount - 1;
                header.OfficerCount = officerCount;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer removed successfully.");

                return RedirectToAction("AddOfficerList", new { id = orderId });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                MenuOrderHeader header = _menuOrderHeaderService.GetByKey(id);
                header.Active = false;
                DataContext.SaveChanges();

                var filterItems = _menuOrderItemDetailService.GetDefaultSpecification();
                filterItems = filterItems.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                List<MenuOrderItemDetail> OrderItemList = _menuOrderItemDetailService.GetCollection(filterItems, p => p.CreationDate).ToList();
                foreach (MenuOrderItemDetail item in OrderItemList)
                {
                    MenuOrderItemDetail detail = _menuOrderItemDetailService.GetByKey(item.UId);
                    detail.Active = false;
                    DataContext.SaveChanges();

                }

                var officerList = _menuOrderOfficerService.GetDefaultSpecification();
                officerList = officerList.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                List<MenuOrderOfficer> OfficerList = _menuOrderOfficerService.GetCollection(officerList, p => p.CreationDate).ToList();
                foreach (MenuOrderOfficer off in OfficerList)
                {
                    MenuOrderOfficer detail = _menuOrderOfficerService.GetByKey(off.UId);
                    detail.Active = false;
                    DataContext.SaveChanges();

                }
                List<BOCTransaction> BOCTransactionList = new List<BOCTransaction>();
                var filterBOC = _bOCTransactionService.GetDefaultSpecification();
                filterBOC = filterBOC.And(p => p.Active == true).And(p => p.MenuOrderUId == id);
                BOCTransactionList = _bOCTransactionService.GetCollection(filterBOC, p => p.CreationDate).ToList();
                foreach (BOCTransaction boc in BOCTransactionList)
                {
                    BOCTransaction newBOC = new BOCTransaction();
                    newBOC = _bOCTransactionService.GetByKey(boc.UId);
                    newBOC.Active = false;
                    DataContext.SaveChanges();
                    IngredientBOC itemBOC = _ingredientBOCService.GetByKey(newBOC.IngriedientBOCUId);
                    if (itemBOC != null)
                    {
                        itemBOC.Qty = itemBOC.Qty + boc.IssueStock;
                        DataContext.SaveChanges();
                    }

                }
                var filter140Header = _f140HeaderService.GetDefaultSpecification();
                filter140Header = filter140Header.And(p => p.Active == true).And(p => p.MenuOrderId == id);
                List<F140Header> F140HeaderList = _f140HeaderService.GetCollection(filter140Header, p => p.CreationDate).ToList();
                foreach (F140Header head in F140HeaderList)
                {
                    F140Header headerCheck = new F140Header();
                    headerCheck = _f140HeaderService.GetByKey(head.UId);
                    headerCheck.Active = false;
                    DataContext.SaveChanges();

                    var filter140Detail = _f140DataService.GetDefaultSpecification();
                    filter140Detail = filter140Detail.And(p => p.Active == true).And(p => p.F140HeaderUId == head.UId);
                    List<F140Data> filter140DetailList = _f140DataService.GetCollection(filter140Detail, p => p.CreationDate).ToList();

                    foreach (F140Data d in filter140DetailList)
                    {
                        F140Data dataCheck = new F140Data();
                        dataCheck = _f140DataService.GetByKey(d.UId);
                        dataCheck.Active = false;
                        DataContext.SaveChanges();
                    }
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Order successfully removed");

                return RedirectToAction("OrderList");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }


        public ActionResult AddOfficerLivingInStatus(int id)
        {
            UserAccount user = new UserAccount();
            user = GetCurrentUser();

            try
            {
                var filterU = _userAccountService.GetDefaultSpecification();
                filterU = filterU.And(p => p.Active == true).And(p => p.LivingStatus == 1).And(p => p.LocationUId == user.LocationUId).And(p => p.RankUId != 148);
                List<UserAccount> UserAccountList = _userAccountService.GetCollection(filterU, p => p.CreationDate).ToList();
                foreach (UserAccount account in UserAccountList)
                {
                    MenuOrderOfficer off = new MenuOrderOfficer();
                    off.UserId = account.Id;
                    off.Active = true;
                    off.MeanuOrderHeaderUId = id;
                    _menuOrderOfficerService.Add(off);
                    DataContext.SaveChanges();

                    MenuOrderHeader header = _menuOrderHeaderService.GetByKey(id);
                    int OfficerCount = header.OfficerCount;
                    OfficerCount = OfficerCount + 1;
                    header.OfficerCount = OfficerCount;
                    DataContext.SaveChanges();
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officers are successfully added.");

                return RedirectToAction("OfficerList", new { id = id });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public ActionResult AddAllOfficers(int id)
        {
            UserAccount user = new UserAccount();
            user = GetCurrentUser();
            try
            {
                var filterU = _userAccountService.GetDefaultSpecification();
                filterU = filterU.And(p => p.Active == true).And(p => p.LocationUId == user.LocationUId).And(p => p.RankUId != 148);
                List<UserAccount> UserAccountList = _userAccountService.GetCollection(filterU, p => p.CreationDate).ToList();
                int OfficerCount = 0;
                MenuOrderHeader header = _menuOrderHeaderService.GetByKey(id);
                OfficerCount = header.OfficerCount;
                foreach (UserAccount account in UserAccountList)
                {

                    var filterO = _menuOrderOfficerService.GetDefaultSpecification();
                    filterO = filterO.And(p => p.Active == true).And(p => p.UserId == account.Id).And(p => p.MeanuOrderHeaderUId == id);
                    MenuOrderOfficer oMenuOrderOfficer = _menuOrderOfficerService.GetBy(filterO);
                    if (oMenuOrderOfficer == null)
                    {
                        MenuOrderOfficer off = new MenuOrderOfficer();
                        off.UserId = account.Id;
                        off.Active = true;
                        off.MeanuOrderHeaderUId = id;
                        _menuOrderOfficerService.Add(off);
                        DataContext.SaveChanges();
                        
                        OfficerCount = OfficerCount + 1;
                    }
                    
                }
                MenuOrderHeader headerInfo = _menuOrderHeaderService.GetByKey(id);
                headerInfo.OfficerCount = OfficerCount;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officers are successfully added.");

                return RedirectToAction("OfficerList", new { id = id });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public ActionResult AddOfficerCadets(int id)
        {
            UserAccount user = new UserAccount();
            user = GetCurrentUser();

            try
            {
                var filterU = _userAccountService.GetDefaultSpecification();
                filterU = filterU.And(p => p.Active == true).And(p => p.RankUId == 148).And(p => p.LocationUId == user.LocationUId);
                List<UserAccount> UserAccountList = _userAccountService.GetCollection(filterU, p => p.CreationDate).ToList();
                foreach (UserAccount account in UserAccountList)
                {
                    MenuOrderOfficer off = new MenuOrderOfficer();
                    off.UserId = account.Id;
                    off.Active = true;
                    off.MeanuOrderHeaderUId = id;
                    _menuOrderOfficerService.Add(off);
                    DataContext.SaveChanges();

                    MenuOrderHeader header = _menuOrderHeaderService.GetByKey(id);
                    int OfficerCount = header.OfficerCount;
                    OfficerCount = OfficerCount + 1;
                    header.OfficerCount = OfficerCount;
                    DataContext.SaveChanges();
                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Officer Cadets are successfully added.");

                return RedirectToAction("OfficerList", new { id = id });
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public ActionResult AddOfficerList(int id)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                var filterO = _menuOrderOfficerService.GetDefaultSpecification();
                filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                model.MenuOrderOfficerList = _menuOrderOfficerService.GetCollection(filterO, p => p.CreationDate).ToList();

                var filterU = _userAccountService.GetDefaultSpecification();
                filterU = filterU.And(p => p.Active == true).And(p => p.LocationUId == account.LocationUId);
                model.UserAccountList = _userAccountService.GetCollection(filterU, p => p.CreationDate).ToList();
                model.MenuOrderId = id;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public ActionResult OfficerList(int id)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                var filterO = _menuOrderOfficerService.GetDefaultSpecification();
                filterO = filterO.And(p => p.Active == true).And(p => p.MeanuOrderHeaderUId == id);
                model.MenuOrderOfficerList = _menuOrderOfficerService.GetCollection(filterO, p => p.CreationDate).ToList();

                var filterU = _userAccountService.GetDefaultSpecification();
                filterU = filterU.And(p => p.Active == true).And(p => p.LocationUId == account.LocationUId);
                model.UserAccountList = _userAccountService.GetCollection(filterU, p => p.CreationDate).ToList();
                model.MenuOrderId = id;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult AddIngridient(int MenuItemId, int OrderId)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                var filterI = _ingredientInfoService.GetDefaultSpecification();
                filterI = filterI.And(p => p.Active == true).And(p => p.LocationUId == account.LocationUId);
                model.IngredientInfoList = _ingredientInfoService.GetCollection(filterI, p => p.CreationDate).ToList();
                model.MenuOrderId = OrderId;
                model.MenuItemId = MenuItemId;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult AddToMenuItem(int MenuItemId, int OrderId, int IngredientId)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                model.IngredientInfo = _ingredientInfoService.GetByKey(IngredientId);
                model.MenuOrderId = OrderId;
                model.MenuItemId = MenuItemId;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddToMenuItem(FormCollection Form)
        {
            OfficerMenuOrderModel model = new OfficerMenuOrderModel();
            try
            {
                UserAccount account = GetCurrentUser();
                int IngridientId = int.Parse(Form["IngredientInfo.UId"].ToString());
                int MenuOrderId = int.Parse(Form["MenuOrderId"].ToString());
                int MenuItemId = int.Parse(Form["MenuItemId"].ToString());
                decimal menucount = decimal.Parse(Form["menucount"].ToString());
                IngredientInfo oIngredientInfo = _ingredientInfoService.GetByKey(IngridientId);
                MenuItem oMenuItem = _menuItemService.GetByKey(MenuItemId);
                var filter = _menuItemDetailService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuItemId == MenuItemId);
                List<MenuItemDetail> MenuItemDetailList = _menuItemDetailService.GetCollection(filter, p => p.CreationDate).ToList();
                MenuItemDetail item = new MenuItemDetail();
                item.SLAFLocationUId = account.LocationUId;
                item.PortionQty = 1;
                item.PortionMeasurementUId = oMenuItem.MeasurementUnitId;
                item.MenuItemId = MenuItemId;
                item.IngriedientUId = oIngredientInfo.UId;
                item.IngriedientMeasurementUId = oIngredientInfo.MeasurementUnitUId;
                item.Active = true;
                item.IngriedientQty = menucount;
                _menuItemDetailService.Add(item);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Ingriedient is successfully added.");

                return RedirectToAction("ProcessOrder", new { id = MenuOrderId });

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
        public ActionResult SearchItem()
        {
            MenuSearchModel model = new MenuSearchModel();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _menuItemService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.MenuCategoryUId!=(int)DataStruct.MenuCategory.Rma_Curry).And(p=>p.SLAFLocationUId== account.LocationUId);
                List<MenuItem> MenuItemList = _menuItemService.GetCollection(filter, p => p.CreationDate).ToList();
                model.MenuItemList = MenuItemList;

            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
    }
}
