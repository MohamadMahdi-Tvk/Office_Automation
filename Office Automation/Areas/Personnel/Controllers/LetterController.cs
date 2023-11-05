using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Personnel.Controllers
{
    public class LetterController : Controller
    {
        OfficeAutomationContext db = new OfficeAutomationContext();

        UserService _userService;
        DepartmentService _departmentService;
        LetterService _letterService;


        public LetterController()
        {
            _userService = new UserService(db);
            _departmentService = new DepartmentService(db);
            _letterService = new LetterService(db);
        }

        public ActionResult letterInfo(int id)
        {

            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "Name");
            ViewBag.DepartmentId = new SelectList(_departmentService.GetAll(), "DepartmentId", "Name");
            var letter = _letterService.GetEntity(id);
            var letterViewModel = AutoMapperConfig.mapper.Map<Letter, LetterViewModel>(letter);
            return View(letterViewModel);
        }

        [HttpGet]
        public ActionResult letterCreate()
        {
            ViewBag.DepartmentId = new SelectList(_departmentService.GetAll(), "DepartmentId", "Name");
            var userSend = User.Identity.Name;
            var userFind = _userService.GetAll().Single(t => t.PersonnelID == userSend);

            ViewBag.UserDepartment = userFind.Department.Name;


            return View();
        }

        [HttpPost]
        public ActionResult letterCreate([Bind(Include = "DepartmentId,Title,LetterContent,Number,Type")] LetterViewModel letterViewModel)
        {
            var userSend = User.Identity.Name;
            var userFind = _userService.GetAll().Single(t => t.PersonnelID == userSend);
            if (ModelState.IsValid)
            {
                var letter = AutoMapperConfig.mapper.Map<LetterViewModel, Letter>(letterViewModel);

                letter.SendDate = DateTime.Now;
                letter.SendDepartmentId = userFind.DepartmentId;
                _letterService.Add(letter);
                _letterService.Save();
                return Redirect("/Personnel/Dashboard/Index");

            }

            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "Name");
            ViewBag.DepartmentId = new SelectList(_departmentService.GetAll(), "DepartmentId", "Name");

            return View(letterViewModel);
        }


    }
}