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
using MIMS.Models;
using Dinota.Domain.EventCount;
using Dinota.Domain.MainCourseMealHeader;


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
        private readonly EventCountService _eventCountService;
        private readonly MainCourseMealHeaderService _mainCourseMealHeaderService;


        public ReportController(IDomainContext dataContext, EventCountService eventCountService, F140DataService f140DataService, 
                                F140HeaderService f140HeaderService, MenuItemDetailService menuItemDetailService, MenuPackageService menuPackageService, 
                                MenuOrderOfficerService menuOrderOfficerService, MenuOrderItemDetailService menuOrderItemDetailService, 
                                MenuOrderHeaderService menuOrderHeaderService, MenuItemService menuItemService, 
                                BOCTransactionService bOCTransactionService, IngredientBOCService ingredientBOCService, 
                                MeasurementUnitService measurementUnitService, IngredientInfoService ingredientInfoService, 
                                UserAccountService userAccountService, SLAFLocationService SLAFLocationService, 
                                FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, 
                                DivisionService divisionService, MainCourseMealHeaderService mainCourseMealHeaderService)
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
            _eventCountService = eventCountService;
            _mainCourseMealHeaderService = mainCourseMealHeaderService;
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

        public ActionResult DailySalesSummary()
        {
            DailySaleReportModel model = new DailySaleReportModel();
            model.IsSet = false;
            return View(model);
        }
        
        
        [HttpPost]
        public ActionResult DailySalesSummary(FormCollection Form)
        {
            DailySaleReportModel model = new DailySaleReportModel();
            try
            {
                UserAccount account = GetCurrentUser();
                List<MainCourseMealHeader> MainCourse = new List<MainCourseMealHeader>();
                var filterMC = _mainCourseMealHeaderService.GetDefaultSpecification().And(p => p.LocationUId == account.LocationUId);
                filterMC = filterMC.And(p => p.Active == true);
                MainCourse = _mainCourseMealHeaderService.GetCollection(filterMC, p => p.CreationDate).ToList();
                int BreakfastNormal = 0, BreakfastDiet = 0, BreakfastVeg = 0, BreakfastCadet = 0;
                int LunchNormal = 0, LunchDiet = 0, LunchVeg = 0, LunchCadet = 0;
                int DinnerNormal = 0, DinnerDiet = 0, DinnerVeg = 0, DinnerCadet = 0;


                foreach (MainCourseMealHeader Meal in MainCourse)
                {
                    //Breakfast
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Breakfast)
                    {
                        BreakfastNormal = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Breakfast_diet)
                    {
                        BreakfastDiet = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Breakfast_veg)
                    {
                        BreakfastVeg = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Breakfast_cadet)
                    {
                        BreakfastCadet = Meal.MenuItemId;
                    }

                    // Lunch
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Lunch)
                    {
                        LunchNormal = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Lunch_diet)
                    {
                        LunchDiet = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Lunch_veg)
                    {
                        LunchVeg = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Lunch_cadet)
                    {
                        LunchCadet = Meal.MenuItemId;
                    }

                    // Dinner
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Dinner)
                    {
                        DinnerNormal = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Dinner_diet)
                    {
                        DinnerDiet = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Dinner_veg)
                    {
                        DinnerVeg = Meal.MenuItemId;
                    }
                    if (Meal.MealId == (int)DataStruct.MainCourseMeal.Dinner_cadet)
                    {
                        DinnerCadet = Meal.MenuItemId;
                    }
                }

                TryUpdateModel(model);
                model.IsSet = true;
                DateTime FromDate = model.EffectiveDate.Date;
                DateTime ToDate = model.EffectiveDate.Date.AddDays(1).AddTicks(-1);
                //Breakafast
                var filterDutyB = _menuOrderItemDetailService.GetDefaultSpecification().And(p=>p.MenuOrderHeader.LocationUId==account.LocationUId);
                //filterDutyB = filterDutyB.And(p => p.Active == true).And(p=>p.MenuOrderHeader.OrderDate>= FromDate).And(p=>p.MenuOrderHeader.OrderDate <= ToDate);
                //filterDutyB = filterDutyB.And(p=>p.MenuItemUId== (int)DataStruct.MainCourse.Rma_Breakfast).And(p => p.MenuItemUId == (int)DataStruct.MainCourse.Rma_Breakfast_diet).And(p => p.MenuItemUId == (int)DataStruct.MainCourse.Rma_Breakfast_veg);
                filterDutyB = filterDutyB.And(p => p.Active == true).And(p=>p.MenuOrderHeader.Active==true)
                .And(p => p.MenuOrderHeader.OrderDate >= FromDate)
                .And(p => p.MenuOrderHeader.OrderDate <= ToDate)
                
                .Or(p => p.MenuItemUId == BreakfastNormal)
                .Or(p => p.MenuItemUId == BreakfastDiet)
                .Or(p => p.MenuItemUId == BreakfastVeg)
                .Or(p => p.MenuItemUId == BreakfastCadet);

                List<MenuOrderItemDetail> BreakfastMenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterDutyB,p=>p.CreationDate).ToList();
                //if (BreakfastMenuOrderItemDetailList.Where(p=>p.MenuOrderHeader.MenuHeaderType==(int)DataStruct.MenuHeaderType.Duty).ToList().Count > 0)
                //{
                //    model.DutyBreakfastCount = BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).First().Qty;
                //}
                List<int> BreakfastMealIdList = new List<int>();
                BreakfastMealIdList.Add(BreakfastNormal);
                BreakfastMealIdList.Add(BreakfastDiet);
                BreakfastMealIdList.Add(BreakfastVeg);
                BreakfastMealIdList.Add(BreakfastCadet);

                model.DutyBreakfastCount = new CustomDataBaseManger().GetMainMealCountCount((int)DataStruct.MenuHeaderType.Duty, FromDate, ToDate, BreakfastMealIdList, account.LocationUId);
                model.CasualBreakfastCount = new CustomDataBaseManger().GetMainMealCountCount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, BreakfastMealIdList, account.LocationUId);
                //if (BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Count > 0)
                //{
                //    model.CasualBreakfastCount = BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Sum(p=>p.Qty);
                //}
                model.DutyBreakfastReceivableAmt =new CustomDataBaseManger().GetMainMealF140Sum((int)DataStruct.MenuHeaderType.Duty, FromDate, ToDate, BreakfastMealIdList, account.LocationUId); 
                model.DutyBreakfastExpenditureAmt = model.DutyBreakfastReceivableAmt;

                //Lunch
                var filterDutyL = _menuOrderItemDetailService.GetDefaultSpecification().And(p => p.MenuOrderHeader.LocationUId == account.LocationUId); 
                filterDutyL = filterDutyL.And(p => p.Active == true).And(p => p.MenuOrderHeader.Active == true)
                .And(p => p.MenuOrderHeader.OrderDate >= FromDate)
                .And(p => p.MenuOrderHeader.OrderDate <= ToDate)
                
                .Or(p => p.MenuItemUId == LunchNormal)
                .Or(p => p.MenuItemUId == LunchDiet)
                .Or(p => p.MenuItemUId == LunchVeg)
                .Or(p => p.MenuItemUId == LunchCadet);


                List<MenuOrderItemDetail> LunchMenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterDutyL, p => p.CreationDate).ToList();
                List<int> LunchMealIdList = new List<int>();
                LunchMealIdList.Add(LunchNormal);
                LunchMealIdList.Add(LunchDiet);
                LunchMealIdList.Add(LunchVeg);
                LunchMealIdList.Add(LunchCadet);

                model.DutyLunchCount = new CustomDataBaseManger().GetMainMealCountCount((int)DataStruct.MenuHeaderType.Duty, FromDate, ToDate, LunchMealIdList, account.LocationUId);
                model.CasualLunchCount = new CustomDataBaseManger().GetMainMealCountCount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, LunchMealIdList, account.LocationUId);
                //if (LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).ToList().Count > 0)
                //{
                //    model.DutyLunchCount = LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).First().Qty;
                //}
                //if (LunchMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Count > 0)
                //{
                //    model.CasualLunchCount = BreakfastMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Sum(p=>p.Qty);
                //}
                model.DutyLunchReceivableAmt = new CustomDataBaseManger().GetMainMealF140Sum((int)DataStruct.MenuHeaderType.Duty, FromDate, ToDate, LunchMealIdList, account.LocationUId);
                model.DutyLunchExpenditureAmt = model.DutyLunchReceivableAmt;

                //Dinner
                var filterDutyD = _menuOrderItemDetailService.GetDefaultSpecification().And(p => p.MenuOrderHeader.LocationUId == account.LocationUId);
                filterDutyD = filterDutyD.And(p => p.Active == true).And(p => p.MenuOrderHeader.Active == true)
                .And(p => p.MenuOrderHeader.OrderDate >= FromDate)
                .And(p => p.MenuOrderHeader.OrderDate <= ToDate)

                .Or(p => p.MenuItemUId == DinnerNormal)
                .Or(p => p.MenuItemUId == DinnerDiet)
                .Or(p => p.MenuItemUId == DinnerVeg)
                .Or(p => p.MenuItemUId == DinnerCadet);


                List<MenuOrderItemDetail> DinnerMenuOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterDutyD, p => p.CreationDate).ToList();
                List<int> DinerMealIdList = new List<int>();

                DinerMealIdList.Add(DinnerNormal);
                DinerMealIdList.Add(DinnerDiet);
                DinerMealIdList.Add(DinnerVeg);
                DinerMealIdList.Add(DinnerCadet);

                model.DutyDinnerCount = new CustomDataBaseManger().GetMainMealCountCount((int)DataStruct.MenuHeaderType.Duty, FromDate, ToDate, DinerMealIdList, account.LocationUId);
                model.CasualDinnerCount = new CustomDataBaseManger().GetMainMealCountCount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, DinerMealIdList, account.LocationUId);
                //if (DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).ToList().Count > 0)
                //{
                //    model.DutyDinnerCount = DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Duty).First().Qty;
                //}
                //if (DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Count > 0)
                //{
                //    model.CasualDinnerCount = DinnerMenuOrderItemDetailList.Where(p => p.MenuOrderHeader.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).ToList().Sum(p => p.Qty);
                //}
                model.DutyDinnerReceivableAmt = new CustomDataBaseManger().GetMainMealF140Sum((int)DataStruct.MenuHeaderType.Duty, FromDate, ToDate, DinerMealIdList, account.LocationUId);
                model.DutyDinnerExpenditureAmt = model.DutyDinnerReceivableAmt;

                model.CasualBreakfastCash = new CustomDataBaseManger().GetCasualAmount((int)DataStruct.MenuHeaderType.Casual,FromDate,ToDate,BreakfastMealIdList, (int)DataStruct.PaymentMethod.Cash, account.LocationUId);
                model.CasualBreakfastCredit = new CustomDataBaseManger().GetCasualAmount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, BreakfastMealIdList, (int)DataStruct.PaymentMethod.Credit, account.LocationUId);
                model.CasualBreakfastExpenditure = model.CasualBreakfastCash + model.CasualBreakfastCredit;
                model.CasualLunchCash = new CustomDataBaseManger().GetCasualAmount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, LunchMealIdList, (int)DataStruct.PaymentMethod.Cash, account.LocationUId);
                model.CasualLunchCredit = new CustomDataBaseManger().GetCasualAmount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, LunchMealIdList, (int)DataStruct.PaymentMethod.Credit, account.LocationUId);
                model.CasualLunchExpenditure = model.CasualLunchCash + model.CasualLunchCredit;
                model.CasualDinnerCash = new CustomDataBaseManger().GetCasualAmount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, DinerMealIdList, (int)DataStruct.PaymentMethod.Cash, account.LocationUId);
                model.CasualDinnerCredit = new CustomDataBaseManger().GetCasualAmount((int)DataStruct.MenuHeaderType.Casual, FromDate, ToDate, DinerMealIdList, (int)DataStruct.PaymentMethod.Credit, account.LocationUId);
                model.CasualDinnerExpenditure = model.CasualDinnerCash + model.CasualDinnerCredit;

                var filterCasual = _menuOrderItemDetailService.GetDefaultSpecification().And(p => p.MenuOrderHeader.LocationUId == account.LocationUId);
                filterCasual = filterCasual.And(p => p.Active == true).And(p => p.MenuOrderHeader.OrderDate >= FromDate).And(p => p.MenuOrderHeader.OrderDate <= ToDate);
                filterCasual = filterCasual.And(p => p.MenuItemUId != BreakfastNormal).And(p => p.MenuItemUId != LunchNormal).And(p => p.MenuItemUId != DinnerNormal);
                filterCasual = filterCasual.And(p => p.MenuItemUId != BreakfastDiet).And(p => p.MenuItemUId != LunchDiet).And(p => p.MenuItemUId != DinnerDiet);
                filterCasual = filterCasual.And(p => p.MenuItemUId != BreakfastVeg).And(p => p.MenuItemUId != LunchVeg).And(p => p.MenuItemUId != DinnerVeg);
                filterCasual = filterCasual.And(p => p.MenuItemUId != BreakfastCadet).And(p => p.MenuItemUId != LunchCadet).And(p => p.MenuItemUId != DinnerCadet);

                List<MenuOrderItemDetail> CasualOrderItemDetailList = _menuOrderItemDetailService.GetCollection(filterCasual, p => p.CreationDate).ToList();
                List<MenuOrderHeader> MenuOrderHeaderExtraMesingCash = new List<MenuOrderHeader>();
                foreach(MenuOrderItemDetail det in CasualOrderItemDetailList)
                {
                    if (MenuOrderHeaderExtraMesingCash.Find(p => p.UId == det.MenuOrderHeader.UId)==null)
                    {
                        MenuOrderHeaderExtraMesingCash.Add(det.MenuOrderHeader);
                    }
                }
                model.ExtraMessingCashAmount= MenuOrderHeaderExtraMesingCash.Where(c => c.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.PaymentMethod == (int)DataStruct.PaymentMethod.Cash).Sum(p => p.F140TotalAmt);
                model.ExtraMessingCreditAmount= MenuOrderHeaderExtraMesingCash.Where(c => c.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual && c.PaymentMethod == (int)DataStruct.PaymentMethod.Credit).Sum(p => p.F140TotalAmt);
                model.ExtraMessingExpenditureAmount= MenuOrderHeaderExtraMesingCash.Where(c => c.MenuHeaderType == (int)DataStruct.MenuHeaderType.Casual).Sum(p => p.F140TotalAmt);
               


                model.BreakfastGasCharges= model.DutyBreakfastExpenditureAmt * (decimal.Parse("2.5") / decimal.Parse("100"));

                model.LunchGasCharges = model.DutyLunchExpenditureAmt * (decimal.Parse("2.5") / decimal.Parse("100"));

                model.DinnerGasCharges = model.DutyDinnerExpenditureAmt * (decimal.Parse("2.5") / decimal.Parse("100"));

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
              
                model.TotalIncomeBreakfast = model.DutyBreakfastReceivableAmt + model.CasualBreakfastCash + model.CasualBreakfastCredit/* + model.BreakfastGasCharges*/;
                model.TotalExpenditureBreakfast=model.DutyBreakfastExpenditureAmt+model.CasualBreakfastExpenditure/* + model.BreakfastGasCharges*/;

                model.TotalIncomeLunch = model.DutyLunchReceivableAmt + model.CasualLunchCash + model.CasualLunchCredit /*+ model.LunchGasCharges*/;
                model.TotalExpenditureLunch = model.DutyLunchExpenditureAmt + model.CasualLunchExpenditure /*+ model.LunchGasCharges*/;

                model.TotalIncomeDinner = model.DutyDinnerReceivableAmt + model.CasualDinnerCash + model.CasualDinnerCredit /*+ model.DinnerGasCharges*/;
                model.TotalExpenditureDinner = model.DutyDinnerExpenditureAmt + model.CasualDinnerExpenditure /*+ model.DinnerGasCharges*/;

                model.TotalIncomeExtramessing = model.ExtraMessingCashAmount + model.ExtraMessingCreditAmount /*+ model.ExtraMessingGasCharges*/ + model.Commodities;
                model.TotalExpenditureExtramessing= model.ExtraMessingExpenditureAmount /*+ model.ExtraMessingGasCharges*/ + model.Commodities;
                model.TotalIncomeAmount = model.TotalIncomeBreakfast + model.TotalIncomeLunch + model.TotalIncomeDinner+model.TotalIncomeExtramessing;
                model.TotalExpenditureAmount = model.TotalExpenditureBreakfast + model.TotalExpenditureLunch + model.TotalExpenditureDinner + model.TotalExpenditureExtramessing;
                model.BreakfastCommodities = (model.DutyBreakfastCount + model.CasualBreakfastCount) * decimal.Parse("5");
                model.LunchCommodities = (model.DutyLunchCount + model.CasualLunchCount) * decimal.Parse("5");
                model.DinnerCommodities = (model.DutyDinnerCount + model.CasualDinnerCount) * decimal.Parse("5");
                model.TotalCommodotiesAmount = model.BreakfastCommodities + model.LunchCommodities + model.DinnerCommodities;
                
            }
            catch (Exception e)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("Please Contact Administrator");
                return RedirectToAction("DailySalesSummary");

                throw;
            }
            return View(model);
        }
        public ActionResult ChartSummary()
        {
            EventReportSummary model = new EventReportSummary();
            var filter = _eventCountService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true);
            List<EventCount> EventCountList= _eventCountService.GetCollection(filter, p => p.CreationDate).ToList();
            model.Incount = EventCountList.Where(p => p.Type == 1).Sum(p=>p.Count);
            model.Outcount = EventCountList.Where(p => p.Type == 2).Sum(p=>p.Count);
            model.Pendingcount = model.Incount - model.Outcount;
            List<EntrySummary> EntrySummaryLis = new List<EntrySummary>();
            int ToHour = DateTime.Now.Hour;
            int StartHour = 6;
            EntrySummary sum0 = new EntrySummary();
            sum0.Time = "6";
            sum0.Incount = 0;
            sum0.Outcount = 0;
            EntrySummaryLis.Add(sum0);
            for (int i= StartHour;i<= ToHour; i++)
            {
                EntrySummary sum = new EntrySummary();
                DateTime FromDate = DateTime.Now.Date.AddTicks(1).AddHours(i);
                DateTime ToDate = DateTime.Now.Date.AddTicks(1).AddHours(i+1);
                var filterSum = _eventCountService.GetDefaultSpecification();
                filterSum = filterSum.And(p => p.Active == true).And(p=>p.EffectiveDate>= FromDate).And(p=>p.EffectiveDate<=ToDate);
                List<EventCount> EventCountSumList= _eventCountService.GetCollection(filterSum, p => p.CreationDate).ToList();
                sum.Incount= EventCountSumList.Where(p => p.Type == 1).Sum(p => p.Count);
                sum.Outcount= EventCountSumList.Where(p => p.Type == 2).Sum(p => p.Count);
                sum.Time = (i + 1).ToString();
                EntrySummaryLis.Add(sum);
            }
            model.EntrySummaryList = EntrySummaryLis;
            //model.IsSet = false;
            return View(model);
        }

       
    }
}
