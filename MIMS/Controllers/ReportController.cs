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

namespace MIMS.Controllers
{
    //[Authorize]
   // [ExternalUserRedirectAttribute]
    public class ReportController : BaseController
    {
        private readonly DivisionService _divisionService;
        private readonly UserTypeService _userTypeService;
        private readonly DistrictService _districtService;
        private readonly SLAFLocationService _SLAFLocationService;
        private readonly FuelTypeService _fuelTypeService;

        public ReportController(IDomainContext dataContext, SLAFLocationService SLAFLocationService, FuelTypeService fuelTypeService, DistrictService districtService, UserTypeService userTypeService, DivisionService divisionService)
            : base(dataContext)
        {
            _divisionService = divisionService;
            _userTypeService = userTypeService;
            _districtService = districtService;
            _fuelTypeService = fuelTypeService;
            _SLAFLocationService = SLAFLocationService;
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
            return View();
        }
        [HttpPost]
        public ActionResult DailySalesSummary(FormCollection Form)
        {
            return View();
        }
    }
}
