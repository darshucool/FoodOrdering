using System;
using System.Web.Mvc;
using System.Web.Security;
using MIMS.Models;
using Dinota.Core.Specification;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Security.Login;
using Dinota.Security.Tracking;
using MIMS.Helpers;

namespace MIMS.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserBaseService _userService;
        private readonly TrackingService _trackingService;
        private readonly LoginService _loginService;
        private readonly IDomainContext _domainContext;

        public AccountController(UserBaseService userService, TrackingService trackingService, LoginService loginService,
                                 IDomainContext domainContext)
            : base(domainContext)
        {
            _userService = userService;
            _trackingService = trackingService;
            _loginService = loginService;
            _domainContext = domainContext;
        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    var userFilter = _userService.GetDefaultSpecification()
                        .And(u => u.UserName == model.UserName);
                    var user = _userService.GetBy(userFilter);

                    var loginFilter = _loginService.GetDefaultSpecification();
                    loginFilter = loginFilter.And(login => login.Id == user.Id);
                    //_loginService.Include(login => login.Groups);
                    //var theLogin = _loginService.GetBy(loginFilter);

                    if (HttpContext.Session != null) HttpContext.Session.Clear();
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                   
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                    if (user.IsFirstLogin)
                    {
                        return RedirectToAction("ChangePassword", "Account", new { archive = 0 });
                    }
                    else
                    {
                        
                        return RedirectToAction("MasterMenu", "Home", new { archive = 0 });
                        
                    }
                       

                   
                }
                TempData[ViewDataKeys.Message] = new FailMessage("The username or password provided is incorrect.");
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            Session.Clear();
            using (_domainContext)
            {
                ISpecification<UserBase> validUser = _userService.GetDefaultSpecification()
                    .And(u => u.UserName == User.Identity.Name);

                UserBase user = _userService.GetBy(validUser);

                if (user != null)
                {
                    Tracking tracking = _trackingService.Create();

                    tracking.IsTrackingEnable = false;
                    tracking.User = user;
                    tracking.CreationDate = DateTime.Now;

                    tracking = _trackingService.GenerateTrackingXmlField(tracking,
                                                                         UserActivityTypeEnum.SignOff.ToString(),
                                                                         user.UserName);
                    _trackingService.Add(tracking);

                    _domainContext.SaveChanges();
                }
            }

            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Password Changed Successfully");
                    var userFilter = _userService.GetDefaultSpecification()
                        .And(u => u.UserName == User.Identity.Name);
                    var user = _userService.GetBy(userFilter);

                    user.IsFirstLogin = false;
                    DataContext.SaveChanges();
                   if (user.UserTypeId==1)
                    {

                        return RedirectToAction("MasterMenu","Home");
                    }
                    else if (user.UserTypeId ==2)
                    {

                        return RedirectToAction("MenuOrderList", "Menu");
                    }
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    TempData[ViewDataKeys.Message] = new FailMessage("The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //
        // GET: /Account/AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }


    }
}
