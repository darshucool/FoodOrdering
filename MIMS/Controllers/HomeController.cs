using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MIMS.Helpers;

using MIMS.Security;
using Excel = Microsoft.Office.Interop.Excel;
using Dinota.Core.Data;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Core.Specification;
using Dinota.Security;
using System;
using AlfasiWeb.Models;
using Dinota.Domain.MenuCategory;
using Dinota.Domain.MenuOrder;
using Dinota.Domain.MenuFavorite;
using Dinota.Domain.MenuItem;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class HomeController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly MenuCategoryService _menuCategoryService;
        private readonly MenuOrderService _menuOrderService;
        private readonly MenuFavoriteService _menuFavoriteService;
        private readonly MenuItemService _menuItemService;

        public HomeController(IDomainContext dataContext, MenuItemService menuItemService, MenuFavoriteService menuFavoriteService, MenuOrderService menuOrderService, MenuCategoryService menuCategoryService, UserAccountService userAccountService)
            : base(dataContext)
        {
            _userAccountService = userAccountService;
            _menuCategoryService = menuCategoryService;
            _menuOrderService = menuOrderService;
            _menuFavoriteService = menuFavoriteService;
            _menuItemService = menuItemService;
        }
        //[AuthorizeUserAccessLevel()]
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            try
            {
                
                UserAccount account = GetCurrentUser();
                model.SLAFLocationUId = account.LocationUId;
                var filter = _menuCategoryService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p=>p.SLAFLocationUId==account.LocationUId);
                model.MenuCategoryList = _menuCategoryService.GetCollection(filter, p => p.CreationDate).ToList();

                //var filterF = _fuelDrawInfoService.GetDefaultSpecification();
                //filterF = filterF.And(p => p.Active == true);
                //model.VehicleDrawCount = _fuelDrawInfoService.GetCollection(filterF, p => p.CreationDate).ToList().Count();

                //var filterT = _fuelDrawInfoService.GetDefaultSpecification();
                //filterT = filterT.And(p => p.Active == true);
                //model.DrawQty = _fuelDrawInfoService.GetCollection(filterF, p => p.CreationDate).ToList().Sum(p=>p.DrawQty);

               
                if (account.UserTypeId == 2|| account.UserTypeId == 4|| account.UserTypeId == 5)
                {
                    //return RedirectToAction("MenuOrderList", "Menu");
                    return RedirectToAction("OrderList", "Order");
                }
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");  
            }
            
            //List<SessionNaviModel> SessionNaviModelList = new CustomDataBaseManger().SelectPermittedNavigation(account.UserTypeId);
            //Session["NaviModel"] = null;
            //Session["NaviModel"] = SessionNaviModelList;
            return View(model);
        }
        public ActionResult SampleView()
        {
            HomeModel model = new HomeModel();
            try
            {

                var filterF = _menuOrderService.GetDefaultSpecification();
                filterF = filterF.And(p => p.Active == true);
                model.MenuOrderList = _menuOrderService.GetCollection(filterF, p => p.CreationDate).OrderByDescending(p => p.UId).Take(5).ToList();

                //var filterT = _fuelDrawInfoService.GetDefaultSpecification();
                //filterT = filterT.And(p => p.Active == true);
                //model.DrawQty = _fuelDrawInfoService.GetCollection(filterF, p => p.CreationDate).ToList().Sum(p=>p.DrawQty);

                UserAccount account = GetCurrentUser();
                if (account.UserTypeId == 2)
                {
                    return RedirectToAction("MenuOrderList", "Menu");
                }
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");
            }

            //List<SessionNaviModel> SessionNaviModelList = new CustomDataBaseManger().SelectPermittedNavigation(account.UserTypeId);
            //Session["NaviModel"] = null;
            //Session["NaviModel"] = SessionNaviModelList;
            return View(model);
        }
        public ActionResult MasterMenu()
        {
            HomeModel model = new HomeModel();
            try
            {
                UserAccount account = GetCurrentUser();
                model.SLAFLocationUId = account.LocationUId;
                var filterF = _menuOrderService.GetDefaultSpecification();
                filterF = filterF.And(p => p.Active == true);
                model.MenuOrderList = _menuOrderService.GetCollection(filterF, p => p.CreationDate).OrderByDescending(p => p.UId).Take(3).ToList();


                var filterP = _menuOrderService.GetDefaultSpecification();
                filterP = filterP.And(p => p.Active == true).And(p => p.UserId == account.Id);
                model.PastOrderList = _menuOrderService.GetCollection(filterP, p => p.CreationDate).OrderByDescending(p => p.UId).Take(5).ToList();

                var filterFav = _menuFavoriteService.GetDefaultSpecification();
                filterFav = filterFav.And(p => p.Active == true).And(p => p.UserId == account.Id);
                model.MenuFavoriteList = _menuFavoriteService.GetCollection(filterFav, p => p.CreationDate).OrderByDescending(p => p.UId).Take(5).ToList();
                //var filterT = _fuelDrawInfoService.GetDefaultSpecification();
                //filterT = filterT.And(p => p.Active == true);
                //model.DrawQty = _fuelDrawInfoService.GetCollection(filterF, p => p.CreationDate).ToList().Sum(p=>p.DrawQty);

               
                if (account.UserTypeId == 2|| account.UserTypeId == 4|| account.UserTypeId == 5)
                {
                    return RedirectToAction("OrderList", "Order");
                }
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");
            }

            //List<SessionNaviModel> SessionNaviModelList = new CustomDataBaseManger().SelectPermittedNavigation(account.UserTypeId);
            //Session["NaviModel"] = null;
            //Session["NaviModel"] = SessionNaviModelList;
            return View(model);
        }
        public ActionResult AddMenuFavorite(int id)
        {
            try
            {
                MenuItem item = _menuItemService.GetByKey(id);
                UserAccount account = GetCurrentUser();
                MenuFavorite fav = new MenuFavorite();
                fav.UserId = account.Id;
                fav.MenuItemUId = id;
                fav.Active = true;
                _menuFavoriteService.Add(fav);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully added to your favorite list");
                return RedirectToAction("MenuItemIndex", "Menu", new { id = item.MenuCategoryUId });  
            }
            catch (Exception)
            {
                TempData[ViewDataKeys.Message] = new FailMessage("You need to login again");
                return RedirectToAction("Login", "Account");  
            }
            return View();
        }
        public ActionResult Profile()
        {
            UserAccount account = GetCurrentUser();
            return View(account);
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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult DataTransfer(int? archive)
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:/03200/officer2.xls");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            int UID = 43709;
            bool insertNew = false;
            int PastHouseRefId = 0;
            for (int i = 0; i <= rowCount; i++)
            {
                if (i > 0)
                {
                    UserAccount useraccount = new UserAccount();
                    if (xlRange.Cells[i, 1].Value != null)
                    {
                        string v = xlRange.Cells[i, 1].Value.ToString();

                        useraccount.UserName = v;
                        useraccount.ServiceNo = v;
                    }
                    if (xlRange.Cells[i, 2].Value != null)
                    {
                        string v1 = xlRange.Cells[i, 2].Value.ToString();
                        useraccount.RankUId = int.Parse(v1);
                    }
                    else
                    {
                        useraccount.RankUId  = 0;
                    }

                    if (xlRange.Cells[i, 3].Value != null)
                    {
                        string v = xlRange.Cells[i, 3].Value.ToString();

                        useraccount.Name = v;
                    }
                    else
                    {
                        useraccount.Name = "";
                    }
                    if (xlRange.Cells[i, 4].Value != null)
                    {
                        useraccount.Telephone1 = xlRange.Cells[i, 4].Value.ToString();
                    }
                    else
                    {
                        useraccount.Telephone1 = "";
                    }
                    useraccount.Email = "test@airforce.lk";
                    useraccount.PasswordHash = "hVA1KfFjdLnlRSlVXUMW0uKUdSuFbrPrbKxCeYU2mGk=";
                    useraccount.IsFirstLogin = true;
                    useraccount.UserTypeId = 1;
                    useraccount.LastActiveDate = DateTime.Now;
                    useraccount.DivisionId = 8;
                    useraccount.LocationUId = 19;
                    useraccount.Active = true;
                    var filter = _userAccountService.GetDefaultSpecification();
                    filter = filter.And(p=>p.Active==true).And(p=>p.UserName== useraccount.UserName);
                    UserAccount acc = _userAccountService.GetBy(filter);
                    if (acc == null)
                    {
                        _userAccountService.Add(useraccount);
                        DataContext.SaveChanges();
                    }
                    else
                    {
                        Excel.Range row1 = xlRange.Rows.Cells[i, 6];
                        row1.Value = "NOSERVICE_NO";
                       
                    }

                }

                // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                //}

                //add useful things here!   
                //}
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return View();
        }
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public ActionResult DataDeletion(int? archive)
        //{
        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:/FuelExcel/FuelExcel/do/delete.xlsx");
        //    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;
        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;
        //    int UID = 43709;
        //    bool insertNew = false;
        //    int PastHouseRefId = 0;
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        if (i > 0)
        //        {
        //            VehicleInfo oVehicleInfo = new VehicleInfo();
        //            string ServiceNo = "";
        //            if (xlRange.Cells[i, 1].Value != null)
        //            {
        //                ServiceNo = xlRange.Cells[i, 1].Value.ToString();
                        
        //            }
        //            if (!string.IsNullOrEmpty(ServiceNo))
        //            {
        //                var filter = _vehicleInfoService.GetDefaultSpecification();
        //                filter = filter.And(p => p.Active == true).And(p => p.ServiceNo == ServiceNo);
        //                List<VehicleInfo> VehicleInfoList = _vehicleInfoService.GetCollection(filter, p => p.CreationDate).ToList();
        //                if (VehicleInfoList.Count > 0)
        //                {
        //                    foreach (VehicleInfo inV in VehicleInfoList)
        //                    {
        //                        VehicleInfo v = _vehicleInfoService.GetByKey(inV.UId);
        //                        v.Status = 2;
        //                        DataContext.SaveChanges();
        //                    }
        //                }
        //                else
        //                {
        //                    string s = "";
        //                }
        //            }
                    

        //        }

        //        // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
        //        //}

        //        //add useful things here!   
        //        //}
        //    }
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    return View();
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

        
    }
}
