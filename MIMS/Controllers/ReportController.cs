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
using Dinota.Domain.IngredientInfo;
using Dinota.Domain.MeasurementUnit;
using Dinota.Domain.IngredientBOC;
using Dinota.Domain.BOCTransaction;
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuOrderHeader;
using Dinota.Domain.MenuOrderItemDetail;
using Dinota.Domain.MenuOrderOfficer;
using Dinota.Domain.MenuPackage;
using Dinota.Domain.F140Header;
using Dinota.Domain.F140Data;
using Dinota.Domain.MenuItemDetail;
using AlfasiWeb;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class ReportController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        private readonly DistrictService _districtService;
        private readonly SLAFLocationService _SLAFLocationService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly IngredientInfoService _ingredientInfoService;
        private readonly MeasurementUnitService _measurementUnitService;
        private readonly IngredientBOCService _ingredientBOCService;
        private readonly BOCTransactionService _bOCTransactionService;
        private readonly MenuItemService _menuItemService;
        private readonly MenuOrderHeaderService _menuOrderHeaderService;
        private readonly MenuOrderItemDetailService _menuOrderItemDetailService;
        private readonly MenuOrderOfficerService _menuOrderOfficerService;
        private readonly MenuPackageService _menuPackageService;
        private readonly MenuItemDetailService _menuItemDetailService;
        private readonly F140HeaderService _f140HeaderService;
        private readonly F140DataService _f140DataService;

        public ReportController(IDomainContext dataContext, F140DataService f140DataService, F140HeaderService f140HeaderService, MenuItemDetailService menuItemDetailService, MenuPackageService menuPackageService, MenuOrderOfficerService menuOrderOfficerService, MenuOrderItemDetailService menuOrderItemDetailService, MenuOrderHeaderService menuOrderHeaderService, MenuItemService menuItemService, BOCTransactionService bOCTransactionService, IngredientBOCService ingredientBOCService, MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, UserAccountService userAccountService, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
            : base(dataContext)
        {
            _divisionService = divisionService;
            _userTypeService = userTypeService;
            _districtService = districtService;
            _fuelTypeService = fuelTypeService;
            _SLAFLocationService = SLAFLocationService;
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
        }
        // [AuthorizeUserAccessLevel()]
        public ActionResult SLAFLocationSummary()
        {
            ReportModel model = new ReportModel();
            BindSLAFLocationList();
            return View(model);
        }
        
       
        public void BindSLAFLocationList()
        {
            try
            {
                var filter = _SLAFLocationService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _SLAFLocationService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Name");
                ViewData[ViewDataKeys.SLAFLocationList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public void BindFuelTypeList()
        {
            try
            {
                var filter = _fuelTypeService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _fuelTypeService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Name");
                ViewData[ViewDataKeys.FuelTypeList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        
        public ActionResult FuelDraw()
        { 
            return View();
        }
        public ActionResult DailySalesSummary()
        {
            DailySaleReportModel model = new DailySaleReportModel();
            model.IsSet = false;
            return View(model);
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
        public ActionResult DailySalesSummary(FormCollection Form)
        {
            DailySaleReportModel model = new DailySaleReportModel();
            try
            {
                UserAccount account = GetCurrentUser();
                TryUpdateModel(model);
                model.IsSet = true;
                DateTime FromDate = model.EffectiveDate.Date;
                DateTime ToDate = model.EffectiveDate.Date.AddDays(1).AddTicks(-1);
                //Breakafast
                var filterDutyB = _menuOrderItemDetailService.GetDefaultSpecification().And(p=>p.MenuOrderHeader.LocationUId==account.LocationUId);
                filterDutyB = filterDutyB.And(p => p.Active == true).And(p=>p.MenuOrderHeader.OrderDate>= FromDate).And(p=>p.MenuOrderHeader.OrderDate <= ToDate);
                filterDutyB = filterDutyB.And(p=>p.MenuItemUId== (int)DataStruct.MainCourse.Rma_Breakfast).And(p => p.MenuItemUId == (int)DataStruct.MainCourse.Rma_Breakfast_diet).And(p => p.MenuItemUId == (int)DataStruct.MainCourse.Rma_Breakfast_veg);
                List<MenuOrderItemDetail> BreakfastMenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterDutyB,p=>p.CreationDate).ToList();
                if (BreakfastMenuOrderItemDetailList.Where(p=>p.MenuOrderHeader.MenuHeaderType==(int)DataStruct.MenuHeaderType.Duty).ToList().Count > 0)
                {
                    model.DutyBreakfastCount = BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).First().MenuOrderHeader.OfficerCount;
                }
                if (BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Count > 0)
                {
                    model.CasualBreakfastCount = BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Sum(p=>p.MenuOrderHeader.OfficerCount);
                }
                model.DutyBreakfastReceivableAmt = BreakfastMenuOrderItemDetailList.Where(p=>p.MenuOrderHeader.MenuHeaderType== (int)DataStruct.MenuHeaderType.Duty).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.DutyBreakfastExpenditureAmt = model.DutyBreakfastReceivableAmt;
                //Lunch
                var filterDutyL = _menuOrderItemDetailService.GetDefaultSpecification().And(p => p.MenuOrderHeader.LocationUId == account.LocationUId); 
                filterDutyL = filterDutyL.And(p => p.Active == true).And(p => p.MenuOrderHeader.OrderDate >= FromDate).And(p => p.MenuOrderHeader.OrderDate <= ToDate);
                filterDutyL = filterDutyL.And(p => p.MenuItemUId == (int)DataStruct.MainCourse.Rma_Lunch);
                List<MenuOrderItemDetail> LunchMenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterDutyL, p => p.CreationDate).ToList();
                if (LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).ToList().Count > 0)
                {
                    model.DutyLunchCount = LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).First().MenuOrderHeader.OfficerCount;
                }
                if (LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Count > 0)
                {
                    model.CasualLunchCount = BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Sum(p=>p.MenuOrderHeader.OfficerCount);
                }
                model.DutyLunchReceivableAmt = LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.DutyLunchExpenditureAmt = model.DutyLunchReceivableAmt;
                //Dinner
                var filterDutyD = _menuOrderItemDetailService.GetDefaultSpecification().And(p => p.MenuOrderHeader.LocationUId == account.LocationUId);
                filterDutyD = filterDutyD.And(p => p.Active == true).And(p => p.MenuOrderHeader.OrderDate >= FromDate).And(p => p.MenuOrderHeader.OrderDate <= ToDate);
                filterDutyD = filterDutyD.And(p => p.MenuItemUId == (int)DataStruct.MainCourse.Rma_Dinner);
                List<MenuOrderItemDetail> DinnerMenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterDutyD, p => p.CreationDate).ToList();
                if (DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).ToList().Count > 0)
                {
                    model.DutyDinnerCount = DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).First().MenuOrderHeader.OfficerCount;
                }
                if (DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Count > 0)
                {
                    model.CasualDinnerCount = DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Sum(p => p.MenuOrderHeader.OfficerCount);
                }
                model.DutyDinnerReceivableAmt = DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.DutyDinnerExpenditureAmt = model.DutyDinnerReceivableAmt;
                
                model.CasualBreakfastCash = BreakfastMenuOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Cash).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.CasualBreakfastCredit = BreakfastMenuOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Credit).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.CasualBreakfastExpenditure = model.CasualBreakfastCash + model.CasualBreakfastCredit;
                model.CasualLunchCash = LunchMenuOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Cash).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.CasualLunchCredit = LunchMenuOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Credit).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.CasualLunchExpenditure = model.CasualLunchCash + model.CasualLunchCredit;
                model.CasualDinnerCash = DinnerMenuOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Cash).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.CasualDinnerCredit = DinnerMenuOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Credit).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.CasualDinnerExpenditure = model.CasualDinnerCash + model.CasualDinnerCredit;
                var filterCasual = _menuOrderItemDetailService.GetDefaultSpecification().And(p => p.MenuOrderHeader.LocationUId == account.LocationUId);
                filterCasual = filterCasual.And(p => p.Active == true).And(p => p.MenuOrderHeader.OrderDate >= FromDate).And(p => p.MenuOrderHeader.OrderDate <= ToDate);
                filterCasual = filterCasual.And(p => p.MenuItemUId != (int)DataStruct.MainCourse.Rma_Breakfast).And(p => p.MenuItemUId != (int)DataStruct.MainCourse.Rma_Lunch).And(p => p.MenuItemUId != (int)DataStruct.MainCourse.Rma_Dinner);
                List<MenuOrderItemDetail> CasualOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterCasual, p => p.CreationDate).ToList();
                
                model.ExtraMessingCashAmount= CasualOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Cash).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.ExtraMessingCreditAmount= CasualOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.MenuOrderHeader.PaymentMethod == (int)DataStruct.PaymentMethod.Credit).Sum(p => p.MenuOrderHeader.F140TotalAmt);
                model.ExtraMessingExpenditureAmount= CasualOrderItemDetailList.Where(c => c.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).Sum(p => p.MenuOrderHeader.F140TotalAmt);
               

                var filterG = _menuItemService.GetDefaultSpecification();
                filterG = filterG.And(p => p.UId == (int)DataStruct.MainCourse.Rma_Breakfast);
                MenuItem itemB = _menuItemService.GetBy(filterG);
                model.BreakfastGasCharges= BreakfastMenuOrderItemDetailList.Sum(p => p.MenuOrderHeader.F140TotalAmt)*(itemB.GasChargePercent/decimal.Parse("100"));
                var filterL = _menuItemService.GetDefaultSpecification();
                filterL = filterL.And(p => p.UId == (int)DataStruct.MainCourse.Rma_Lunch);
                MenuItem itemL = _menuItemService.GetBy(filterL);
                model.LunchGasCharges = LunchMenuOrderItemDetailList.Sum(p => p.MenuOrderHeader.F140TotalAmt) * itemL.GasChargePercent / decimal.Parse("100");
                var filterD = _menuItemService.GetDefaultSpecification();
                filterD = filterD.And(p => p.UId == (int)DataStruct.MainCourse.Rma_Dinner);
                MenuItem itemD = _menuItemService.GetBy(filterD);
                model.DinnerGasCharges = LunchMenuOrderItemDetailList.Sum(p => p.MenuOrderHeader.F140TotalAmt) * itemD.GasChargePercent / decimal.Parse("100");
                decimal ExtramessingGasCharge=0;
                foreach (MenuOrderItemDetail detail in CasualOrderItemDetailList)
                {
                    ExtramessingGasCharge += detail.MenuOrderHeader.F140TotalAmt * (detail.MenuItem.GasChargePercent / decimal.Parse("100"));
                }
                model.ExtraMessingGasCharges = ExtramessingGasCharge;
                model.TotalGasAmount = model.BreakfastGasCharges + model.LunchGasCharges + model.DinnerGasCharges + model.ExtraMessingGasCharges;
                model.TotalDutyReceivableAmount = model.DutyBreakfastReceivableAmt + model.DutyLunchReceivableAmt + model.DutyDinnerReceivableAmt;
                model.TotalDutyExpenditureAmount = model.DutyBreakfastExpenditureAmt + model.DutyLunchExpenditureAmt + model.DutyDinnerExpenditureAmt;

                model.TotalExtraMessingCredit = model.ExtraMessingCreditAmount;
                model.TotalExtraMessingCash = model.ExtraMessingCashAmount;
                model.TotalExtraMessingExpenditure = model.TotalExtraMessingCredit + model.TotalExtraMessingCash;
              
                model.TotalIncomeBreakfast = model.DutyBreakfastReceivableAmt + model.CasualBreakfastCash + model.CasualBreakfastCredit + model.BreakfastGasCharges;
                model.TotalExpenditureBreakfast=model.DutyBreakfastExpenditureAmt+model.CasualBreakfastExpenditure + model.BreakfastGasCharges;

                model.TotalIncomeLunch = model.DutyLunchReceivableAmt + model.CasualLunchCash + model.CasualLunchCredit + model.LunchGasCharges;
                model.TotalExpenditureLunch = model.DutyLunchExpenditureAmt + model.CasualLunchExpenditure + model.LunchGasCharges;

                model.TotalIncomeDinner = model.DutyDinnerReceivableAmt + model.CasualDinnerCash + model.CasualDinnerCredit + model.DinnerGasCharges;
                model.TotalExpenditureDinner = model.DutyDinnerExpenditureAmt + model.CasualDinnerExpenditure + model.DinnerGasCharges;

                model.TotalIncomeExtramessing = model.ExtraMessingCashAmount + model.ExtraMessingCreditAmount + model.ExtraMessingGasCharges + model.Commodities;
                model.TotalExpenditureExtramessing= model.ExtraMessingExpenditureAmount + model.ExtraMessingGasCharges + model.Commodities;
                model.TotalIncomeAmount = model.TotalIncomeBreakfast + model.TotalIncomeLunch + model.TotalIncomeDinner+model.TotalIncomeExtramessing;
                model.TotalExpenditureAmount = model.TotalExpenditureBreakfast + model.TotalExpenditureLunch + model.TotalExpenditureDinner + model.TotalExpenditureExtramessing;
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        
    }
}
