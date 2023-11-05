using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Personnel.Controllers
{

    public class DashboardController : Controller
    {

        OfficeAutomationContext db = new OfficeAutomationContext();

        UserService _userService;
        DepartmentService _departmentService;
        LetterService _letterService;

        public DashboardController()
        {
            _userService = new UserService(db);
            _departmentService = new DepartmentService(db);
            _letterService = new LetterService(db);
        }


        public ActionResult Index()
        {
            ViewBag.DepartmentId = new SelectList(_departmentService.GetAll(), "DepartmentId", "Name");

            var letter = _letterService.GetAll().ToList();

            var letterViewModel = AutoMapperConfig.mapper.Map<List<Letter>, List<LetterViewModel>>(letter);

            

            return View(letterViewModel);
        }

        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            var loginId = User.Identity.Name;

            var loginUser = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == loginId).UserId;

            var user = _userService.GetEntity(loginUser);

            var userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);

            return PartialView(userViewModel);
        }


        [ChildActionOnly]
        public ActionResult UserPanel()
        {
            ViewBag.letterCount = _letterService.GetAll().Count();
            return PartialView();
        }
    }
}