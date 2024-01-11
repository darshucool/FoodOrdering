using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MIMS.Helpers;
using MIMS.Models;
using MIMS.Security;
using AlfasiWeb.Util;
using Dinota.Core.Specification;
using Dinota.Domain;
using Dinota.Domain.AdminUser;
using Dinota.Domain.User;
using Dinota.Security;
using Dinota.Security.Crypto;
using Dinota.Models;
using Dinota.Domain.PageObject;
using Dinota.Domain.UserPermission;
using Dinota.Domain.UserType;
using System.Collections.Generic;
using Dinota.Domain.Division;
using Dinota.Domain.BarRecovery;

namespace MIMS.Controllers
{
    [ExternalUserRedirectAttribute]
    public class UsersController : BaseController
    {
        private readonly UserBaseService _userService;
        private readonly AdminUserService _adminUserService;
        private readonly UserPermissionService _userPermissionService;
        private readonly PageObjectService _pageObjectService;
        private readonly UserAccountService _userAccountService;
        private readonly UserTypeService _userTypeService;
        private readonly DivisionService _divisionService;
        private readonly BarRecoveryService _barRecoveryService;

        private readonly ICryptoProvider _cryptoProvider;

        public UsersController(UserBaseService userService, DivisionService divisionService, UserTypeService userTypeService, 
            PageObjectService pageObjectService, UserPermissionService userPermissionService, AdminUserService adminUserService, 
            UserAccountService userAccountService, BarRecoveryService barRecoveryService,
            IDomainContext domainContext, ICryptoProvider cryptoProvider)
            : base(domainContext)
        {
            _userService = userService;
            _adminUserService = adminUserService;           
            _userAccountService = userAccountService;
            _cryptoProvider = cryptoProvider;
            _userPermissionService = userPermissionService;
            _pageObjectService = pageObjectService;
            _userTypeService = userTypeService;
            _divisionService = divisionService;
           
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

        [AuthorizeUserAccessLevel()]
        public ActionResult AdminUserList()
        {
            List<UserAccount> UserAccountList = new List<UserAccount>();
            try
            {
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true);
                UserAccountList = _userAccountService.GetCollection(filter, p => p.CreationDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(UserAccountList);
        }

        public ActionResult UserList()
        {
            List<UserAccount> UserAccountList = new List<UserAccount>();
            try
            {
                UserAccount account = GetCurrentUser();
                var filter = _userAccountService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true).And(p => p.LocationUId == account.LocationUId);
                UserAccountList = _userAccountService.GetCollection(filter, p => p.CreationDate).ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(UserAccountList);
        }
        [DinotaAuthorize(FunctionalAreas.Users, SetPermission = true)]
        public ActionResult Index(UserSearchModel userSearchModel, byte typeenum = 1)
        {
            if (string.IsNullOrWhiteSpace(userSearchModel.Column))
            {
                userSearchModel.Column = "UserName";
            }

            TempData[ViewDataKeys.UserTypeEnum] = typeenum;

            LoadDataWithCurrentUser(typeenum, userSearchModel);

            return  View(userSearchModel);
            
        }

        [DinotaAuthorize(FunctionalAreas.Users, SetPermission = true)]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LoadUserList(UserSearchModel userSearchModel, byte typeenum)
        {

            if (string.IsNullOrWhiteSpace(userSearchModel.Column))
            {
                userSearchModel.Column = "UserName";
            }

            TempData[ViewDataKeys.UserTypeEnum] = typeenum;

            LoadDataWithCurrentUser(typeenum, userSearchModel);

            return PartialView(userSearchModel);
        }

        private void LoadDataWithCurrentUser(byte typeenum, UserSearchModel userSearchModel)
        {
            if (typeenum == 1)
            {
                var filter = _userAccountService.GetDefaultSpecification();
               // ViewData[ViewDataKeys.List] = _userAccountService.GetPagination(filter, userSearchModel);
            }
            if (typeenum == 2)
            {
               

            }

            if (typeenum == 3)
            {
                

            }

            if (typeenum == 4)
            {
            
            }

            if (typeenum == 5)
            {
              

            }

            if (typeenum == 6)
            {
             

            }

            ViewData[ViewDataKeys.CurrentUser] = MembershipUser.UserName;
        }

        [OutputCache(Duration = 0)]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult Create( )
        {
            return PartialView();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult Create(RegisterModel registerModel)
        {
           
            if (ModelState.IsValid)
            {
                byte usertypeenum = (byte)TempData[ViewDataKeys.UserTypeEnum];
                try
                {
                    switch (usertypeenum)
                    {
                        case 1:
                            CreateUserAccount(registerModel, _userAccountService);
                            break;
                        case 2:
                           
                            break;
                        case 3:
                            
                            break;
                        case 4:
                          
                            break;
                        case 5:
                          
                            break;
                        case 6:
                            
                            break;
                    }
                    
                    //ViewData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityCreateSuccess, "Record"));

                    return RedirectToAction("Index", "users", new { typeenum = usertypeenum });
                 
                    
                }
                catch (System.Data.DataException de)
                {
                    ExceptionTranslator.Trnaslate(de, ModelState);
                    ViewData[ViewDataKeys.Message] = new FailMessage(string.Format(ModelState.LastOrDefault().Value.Errors[0].ErrorMessage, "Record"));
                    return RedirectToAction("Index", "users", new { typeenum = usertypeenum });
                }
            }

            return PartialView();
        }

        private void CreateUserAccount(RegisterModel registerModel,object service)
        {
            if(service is UserAccountService)
            {
              var user = ((UserAccountService)service).Create();
              user.UserName = registerModel.UserName;
              user.Name = registerModel.Name;
              user.Telephone1 = registerModel.Telephone1;
              user.Email = registerModel.Email;
              user.PasswordHash = _cryptoProvider.HashPassword(registerModel.Password);
              ((UserAccountService)service).Add(user); 
            }

            DataContext.SaveChanges();
        }


        [OutputCache(Duration = 0)]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult Edit(int id)
        {
            var filter = _userService.GetDefaultSpecification();
            filter = filter.And(user => user.Id == id);

            var theUser = _userService.GetBy(filter);
            
            return PartialView(theUser);
        }

        [HttpPost]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult Edit(int id, FormCollection collection)
        {

            byte usertypeenum = (byte)TempData[ViewDataKeys.UserTypeEnum];
            try
            {
                var filter = _userService.GetDefaultSpecification();
                filter = filter.And(user => user.Id == id);

                var theUser = _userService.GetBy(filter);

                if (theUser == null)
                {
                    return RedirectToAction("Index","users");
                }

                UpdateModel(theUser);

                DataContext.SaveChanges();

               // ViewData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityEditSuccess, "Record"));

                return RedirectToAction("Index", "users", new { typeenum = usertypeenum });
           
            }
            catch (System.Data.DataException de)
            {
                ExceptionTranslator.Trnaslate(de, ModelState);
                ViewData[ViewDataKeys.Message] = new FailMessage(string.Format(ModelState.LastOrDefault().Value.Errors[0].ErrorMessage, "Record"));
                return RedirectToAction("Index", "users", new { typeenum = usertypeenum });
             
            }
        }

        //
        // GET: /Groups/Delete/5
        [OutputCache(Duration = 0)]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult Delete(int id)
        {
            var user = _userService.GetByKey(id);
            TempData[ViewDataKeys.UserId] = id;
            var resetModel = new ResetPasswordModel { UserName = user.UserName };
            return PartialView("Delete", resetModel);
        }

        [HttpPost]
        [OutputCache(Duration = 0)]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                byte usertypeenum = (byte)TempData[ViewDataKeys.UserTypeEnum];

                var filter = _userService.GetDefaultSpecification();
                filter = filter.And(user => user.Id == id);

                var theUser = _userService.GetBy(filter);

                if (theUser == null)
                {
                    //ViewData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.EntityNotFound, "Record"));

                    return RedirectToAction("Index");
                }
                _userService.Delete(theUser);
                DataContext.SaveChanges();

                //ViewData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityDeleteSuccess, "Record"));

                Cache.Remove(CacheKeys.Suppliers);
                Cache.Remove(CacheKeys.Fabricators);
                Cache.Remove(CacheKeys.Erectors);
                Cache.Remove(CacheKeys.Transporters);

                return RedirectToAction("Index", "users", new { typeenum = usertypeenum });
             
            }
            catch (System.Data.DataException de)
            {
                ExceptionTranslator.Trnaslate(de, ModelState);

                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Users/ResetPassword
        [OutputCache(Duration = 0)]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult ResetPassword(int id)
        {
            var user = _userService.GetByKey(id);

            if (user == null)
            {
                //ViewData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.EntityNotFound, "Record"));

                return RedirectToAction("Index");
            }
            TempData[ViewDataKeys.UserId] = id;
            var resetModel = new ResetPasswordModel { UserName = user.UserName };

            return PartialView("ResetPassword",resetModel);
        }

        //
        // POST: /Users/ResetPassword
        [HttpPost]
        [OutputCache(Duration = 0)]
        [DinotaAuthorize(FunctionalAreas.Users, Writable = true)]
        public ActionResult ResetPasswordProcess(int id, ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte usertypeenum = (byte)TempData[ViewDataKeys.UserTypeEnum];
                    var user = _userService.GetByKey(id);

                    if (user == null)
                    {
                       // ViewData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.EntityNotFound, "Record"));

                        return RedirectToAction("Index");
                    }

                    user.PasswordHash = _cryptoProvider.HashPassword(resetPasswordModel.NewPassword);

                    DataContext.SaveChanges();

                    ViewData[ViewDataKeys.Message] = new SuccessMessage("Password reset successful.");

                    return RedirectToAction("Index", "users", new { typeenum = usertypeenum });
                }
                catch (System.Data.DataException de)
                {
                    ExceptionTranslator.Trnaslate(de, ModelState);

                    return RedirectToAction("Index");
                }
            }

            return  RedirectToAction("Index");
        }

        [OutputCache(Duration = 0)]
        public ActionResult Details(int id)
        {
             TempData.Keep(ViewDataKeys.DetailPermission);
            var filter = _userService.GetDefaultSpecification();
            filter = filter.And(item => item.Id == id);

            var theUser = _userService.GetBy(filter);

            if (theUser == null)
            {
                //ViewData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.EntityNotFound, "User"));

                return View("Index");
            }

            return PartialView(theUser);
        }

        public JsonResult IsUsernameAvailable(string username)
        {
            var filter = _userService.GetDefaultSpecification();
            filter = filter.And(project => project.UserName.Equals(username));
            var result = _userService.GetBy(filter);

            if (result == null)
                return Json(true, JsonRequestBehavior.AllowGet);

            string suggestedName = String.Format(CultureInfo.InvariantCulture, "{0} is already exist.", username);
            return Json(suggestedName, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAccessLevel()]
        public ActionResult PermissionEdit(int id)
        {
            PermissionModel model = new PermissionModel();
            try
            {

                model.UserTypeId = id;
                model.UserType = _userTypeService.GetByKey(id);
                //model.DivisionName = info.Name;
                var filter = _pageObjectService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true);
                List<PageObject> objectList = _pageObjectService.GetCollection(filter, p => p.CreationDate).ToList();
                var filterP = _pageObjectService.GetDefaultSpecification();
                filterP = filterP.And(p => p.Active == true).And(p => p.ParentUId == 0);
                List<PageObject> ParentobjectList = _pageObjectService.GetCollection(filterP, p => p.CreationDate).ToList();
                model.ParentObjectList = ParentobjectList;
                model.PageObjectList = objectList;
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

            return View(model);
        }
        [HttpPost]
        //[AuthorizeUserAccessLevel()]
        public ActionResult PermissionEdit(FormCollection collection, int id)
        {
            PermissionModel permissionModel = new PermissionModel();
            UserType type = _userTypeService.GetByKey(id);
            List<UserPermission> permissionGroupList = new List<UserPermission>();
            try
            {
                //User user = GetCurrentUser();
                #region Collect permission object
                var filter = _pageObjectService.GetDefaultSpecification();
                filter = filter.And(p => p.Active == true);
                List<PageObject> objectList = _pageObjectService.GetCollection(filter, p => p.CreationDate).ToList();

                foreach (PageObject f in objectList)
                {
                    UserPermission permission = new UserPermission();
                    permission.ObjectId = f.UId;
                    permission.UserTypeUId = id;
                    string read = collection["chkPermitted" + f.UId];
                    if (string.IsNullOrEmpty(read) == false && read != "false")
                    {
                        permission.IsPermitted = true;
                    }
                    permission.Active = true;
                    permissionGroupList.Add(permission);
                }

                #endregion

                foreach (UserPermission up in permissionGroupList)
                {
                    var filterP = _userPermissionService.GetDefaultSpecification();
                    filterP = filterP.And(p => p.UserTypeUId == id).And(p => p.ObjectId == up.ObjectId);
                    UserPermission permissionGroup = _userPermissionService.GetBy(filterP);
                    if (permissionGroup != null) // already exist
                    {
                        permissionGroup.IsPermitted = up.IsPermitted;
                        DataContext.SaveChanges();
                    }
                    else
                    {
                        permissionGroup = new UserPermission();
                        permissionGroup.ObjectId = up.ObjectId;
                        permissionGroup.IsPermitted = up.IsPermitted;
                        permissionGroup.Active = true;
                        permissionGroup.UserTypeUId = id;
                        _userPermissionService.Add(permissionGroup);
                        DataContext.SaveChanges();
                    }

                }
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Updated");

            }
            catch (Exception ex)
            {

                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message);
            }
            return RedirectToAction("UserRoleList", "DivisionInfo", new { id = type.DivisionId});
        }
        [AuthorizeUserAccessLevel()]
        public ActionResult RegisterMember()
        {
            UserAccount oUserAccount = new UserAccount();
            //BindAppointmentList();
            BindDivisionList();
            BindUserTypeList();
            return View(oUserAccount);
        }
        [HttpPost]
        public ActionResult RegisterMember(FormCollection Form)
        {
            UserAccount oUserAccount = new UserAccount();
            UserAccount checkUserAccount = new UserAccount();
            try
            {
                TryUpdateModel(oUserAccount);
                var filterU = _userAccountService.GetDefaultSpecification();
                filterU = filterU.And(p => p.Active == true).And(p => p.UserName == oUserAccount.UserName);
                checkUserAccount = _userAccountService.GetBy(filterU);
                if (checkUserAccount != null)
                {
                    TempData[ViewDataKeys.Message] = new FailMessage("User account already exists");
                    return View(oUserAccount);
                }
                if (ModelState.IsValid)
                {
                    
                    oUserAccount.Active = true;
                    oUserAccount.PasswordHash = _cryptoProvider.HashPassword("password");
                    _userAccountService.Add(oUserAccount);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Created.");
                    return RedirectToAction("UserList");
                }
            }
            catch (Exception)
            {

                throw;
            }


            return View(oUserAccount);
        }
        [HttpPost]
        public ActionResult ShowUserType(int DivisionVal)
        {
            var filter = _userTypeService.GetDefaultSpecification();
            filter = filter.And(p => p.Active == true).And(p => p.DivisionId == DivisionVal);
            List<UserType> lst = _userTypeService.GetCollection(filter, p => p.CreationDate).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);

        }
        public void BindDivisionList()
        {
            try
            {
                var filter = _divisionService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _divisionService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Name");
                ViewData[ViewDataKeys.DivisionList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        public void BindUserTypeList()
        {
            try
            {
                var filter = _userTypeService.GetDefaultSpecification().And(s => s.Active == true);
                var TypeList = _userTypeService.GetCollection(filter, d => d.UId);
                SelectList list = new SelectList(TypeList, "UId", "Name");
                ViewData[ViewDataKeys.UserTypeList] = list;
            }
            catch (Exception ex)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(ex.Message.ToString());

            }

        }
        
        [AuthorizeUserAccessLevel()]
        public ActionResult EditMember(int id)
        {
            UserAccount account = new UserAccount();
            try
            {
                BindDivisionList();
                BindUserTypeList();
                account = _userAccountService.GetByKey(id);

            }
            catch (Exception)
            {

                throw;
            }
            return View(account);
        }
        [HttpPost]
        public ActionResult EditMember(FormCollection Form, int id)
        {
            UserAccount account = new UserAccount();
            try
            {
                BindDivisionList();
                BindUserTypeList();
                account = _userAccountService.GetByKey(id);
                TryUpdateModel(account);
                if (ModelState.IsValid)
                {
                    
                }
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully Updated.");
                return RedirectToAction("UserList");
            }
            catch (Exception)
            {

                throw;
            }
            return View(account);
        }
    }
}