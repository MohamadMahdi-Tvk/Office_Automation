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
using System.Web.Security;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Personnel.Controllers
{
    [Authorize(Roles = "Admin,Personnel")]
    public class DashboardController : Controller
    {

        OfficeAutomationContext db = new OfficeAutomationContext();

        UserService _userService;
        DepartmentService _departmentService;
        LetterService _letterService;
        MessageService _messageService;

        public DashboardController()
        {
            _userService = new UserService(db);
            _departmentService = new DepartmentService(db);
            _letterService = new LetterService(db);
            _messageService = new MessageService(db);
        }


        public ActionResult Index(int pageid = 1)
        {

            var personnelId = User.Identity.Name;
            int userDepartmentId = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == personnelId).DepartmentId;

            var skip = (pageid - 1) * 8;
            int count = _letterService.GetAll().Where(t => t.DepartmentId == userDepartmentId).Count();

            ViewBag.PageID = pageid;

            ViewBag.PageCount = count / 8;

            ViewBag.departmentNames = _departmentService.GetAll().Select(t => t.Name).ToArray();

            var letter = _letterService.GetAll().Where(t => t.DepartmentId == userDepartmentId).OrderByDescending(t => t.SendDate).Skip(skip).Take(8).ToList();

            var letterViewModel = AutoMapperConfig.mapper.Map<List<Letter>, List<LetterViewModel>>(letter);

            return View(letterViewModel);
        }

        public ActionResult ShowAllLetters(int pageid = 1)
        {
            var skip = (pageid - 1) * 8;
            int count = _letterService.GetAll().Count();
            ViewBag.PageID = pageid;
            ViewBag.PageCount = count / 8;
            ViewBag.departmentNames = _departmentService.GetAll().Select(t => t.Name).ToArray();

            var letter = _letterService.GetAll().OrderByDescending(t => t.SendDate).Skip(skip).Take(8).ToList();

            var letterViewModel = AutoMapperConfig.mapper.Map<List<Letter>, List<LetterViewModel>>(letter);

            return View(letterViewModel);
        }

        public ActionResult ShowSendLetters(int pageid = 1)
        {
            var skip = (pageid - 1) * 8;

            ViewBag.PageID = pageid;

            ViewBag.departmentNames = _departmentService.GetAll().Select(t => t.Name).ToArray();

            var personnelId = User.Identity.Name;
            int userDepartmentId = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == personnelId).DepartmentId;

            var letter = _letterService.GetAll().Where(t => t.SendDepartmentId == userDepartmentId).OrderByDescending(t => t.SendDate).Skip(skip).Take(8).ToList();
            int count = _letterService.GetAll().Where(t => t.SendDepartmentId == userDepartmentId).Count();
            ViewBag.PageCount = count / 8;
            var letterViewModel = AutoMapperConfig.mapper.Map<List<Letter>, List<LetterViewModel>>(letter);

            return View(letterViewModel);
        }

        public ActionResult ShowUnreadLetters(int pageid = 1)
        {
            var personnelId = User.Identity.Name;
            int userDepartmentId = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == personnelId).DepartmentId;

            var skip = (pageid - 1) * 8;
            int count = _letterService.GetAll().Where(t => t.DepartmentId == userDepartmentId).Count();

            ViewBag.PageID = pageid;
            ViewBag.PageCount = count / 8;
            ViewBag.departmentNames = _departmentService.GetAll().Select(t => t.Name).ToArray();


            var letter = _letterService.GetAll().Where(t => t.DepartmentId == userDepartmentId && t.LetterVisit == false).OrderByDescending(t => t.SendDate).Skip(skip).Take(8).ToList();

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
            var personnelId = User.Identity.Name;
            int userDepartmentId = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == personnelId).DepartmentId;
            int userId = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == personnelId).UserId;
            ViewBag.letterCount = _letterService.GetAll().Where(t => t.DepartmentId == userDepartmentId).Count();
            ViewBag.letterCountAll = _letterService.GetAll().Count();
            ViewBag.letterSendCount = _letterService.GetAll().Where(t => t.SendDepartmentId == userDepartmentId).Count();
            ViewBag.UnreadLettersCount = _letterService.GetAll().Where(t => t.LetterVisit == false && t.DepartmentId == userDepartmentId).Count();
            ViewBag.SavedLettersCount = _letterService.GetAll().Where(t => t.LetterSave == true).Count();
            ViewBag.PrivateMessageCount = _messageService.GetAll().Where(t => t.UserId == userId).Count();

            ViewBag.Admin = false;

            var loginId = User.Identity.Name;

            var loginAdmin = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == loginId).RoleId;


            if (loginAdmin == 1)
            {
                ViewBag.Admin = true;
            }

            return PartialView();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}