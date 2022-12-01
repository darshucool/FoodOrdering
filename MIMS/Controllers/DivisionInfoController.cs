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
namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class DivisionInfoController : BaseController
    {
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        
        public DivisionInfoController(IDomainContext dataContext,  UserTypeService userTypeService, DivisionService divisionService)
            : base(dataContext)
        {
            _divisionService = divisionService;
            _userTypeService = userTypeService;
        }
        [AuthorizeUserAccessLevel()]
        public ActionResult Index()
        {
            List<Division> DivisionList = new List<Division>();
            var filter = _divisionService.GetDefaultSpecification();
            filter = filter.And(p=>p.Active==true);
            DivisionList = _divisionService.GetCollection(filter, p => p.CreationDate).ToList();
            return View(DivisionList);
        }
       [AuthorizeUserAccessLevel()]
        public ActionResult EditDivision(int id)
        {
            Division oDivision = new Division();
            oDivision = _divisionService.GetByKey(id);
            return View(oDivision);
        }
        [HttpPost]
        public ActionResult EditDivision(FormCollection Form,int id)
        {
            Division oDivision = new Division();
            oDivision = _divisionService.GetByKey(id);
            try
            {
                TryUpdateModel(oDivision);
                DataContext.SaveChanges();
                TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully updated");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(oDivision);
        }
        [AuthorizeUserAccessLevel()]
        public ActionResult RegisterDivision()
        {
            Division oDivision = new Division();
            return View(oDivision);
        }
        [HttpPost]
        public ActionResult RegisterDivision(FormCollection Form)
        {
            Division oDivision = new Division();
            
            try
            {
                TryUpdateModel(oDivision);
                if (ModelState.IsValid)
                {
                    oDivision.Active = true;
                    _divisionService.Add(oDivision);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully updated");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(oDivision);
        }
        [AuthorizeUserAccessLevel()]
        public ActionResult UserRoleList(int id)
        {
            List<UserType> UserTypeList = new List<UserType>();
            try
            {
              
                var filter = _userTypeService.GetDefaultSpecification();
                filter = filter.And(p => p.DivisionId == id).And(p=>p.Active==true);
                UserTypeList = _userTypeService.GetCollection(filter, p => p.CreationDate).ToList();
                TempData["DivisionName"] = _divisionService.GetByKey(id).Name;
                TempData["DivisionId"] = _divisionService.GetByKey(id).UId;
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(UserTypeList);
        }

        [AuthorizeUserAccessLevel()]
        public ActionResult EditUserType(int id)
        {
            UserType oUserType = new UserType();
            oUserType = _userTypeService.GetByKey(id);
            TempData["DivisionName"] = _divisionService.GetByKey(oUserType.DivisionId).Name;
            TempData["DivisionId"] = _divisionService.GetByKey(oUserType.DivisionId).UId;
            return View(oUserType);
        }
        
        [HttpPost]
        public ActionResult EditUserType(FormCollection Form, int id)
        {
            UserType oUserType = new UserType();
            oUserType = _userTypeService.GetByKey(id);
            try
            {
                TryUpdateModel(oUserType);
                TempData["DivisionName"] = _divisionService.GetByKey(oUserType.DivisionId).Name;
                TempData["DivisionId"] = _divisionService.GetByKey(oUserType.DivisionId).UId;
                if (ModelState.IsValid)
                {
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully updated");
                    return RedirectToAction("UserRoleList", "DivisionInfo", new { id = oUserType.DivisionId });
                }
                else
                {
                    return View(oUserType);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(oUserType);
        }
        [AuthorizeUserAccessLevel()]
        public ActionResult RegisterUserType(int id)
        {
            UserType oUserType = new UserType();
            oUserType.DivisionId = id;
            TempData["DivisionName"] = _divisionService.GetByKey(id).Name;
            TempData["DivisionId"] = _divisionService.GetByKey(id).UId;
            return View(oUserType);
        }
        [HttpPost]
        public ActionResult RegisterUserType(FormCollection Form)
        {
            UserType oUserType = new UserType();
            try
            {
                TryUpdateModel(oUserType);
                oUserType.DivisionId = oUserType.DivisionId;
                TempData["DivisionName"] = _divisionService.GetByKey(oUserType.DivisionId).Name;
                TempData["DivisionId"] = _divisionService.GetByKey(oUserType.DivisionId).UId;
                if (ModelState.IsValid)
                {
                    oUserType.Active = true;
                    _userTypeService.Add(oUserType);
                    DataContext.SaveChanges();
                    TempData[ViewDataKeys.Message] = new SuccessMessage("Successfully added");
                    return RedirectToAction("UserRoleList", "DivisionInfo", new { id = oUserType.DivisionId });
                }
                else
                {
                    TempData[ViewDataKeys.Message] = new FailMessage("Error Occured");
                    return View(oUserType);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
