using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Personnel.Controllers
{
    [Authorize(Roles = "Admin,Personnel")]
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
            letter.LetterVisit = true;
            _letterService.Update(letter);
            _letterService.Save();
            var letterViewModel = AutoMapperConfig.mapper.Map<Letter, LetterViewModel>(letter);

            ViewBag.sendDepartmentName = _departmentService.GetAll().FirstOrDefault(t => t.DepartmentId == letter.SendDepartmentId).Name;

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
        public ActionResult letterCreate([Bind(Include = "DepartmentId,Title,LetterContent,Number,Type,LetterSave")] LetterViewModel letterViewModel)
        {
            var userSend = User.Identity.Name;
            var userFind = _userService.GetAll().Single(t => t.PersonnelID == userSend);
            if (ModelState.IsValid)
            {
                var letter = AutoMapperConfig.mapper.Map<LetterViewModel, Letter>(letterViewModel);

                letter.SendDate = DateTime.Now;
                letter.SendDepartmentId = userFind.DepartmentId;
                letter.LetterVisit = false;
                _letterService.Add(letter);
                _letterService.Save();
                return Redirect("/Personnel/Dashboard/Index");

            }

            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "Name");
            ViewBag.DepartmentId = new SelectList(_departmentService.GetAll(), "DepartmentId", "Name");

            return View(letterViewModel);
        }

        public ActionResult SearchIndex()
        {
            return View();
        }

        public ActionResult SearchResult(string q, int pageid = 1)
        {
            ViewBag.Result = q;
            var skip = (pageid - 1) * 8;

            var letterFind = _letterService.SearchLetter(q).OrderByDescending(t => t.SendDate).Skip(skip).Take(8).ToList();
            int count = letterFind.Count();

            ViewBag.PageID = pageid;

            ViewBag.PageCount = count / 8;
            var letterViewModel = AutoMapperConfig.mapper.Map<List<Letter>, List<LetterViewModel>>(letterFind);

            ViewBag.departmentNames = _departmentService.GetAll().Select(t => t.Name).ToArray();
            return View(letterViewModel);
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter = _letterService.GetEntity(id.Value);

            if (letter == null)
            {
                return HttpNotFound();
            }

            var letterViewModel = AutoMapperConfig.mapper.Map<Letter, LetterViewModel>(letter);

            return View(letterViewModel);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Letter letter = _letterService.GetEntity(id);
            _letterService.Delete(letter);
            _letterService.Save();

            return Redirect("/Personnel/Dashboard/Index");
        }


        public ActionResult SaveLetter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter = _letterService.GetEntity(id.Value);

            if (letter == null)
            {
                return HttpNotFound();
            }

            letter.LetterSave = true;
            _letterService.Update(letter);
            _letterService.Save();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult UnSaveLetter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter = _letterService.GetEntity(id.Value);

            if (letter == null)
            {
                return HttpNotFound();
            }

            letter.LetterSave = false;
            _letterService.Update(letter);
            _letterService.Save();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ShowSaveLetters(int pageid = 1)
        {
            var skip = (pageid - 1) * 8;
            var letters = _letterService.GetAll().Where(t => t.LetterSave).OrderByDescending(t=>t.SendDate).Skip(skip).Take(8).ToList();

            int count = _letterService.GetAll().Where(t => t.LetterSave).Count();
            ViewBag.PageID = pageid;
            ViewBag.PageCount = count / 8;
            var lettersViewModel = AutoMapperConfig.mapper.Map<List<Letter>, List<LetterViewModel>>(letters);
            ViewBag.departmentNames = _departmentService.GetAll().Select(t => t.Name).ToArray();
            return View(lettersViewModel);
        }
    }
}