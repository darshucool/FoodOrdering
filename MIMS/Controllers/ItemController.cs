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
using AlfasiWeb;

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
        private readonly IngredientBOCService _ingredientBOCService;
        private readonly BOCTransactionService _bOCTransactionService;
        private readonly StockSheetTransactionService _stockSheetTransactionService;
        private readonly F140HeaderService _f140HeaderService;
        private readonly F140DataService _f140DataService;

        public ItemController(IDomainContext dataContext, F140DataService f140DataService, F140HeaderService f140HeaderService, StockSheetTransactionService stockSheetTransactionService, BOCTransactionService bOCTransactionService, IngredientBOCService ingredientBOCService, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, UserAccountService userAccountService, RoomNoService roomNoService, RoomInfoService roomInfoService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
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
        public ActionResult BOCRegister()
        {
            IngredientInfo info = new IngredientInfo();
            try
            {
                BindItemList();
                BindMeasurementList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(info);   
         }
        public ActionResult ItemStockSummary(int id)
        {
            StockSummaryModel model = new StockSummaryModel();
            try
            {
                IngredientInfo info = _ingredientInfoService.GetByKey(id);
                model.IngredientInfo = info;
                var filter = _ingredientBOCService.GetDefaultSpecification();
                filter = filter.And(p=>p.Active==true).And(p=>p.IngredientUId== id);
                List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filter, p => p.CreationDate).ToList();
                model.IngredientBOCList = IngredientBOCList;
                var filterTr = _bOCTransactionService.GetDefaultSpecification();
                filterTr = filterTr.And(p => p.Active == true).And(p => p.IngredientBOC.IngredientUId == id).And(p=>p.MenuOrderHeader.Active==true).And(p=>p.MenuOrderHeader.Status>(int)DataStruct.MenuOrderItemStatus.Pending).And(p => p.MenuOrderHeader.Status < (int)DataStruct.MenuOrderItemStatus.Cancel);
                List<BOCTransaction> BOCTransactionList = _bOCTransactionService.GetCollection(filterTr, p => p.CreationDate).ToList();
                model.BOCTransactionList = BOCTransactionList;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        public ActionResult StockSheet(int id)
        {
            UserAccount account = GetCurrentUser();

            StockSheetTransactionModel StockSheetTransactionModelList = new StockSheetTransactionModel();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var filter = _stockSheetTransactionService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p=>p.EffectiveDate>= firstDayOfMonth).And(p=>p.EffectiveDate<= lastDayOfMonth);
            List<StockSheetTransaction> StockSheetTransactionList = _stockSheetTransactionService.GetCollection(filter, p => p.CreationDate).ToList();
            if (StockSheetTransactionList.Count > 0)
            {

            }
            DateTime CurrentFromDate = DateTime.Now.Date;
            DateTime CurrentToDate = DateTime.Now.Date.AddDays(1).AddTicks(-1);
            var filterI = _ingredientInfoService.GetDefaultSpecification();
            filterI = filterI.And(p => p.Active == true).And(p => p.ItemTypeId == id).And(p => p.LocationUId == account.LocationUId);
            List<IngredientInfo> IngredientInfoList = _ingredientInfoService.GetCollection(filterI, p => p.CreationDate).ToList();
            List<IngredientInfo> IngredientList = new List<IngredientInfo>();
            List<BOCAmountList> BOCAmountList = new List<BOCAmountList>();
            List<BOCQtyList> BOCQtyList = new List<BOCQtyList>();
            List<IngIssueList> IngIssueList = new List<IngIssueList>();
            foreach (IngredientInfo info in IngredientInfoList)
            {
                IngredientList.Add(info);
               
                var filterBOCA = _ingredientBOCService.GetDefaultSpecification();
                filterBOCA = filterBOCA.And(p => p.Active == true).And(p => p.IngredientUId == info.UId);
                List<IngredientBOC> IngredientInfoBOCAmountList = _ingredientBOCService.GetCollection(filterBOCA, p => p.CreationDate).OrderByDescending(p=>p.UId).ToList();
                BOCAmountList oBOCAmount = new BOCAmountList();
                if (IngredientInfoBOCAmountList.Count > 0)
                {
                    oBOCAmount.BocAmount = IngredientInfoBOCAmountList[0].Price;
                }
                else
                {
                    oBOCAmount.BocAmount = 0;
                }
                BOCAmountList.Add(oBOCAmount);
                var filterBOCNew = _ingredientBOCService.GetDefaultSpecification();
                filterBOCNew = filterBOCNew.And(p=>p.Active==true).And(p=>p.IngredientUId== info.UId).And(p=>p.EffectiveDate>= CurrentFromDate).And(p=>p.EffectiveDate<= CurrentToDate);
                List<IngredientBOC> IngredientInfoBOCNewList = _ingredientBOCService.GetCollection(filterBOCNew, p=>p.CreationDate).ToList();

                var filterBOC = _ingredientBOCService.GetDefaultSpecification();
                filterBOC = filterBOC.And(p => p.Active == true).And(p => p.IngredientUId == info.UId);
                List<IngredientBOC> IngredientInfoBOCList = _ingredientBOCService.GetCollection(filterBOC, p => p.CreationDate).ToList();
                BOCQtyList bqty = new BOCQtyList();
                bqty.Qty= IngredientInfoBOCList.Sum(p => p.Qty);
                BOCQtyList.Add(bqty);

                var filterData = _f140DataService.GetDefaultSpecification();
                filterData = filterData.And(p => p.Active == true).And(p=>p.IngridientUId== info.UId).And(p => p.F140Header.MenuOrderHeader.EffectiveDate >= CurrentFromDate).And(p => p.F140Header.MenuOrderHeader.EffectiveDate <= CurrentToDate);
                List<F140Data> DataList = _f140DataService.GetCollection(filterData, p => p.CreationDate).ToList();
                IngIssueList IngIss = new IngIssueList();
                IngIss.Qty = DataList.Sum(p=>p.Qty);
                IngIssueList.Add(IngIss);
            }
            StockSheetTransactionModelList.IngredientInfoList = IngredientList;
            StockSheetTransactionModelList.BOCAmountList = BOCAmountList;
            StockSheetTransactionModelList.BOCQtyList = BOCQtyList;
            StockSheetTransactionModelList.IngIssueList = IngIssueList;
            return View(StockSheetTransactionModelList);
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
        public ActionResult StockCheck()
        {
            StockCheckModel model = new StockCheckModel();
            List<IngredientInfo> IngredientInfoList = new List<IngredientInfo>();
            try
            {
                List<IngriendientStockInfo> IngredientStockInfoList = new List<IngriendientStockInfo>();
                var filter = _ingredientInfoService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true);
                IngredientInfoList = _ingredientInfoService.GetCollection(filter, p => p.CreationDate).ToList() ;
                foreach (IngredientInfo info in IngredientInfoList)
                {
                    IngriendientStockInfo check = new IngriendientStockInfo();
                    check.ItemName = info.ItemName;
                    var filterB = _ingredientBOCService.GetDefaultSpecification();
                    filterB = filterB.And(p => p.Active == true).And(p => p.IngredientUId == info.UId);
                    List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filterB, p => p.CreationDate).ToList();
                    check.CurrentStock = IngredientBOCList.Sum(p=>p.Qty);
                    check.Unit = info.MeasurementUnit.Unit;
                    IngredientStockInfoList.Add(check);
                }
                model.IngredientInfoList = IngredientStockInfoList;
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(model);   
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
            UserAccount account = GetCurrentUser();
            var filter = _ingredientInfoService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true).And(p=>p.LocationUId==account.LocationUId);
            List<IngredientInfo> IngredientInfoList = _ingredientInfoService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(IngredientInfoList);
        }


        public ActionResult GetMeasurementUnit(int ingriedientUId)
        {
            string Unit = new CustomDataBaseManger().GetMeasurementUnit(ingriedientUId);

            return Json(new { Unit }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DayEnd()
        {

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult TotalStockBOCValue()
        {
            List<BOCTotalModel> List = new List<BOCTotalModel>();
            try
            {
                var filter = _ingredientInfoService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true);
                List<IngredientInfo> IngredientInfoList = _ingredientInfoService.GetCollection(filter, p => p.CreationDate).ToList() ;
                
                foreach (IngredientInfo info in IngredientInfoList)
                {
                    decimal TotalValue = 0;
                    decimal TotalWeight = 0;
                    BOCTotalModel boc = new BOCTotalModel();
                    boc.IngredientInfo = info;
                    var filterI = _ingredientBOCService.GetDefaultSpecification();
                    filterI = filterI.And(p=>p.IngredientUId==info.UId).And(p=>p.Active==true).And(p=>p.Qty>0);
                    List<IngredientBOC> IngredientBOCList = _ingredientBOCService.GetCollection(filterI,p=>p.CreationDate).ToList();
                    foreach (IngredientBOC Iboc in IngredientBOCList)
                    {
                        TotalValue += Iboc.Qty * Iboc.Price;
                        TotalWeight += Iboc.Qty;
                    }
                    boc.BOCValue = TotalValue;
                    boc.RemQty = TotalWeight;
                    List.Add(boc);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(List);
        }

    }
}
