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
using Dinota.Domain.MenuItem;
using Dinota.Domain.MenuOrder;

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class MenuController : BaseController
    {
        private readonly UserAccountService _userAccountService;
        private readonly MenuCategoryService _menuCategoryService;
        private readonly MenuItemService _menuItemService;
        private readonly MenuOrderService _menuOrderService;
        public MenuController(IDomainContext dataContext, MenuOrderService menuOrderService, MenuItemService menuItemService, MenuCategoryService menuCategoryService, UserAccountService userAccountService)
            : base(dataContext)
        {
            _userAccountService = userAccountService;
            _menuCategoryService = menuCategoryService;
            _menuItemService = menuItemService;
            _menuOrderService = menuOrderService;
        }
        //[AuthorizeUserAccessLevel()]
        public ActionResult MenuItemIndex(int id)
        {
            MenuModel model = new MenuModel();
            model.oMenuCategory = _menuCategoryService.GetByKey(id);
            List<MenuItemModel> MenuItemModelList=new List<MenuItemModel>();
            var filter = _menuItemService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p => p.MenuCategoryUId == id);
            List<MenuItem> MenuItemList= _menuItemService.GetCollection(filter, p => p.CreationDate).ToList();
            foreach(MenuItem item in MenuItemList)
            {
                MenuItemModel mod=new MenuItemModel();
                mod.MenuItem=item;
                MenuItemModelList.Add(mod);
            }
            model.MenuItemModel = MenuItemModelList;
            
            return View(model);
        }
        public ActionResult MenuOrderList()
        {
            
            var filter = _menuOrderService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p => p.Status == 10);
            List<MenuOrder> MenuItemList = _menuOrderService.GetCollection(filter, p => p.CreationDate).ToList();
            
           

            return View(MenuItemList);
        }
        [HttpPost]
        public ActionResult OrderMenu(FormCollection Form,int id)
        {
            try
            {
                MenuCategory oMenuCategory = _menuCategoryService.GetByKey(id);
                int MenuItemId = 0;
                UserAccount account = GetCurrentUser();
                if (account == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (oMenuCategory.MenuTypeId == 1)
                {
                    MenuOrder oMenuOrder = new MenuOrder();
                    if (Form["menuitemid"] != null)
                     MenuItemId = int.Parse(Form["menuitemid"].ToString());
                    if (Form["vegrad"] != null)
                        oMenuOrder.IsVeg = bool.Parse(Form["vegrad"].ToString());
                    if (Form["dietrad"] != null)
                        oMenuOrder.IsDiet = bool.Parse(Form["dietrad"].ToString());
                    if (Form["menucount"] != null)
                        oMenuOrder.Qty = int.Parse(Form["menucount"].ToString());
                    oMenuOrder.UserId = account.Id;
                    oMenuOrder.Status = 10;
                    oMenuOrder.Active = true;
                    oMenuOrder.MenuItemUId = MenuItemId;
                    _menuOrderService.Add(oMenuOrder);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully added to the Order");
                }
                return RedirectToAction("MenuItemIndex", new { id = id });
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

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public ActionResult DataTransfer(int? archive)
        //{
        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:/FuelExcel/FuelExcel/do/part2.xlsx");
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
        //            if (xlRange.Cells[i, 1].Value != null)
        //            {
        //                string v = xlRange.Cells[i, 1].Value.ToString();
        //                int Val = int.Parse(v);
        //                oVehicleInfo.SLAFLocationUId = Val;
        //            }
        //            if (xlRange.Cells[i, 2].Value != null)
        //            {
        //                oVehicleInfo.ServiceNo = xlRange.Cells[i, 2].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.ServiceNo = "";
        //            }

        //            if (xlRange.Cells[i, 3].Value != null)
        //            {
        //                int RankID = int.Parse(xlRange.Cells[i, 3].Value.ToString());
        //                oVehicleInfo.RankUId = RankID;
        //            }
        //            else
        //            {
        //                oVehicleInfo.RankUId = 0;
        //            }
        //            if (xlRange.Cells[i, 4].Value != null)
        //            {
        //                oVehicleInfo.Name = xlRange.Cells[i, 4].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.Name = "";
        //            }
        //            if (xlRange.Cells[i, 5].Value != null)
        //            {
        //                oVehicleInfo.VehicleNo = xlRange.Cells[i, 5].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.VehicleNo = "";
        //            }
        //            if (xlRange.Cells[i, 6].Value != null)
        //            {
        //                oVehicleInfo.VehicleOwnership = xlRange.Cells[i,6].Value.ToString();
        //            }
        //            else
        //            {
        //                oVehicleInfo.VehicleOwnership = "";
        //            }
                    
                 
                   
        //            if (xlRange.Cells[i,7].Value != null)
        //            {
        //                int VehicleCategoryUId = int.Parse(xlRange.Cells[i, 7].Value.ToString());
        //                oVehicleInfo.VehicleCategoryUId = VehicleCategoryUId;
        //            }
        //            else
        //            {
        //                oVehicleInfo.VehicleCategoryUId = 0;
        //            }
        //            oVehicleInfo.DistrictUId = 1;
        //            oVehicleInfo.TownUId = "RMA";
        //            oVehicleInfo.Status = 1;
        //            oVehicleInfo.IsOld = false;

        //            oVehicleInfo.Active = true;
        //            int VehicleExist = new CustomDataBaseManger().CheckVehicleSserviceNoExist(oVehicleInfo.ServiceNo);
        //            if (VehicleExist > 0)
        //            {
        //                int hasHefuelDrawwed = new CustomDataBaseManger().CheckVehicleFuelDrawExist(oVehicleInfo.ServiceNo);
        //                if (hasHefuelDrawwed == 0)
        //                {
        //                    var filter = _vehicleInfoService.GetDefaultSpecification();
        //                    filter = filter.And(p => p.Active == true).And(p=>p.ServiceNo== oVehicleInfo.ServiceNo);
        //                    List<VehicleInfo> VehicleInfoList = _vehicleInfoService.GetCollection(filter, p => p.CreationDate).ToList();
        //                    foreach(VehicleInfo inV in  VehicleInfoList)
        //                    {
        //                        VehicleInfo v = _vehicleInfoService.GetByKey(inV.UId);
        //                        v.Active = false;
        //                        DataContext.SaveChanges();
        //                    }
        //                    _vehicleInfoService.Add(oVehicleInfo);
        //                    DataContext.SaveChanges();
        //                }
        //            }
        //            else {
        //                _vehicleInfoService.Add(oVehicleInfo);
        //                DataContext.SaveChanges();
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
