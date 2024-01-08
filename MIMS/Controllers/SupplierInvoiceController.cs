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
using Dinota.Domain.SupplierInvoice;
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.IngredientBOC;
using AlfasiWeb;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class SupplierInvoiceController : BaseController
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
        private readonly SupplierInvoiceService _supplierInvoiceService;
        private readonly IngredientInfoService _ingredientInfoService;
        private readonly IngredientBOCService _ingredientBOCService;

        public SupplierInvoiceController(IDomainContext dataContext, IngredientBOCService ingredientBOCService, IngredientInfoService ingredientInfoService, SupplierInvoiceService supplierInvoiceService, SupplierService supplierService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
            _supplierInvoiceService = supplierInvoiceService;
            _ingredientInfoService = ingredientInfoService;
            _ingredientBOCService = ingredientBOCService;
        }
        // [AuthorizeUserAccessLevel()]

        public ActionResult DeleteItem(int id)
        {
            IngredientBOC oIngredientBOC = new IngredientBOC();
            try
            {
                oIngredientBOC=_ingredientBOCService.GetByKey(id);
                oIngredientBOC.Active = false;
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Removed");
                return RedirectToAction("RegisterItemInvoice", new { id = oIngredientBOC.SupplierInvoiceId });
            }
            catch (Exception)
            {
                
                throw;
            }
            return View();
        }

        public void BindSupplierList(int LocationUId)
        {
            try
            {
                var filter = _supplierService.GetDefaultSpecification().And(s => s.Active == true).And(p=>p.LocationUId==LocationUId);
                var TypeList = _supplierService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Name");
                ViewData[ViewDataKeys.Suppliers] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public void BindItemList(int LocationUId)
        {
            try
            {
                List<CustomList> Custom = new List<CustomList>();
                var filter = _ingredientInfoService.GetDefaultSpecification().And(s => s.Active == true).And(p => p.LocationUId == LocationUId);
                List<IngredientInfo> IngredientInfoList = _ingredientInfoService.GetCollection(p=>p.CreationDate).ToList();
                foreach (IngredientInfo info in IngredientInfoList)
                {
                    CustomList cus = new CustomList();
                    cus.UId = info.UId;
                    cus.Name = info.ItemName + " - " + info.MeasurementUnit.Unit;
                    Custom.Add(cus);
                }
                
                SelectList list = new SelectList(Custom, "UId", "Name");
                ViewData[ViewDataKeys.IngredientInfoList] = list;
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
                //BindRoomNoList();
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

        public ActionResult CreateInvoice()
        {
            SupplierInvoice oSupplierInvoice = new SupplierInvoice();
            UserAccount account = GetCurrentUser();
            BindSupplierList(account.LocationUId);
            return View(oSupplierInvoice);
        }
        [HttpPost]
        public ActionResult CreateInvoice(FormCollection Form)
        {
            SupplierInvoice oSupplierInvoice = new SupplierInvoice();
            TryUpdateModel(oSupplierInvoice);
            UserAccount account = GetCurrentUser();
            BindSupplierList(account.LocationUId);
            try
            {
                oSupplierInvoice.SubTotal = 0;
                oSupplierInvoice.GrandTotal = 0;
                oSupplierInvoice.Discount = 0;
                oSupplierInvoice.LocationUId = account.LocationUId;
                oSupplierInvoice.Tax = 0;
                oSupplierInvoice.Active = true;
                oSupplierInvoice.Status = 10;
                _supplierInvoiceService.Add(oSupplierInvoice);
                DataContext.SaveChanges();


                return RedirectToAction("RegisterItemInvoice", new { id= oSupplierInvoice.UId});
            }
            catch (Exception)
            {

                throw;
            }
            return View(oSupplierInvoice);
        }
        [HttpPost]
        public ActionResult RegisterItemInvoice(FormCollection Form,string btnAdd,int id)
        {
            SupplierInvoiceModel model = new SupplierInvoiceModel();
            try
            {
                UserAccount account = GetCurrentUser();
                SupplierInvoice oSupplierInvoice = _supplierInvoiceService.GetByKey(id);
                TryUpdateModel(model);
                if (btnAdd == "ADDITEM")
                {
                    IngredientBOC oIngredientBOC = new IngredientBOC();
                    oIngredientBOC.IngredientUId = int.Parse(Form["ItemId"].ToString());
                    oIngredientBOC.Qty = decimal.Parse(Form["Qty"].ToString());
                    oIngredientBOC.Price = decimal.Parse(Form["UnitPrice"].ToString());
                    oIngredientBOC.SupplierInvoiceId = id;
                    oIngredientBOC.EffectiveDate = oSupplierInvoice.EffectiveDate;
                    IngredientInfo oIngredientInfo = _ingredientInfoService.GetByKey(oIngredientBOC.IngredientUId);
                    oIngredientBOC.UnitId = oIngredientInfo.MeasurementUnitUId;
                    oIngredientBOC.Active = true;
                    
                    oIngredientBOC.TotalPrice = oIngredientBOC.Qty * oIngredientBOC.Price;
                    oIngredientBOC.TransactionType = (int)DataStruct.BOCTransactionType.BOC;
                    oIngredientBOC.SLAFLocationUId = account.LocationUId;
                    _ingredientBOCService.Add(oIngredientBOC);
                    DataContext.SaveChanges();

                    return RedirectToAction("RegisterItemInvoice", new { id = oSupplierInvoice.UId });
                }
                else if (btnAdd == "COMPLETE")
                {
                    var filter = _ingredientBOCService.GetDefaultSpecification();
                    filter = filter.And(p => p.Active == true).And(p => p.SupplierInvoiceId == oSupplierInvoice.UId);
                    List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filter, p => p.CreationDate).ToList();
                    
                    decimal Tax = decimal.Parse(Form["SupplierInvoice.Tax"].ToString());
                    decimal Discount = decimal.Parse(Form["SupplierInvoice.Discount"].ToString());
                    decimal SubTotal = decimal.Parse(Form["SupplierInvoice.SubTotal"].ToString());
                    decimal GrandTotal = (SubTotal + Tax) - Discount;
                    SupplierInvoice oSupplierInvoiceuUpdate = _supplierInvoiceService.GetByKey(id);
                    oSupplierInvoiceuUpdate.Discount = Discount;
                    oSupplierInvoiceuUpdate.Tax = Tax;
                    oSupplierInvoiceuUpdate.GrandTotal = GrandTotal;
                    oSupplierInvoiceuUpdate.SubTotal = SubTotal;
                    oSupplierInvoiceuUpdate.Status = 20;
                    DataContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult RegisterItemInvoice(int id)
        {
            SupplierInvoiceModel model = new SupplierInvoiceModel();
            SupplierInvoice oSupplierInvoice = new SupplierInvoice();
            try
            {
                oSupplierInvoice = _supplierInvoiceService.GetByKey(id);
               
                BindItemList(oSupplierInvoice.LocationUId);
                var filter = _ingredientBOCService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.SupplierInvoiceId == oSupplierInvoice.UId);
                List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filter, p => p.CreationDate).ToList();
                model.IngredientBOCList = IngredientBOCList;
                decimal SubTotal = 0;
                decimal GrandTotal = 0;
                decimal Tax = 0;
                decimal Discount = 0;
                foreach (IngredientBOC boc in IngredientBOCList)
                {
                    SubTotal += boc.Qty * boc.Price;
                   
                }
                oSupplierInvoice.SubTotal = SubTotal;
                oSupplierInvoice.GrandTotal = (SubTotal - Discount) + Tax;

                model.SupplierInvoice = oSupplierInvoice;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public ActionResult Index()
        {
            UserAccount account = GetCurrentUser();
            var filter = _supplierInvoiceService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true).And(p=>p.LocationUId== account.LocationUId);
            List<SupplierInvoice> SupplierList = _supplierInvoiceService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(SupplierList);
        }
        public ActionResult View(int id)
        {
            SupplierInvoiceModel model = new SupplierInvoiceModel();
            SupplierInvoice oSupplierInvoice = new SupplierInvoice();
            try
            {
                oSupplierInvoice = _supplierInvoiceService.GetByKey(id);

                BindItemList(oSupplierInvoice.LocationUId);
                var filter = _ingredientBOCService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.SupplierInvoiceId == oSupplierInvoice.UId);
                List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filter, p => p.CreationDate).ToList();
                model.IngredientBOCList = IngredientBOCList;
                model.SupplierInvoice = oSupplierInvoice;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
    }
}
