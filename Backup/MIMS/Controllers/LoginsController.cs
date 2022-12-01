using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MIMS.Models;
using AlfasiWeb.Properties;
using MIMS.Security;
using MIMS.Util;
using Dinota.Core.Extensions;
using Dinota.Core.Specification;
using Dinota.Security;
using Dinota.Security.Group;
using Dinota.Security.Login;

namespace MIMS.Controllers
{
    public class LoginsController : BaseController
    {

        private readonly LoginService _loginService;

        private readonly GroupService _groupService;

        public LoginsController(LoginService loginService, GroupService groupService, ISecurityContext securityContext)
            : base(securityContext)
        {
            _loginService = loginService;
            _groupService = groupService;
        }

        //
        // GET: /Groups/
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        [OutputCache(Duration = 0)]
        public ActionResult Groups(int userId, ListModel listModel)
        {
            var filter = _loginService.GetDefaultSpecification();
            filter = filter.And(login => login.Id == userId);

            _loginService.Include(login => login.Groups);

            var theLogin = _loginService.GetBy(filter);

            ViewData[ViewDataKeys.Login] = theLogin;

            if (listModel == null)
            {
                listModel = new ListModel { Column = "Name" };
            }
            else if (listModel.Column.IsNullOrEmpty())
            {
                listModel.Column = "Name";
            }

            ViewData[ViewDataKeys.List] = _groupService.GetPagination(listModel);

            return PartialView("Groups",listModel);
        }

        //
        // GET: /RemoveFromGroup/
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult RemoveFromGroup(int loginId, int groupId)
        {
            var filter = _loginService.GetDefaultSpecification();
            filter = filter.And(login => login.Id == loginId);

            _loginService.Include(login => login.Groups);

            try
            {
                var theLogin = _loginService.GetBy(filter);

                if (theLogin.Groups.Any(group => group.Id == groupId))
                {
                    theLogin.Groups.Remove(theLogin.Groups.First(group => group.Id == groupId));

                    DataContext.SaveChanges();
                }

                return
                    Content(GetAjaxHelper().ActionLink("Add", "AddToGroup", new { loginId, groupId, rand = new Random().Next() },
                                                       new AjaxOptions()
                                                       {
                                                           OnFailure = "serverError();",
                                                           UpdateTargetId = "grp-" + groupId,
                                                           LoadingElementId = "ajax-loading"
                                                       }).ToString());
            }
            catch (Exception)
            {
                return Content(Resources.UnableToProcessRequest);
            }
        }

        //
        // GET: /AddToGroup/
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddToGroup(int loginId, int groupId)
        {
            var filter = _loginService.GetDefaultSpecification();
            filter = filter.And(login => login.Id == loginId);

            _loginService.Include(login => login.Groups);

            try
            {
                var theLogin = _loginService.GetBy(filter);

                if (!theLogin.Groups.Any(group => group.Id == groupId))
                {
                    var groupFilter = _groupService.GetDefaultSpecification();
                    groupFilter = groupFilter.And(group => group.Id == groupId);

                    var theGroup = _groupService.GetBy(groupFilter);

                    theLogin.Groups.Add(theGroup);

                    DataContext.SaveChanges();
                }

                return
                    Content(GetAjaxHelper().ActionLink("Remove", "RemoveFromGroup", new { loginId, groupId, rand = new Random().Next() },
                                                       new AjaxOptions()
                                                       {
                                                           OnFailure = "serverError();",
                                                           UpdateTargetId = "grp-" + groupId,
                                                           LoadingElementId = "ajax-loading"
                                                       }).ToString());
            }
            catch (Exception)
            {
                return Content(Resources.UnableToProcessRequest);
            }
        }

        protected HtmlHelper GetHtmlHelper()
        {
            var viewContext = new ViewContext(ControllerContext, new WebFormView(ControllerContext, "fake"), ViewData,
                                              TempData, System.IO.TextWriter.Null);
            return new HtmlHelper(viewContext, new ViewPage());
        }

        protected AjaxHelper GetAjaxHelper()
        {
            var viewContext = new ViewContext(ControllerContext, new WebFormView(ControllerContext, "fake"), ViewData,
                                              TempData, System.IO.TextWriter.Null);
            return new AjaxHelper(viewContext, new ViewPage());
        }

    }
}
