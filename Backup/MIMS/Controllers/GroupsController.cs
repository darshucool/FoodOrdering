using System.Linq;
using System.Web.Mvc;
using MIMS.Helpers;
using AlfasiWeb.Properties;
using MIMS.Security;
using MIMS.Models;
using Dinota.Core.Extensions;
using Dinota.Core.Specification;
using Dinota.Security;
using Dinota.Security.FunctionalArea;
using Dinota.Security.Group;
using Dinota.Security.Role;

namespace MIMS.Controllers
{
    [ExternalUserRedirectAttribute]
    public class GroupsController : BaseController
    {
        private readonly GroupService _groupService;
        private readonly RoleService _roleService;
        private readonly FunctionalAreaService _functionalAreaService;

        public GroupsController(GroupService groupService, RoleService roleService, FunctionalAreaService functionalAreaService, ISecurityContext securityContext)
            : base(securityContext)
        {
            _groupService = groupService;
            _roleService = roleService;
            _functionalAreaService = functionalAreaService;
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        public ActionResult Index(GroupSearchModel groupSearchModel)
        {
            if (string.IsNullOrWhiteSpace(groupSearchModel.Column))
            {
                groupSearchModel.Column = "Name";
            }

            var filter = _groupService.GetDefaultSpecification();

            if (!string.IsNullOrWhiteSpace(groupSearchModel.Name))
            {
                filter = filter.And(group => group.Name.StartsWith(groupSearchModel.Name));
            }

           // ViewData[ViewDataKeys.List] = _groupService.GetPagination(filter, groupSearchModel);

            return View(groupSearchModel);
        }

        //
        // GET: /Groups/Create
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Groups/Create
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var group = _groupService.Create();
                UpdateModel(group);

                _groupService.Add(group);
                DataContext.SaveChanges();

                TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityCreateSuccess, "Group"));

                return RedirectToAction("Index");
            }
            catch (System.Data.DataException de)
            {
                ExceptionTranslator.Trnaslate(de, ModelState);

                return View();
            }
        }

        //
        // GET: /Groups/Edit/5
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        public ActionResult Edit(int id)
        {
            var filter = _groupService.GetDefaultSpecification();
            filter = filter.And(group => group.Id == id);

            var theGroup = _groupService.GetBy(filter);

            if (theGroup == null)
            {
                TempData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.EntityNotFound, "Group"));

                return RedirectToAction("Index");
            }

            return View(theGroup);
        }

        //
        // POST: /Groups/Edit/5
        [HttpPost]
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var filter = _groupService.GetDefaultSpecification();
                filter = filter.And(group => group.Id == id);

                var theGroup = _groupService.GetBy(filter);

                if (theGroup == null)
                {
                    return RedirectToAction("Index");
                }

                UpdateModel(theGroup);

                DataContext.SaveChanges();

                TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityEditSuccess, "Group"));

                return RedirectToAction("Index");
            }
            catch (System.Data.DataException de)
            {
                ExceptionTranslator.Trnaslate(de, ModelState);

                return View();
            }
        }

        //
        // GET: /Groups/Delete/5
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        public ActionResult Delete(int id)
        {
            return PartialView("Delete", id);
        }

        [HttpPost]
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var filter = _groupService.GetDefaultSpecification();
                filter = filter.And(group => group.Id == id);

                var theGroup = _groupService.GetBy(filter);

                if (theGroup == null)
                {
                    TempData[ViewDataKeys.Message] = new FailMessage(string.Format(Resources.EntityNotFound, "Group"));

                    return RedirectToAction("Index");
                }
                _groupService.Delete(theGroup);
                DataContext.SaveChanges();

                TempData[ViewDataKeys.Message] = new SuccessMessage(string.Format(Resources.EntityDeleteSuccess, "Group"));

                return RedirectToAction("Index");
            }
            catch (System.Data.DataException de)
            {
                ExceptionTranslator.Trnaslate(de, ModelState);

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Load the roles and assigned permissions 
        /// </summary>
        /// <param name="id">id of the group</param>
        /// <param name="listModel">list model to get pagination</param>
        /// <returns>partial view</returns>
        [DinotaAuthorize(FunctionalAreas.UserGroups, SetPermission = true)]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Roles(int id, ListModel listModel)
        {
            var filter = _groupService.GetDefaultSpecification();
            filter = filter.And(group => group.Id == id);

            _groupService.Include(group => group.Roles);

            var theGroup = _groupService.GetBy(filter);

            ViewData[ViewDataKeys.Group] = theGroup;

            if (listModel == null)
            {
                listModel = new ListModel { Column = "Name" };
            }
            else if (listModel.Column.IsNullOrEmpty())
            {
                listModel.Column = "Name";
            }

           // ViewData[ViewDataKeys.List] = _functionalAreaService.GetPagination(listModel);
            return PartialView("Roles", listModel);
        }

        /// <summary>
        /// Set the role write/read/none for the given group and functional area
        /// </summary>
        /// <param name="groupId">id of the group</param>
        /// <param name="funcArea">id of the functional area</param>
        /// <param name="roleType">type of the role. here it's a mapping enum value write/read/none</param>
        /// <returns>content results saying status of the assignment</returns>
        [DinotaAuthorize(FunctionalAreas.UserGroups, Writable = true)]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SetRole(int groupId, int funcArea, int? roleType)
        {
            var filter = _groupService.GetDefaultSpecification();
            filter = filter.And(group => group.Id == groupId);

            _groupService.Include(group => group.Roles);

            var theGroup = _groupService.GetBy(filter);

            if (theGroup != null)
            {
                var roles = theGroup.Roles.Where(role => role.FunctionalAreaId == funcArea).ToList();
                foreach (var role in roles)
                {
                    theGroup.Roles.Remove(role);
                }

                if (roleType != null)
                {
                    if (roleType.Value == (int)RoleTypeEnum.Readable)
                    {
                        theGroup.Roles.Add(_roleService.GetReadableRole(funcArea));
                    }

                    if (roleType.Value == (int)RoleTypeEnum.Writable)
                    {
                        var rolesToAssign = _roleService.GetRoles(funcArea);
                        foreach (var role in rolesToAssign)
                        {
                            theGroup.Roles.Add(role);
                        }
                    }
                }

                DataContext.SaveChanges();
                return Content(Resources.PermissionsChangedSuccessfully);
            }
            return Content(Resources.PermissionsChangeFailed);
        }


    }
}
