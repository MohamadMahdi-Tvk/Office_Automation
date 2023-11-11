using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Admin.Controllers
{


    public class DashboardController : Controller
    {
        OfficeAutomationContext db = new OfficeAutomationContext();

        UserService _userService;
        DepartmentService _departmentService;

        public DashboardController()
        {
            _userService = new UserService(db);
            _departmentService = new DepartmentService(db);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminInfo()
        {
            var loginId = User.Identity.Name;

            var loginAdmin = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == loginId).UserId;

            var admin = _userService.GetEntity(loginAdmin);

            var adminViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(admin);

            return PartialView(adminViewModel);
        }

        public ActionResult AdminPanel()
        {
            return PartialView();
        }

        public ActionResult GeneralInfo()
        {
            ViewBag.UserCount = _userService.GetAll().Count();
            ViewBag.UserActive = _userService.GetAll().Count(t => t.IsActive == true);
            ViewBag.UserDeactive = _userService.GetAll().Count(t => t.IsActive == false);

            ViewBag.DepartmentCount = _departmentService.GetAll().Count();
            ViewBag.DepartmentActive = _departmentService.GetAll().Count(t => t.IsActive == true);
            ViewBag.DepartmentDeactive = _departmentService.GetAll().Count(t => t.IsActive == false);

            return PartialView();
        }

    }
}