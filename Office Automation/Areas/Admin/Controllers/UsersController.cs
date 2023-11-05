using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Admin.Controllers
{
    
    public class UsersController : Controller
    {
        OfficeAutomationContext db = new OfficeAutomationContext();
        UserService _userService;
        RoleService _roleService;
        DepartmentService _departmentService;
        public UsersController()
        {
            _userService = new UserService(db);
            _roleService = new RoleService(db);
            _departmentService = new DepartmentService(db);
        }

        public ActionResult UsersList()
        {
            var userslist = _userService.GetAll().ToList();

            var usersListViewModel = AutoMapperConfig.mapper.Map<List<User>, List<UserViewModel>>(userslist);

            return View(usersListViewModel);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_roleService.GetAll(), "RoleId", "Title");
            ViewBag.DepartmentId = new SelectList(_departmentService.GetAll(), "DepartmentId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Family,Gender,BirthDate,PhoneNumber,PersonnelID,StartingDate,RoleId,DepartmentId,Password")] UserViewModel userViewModel, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                string newImageName = "user.png";
                if (ImageUpload != null)
                {
                    if (ImageUpload.ContentType != "image/jpeg" && ImageUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageUpload", "تصویر شما فقط باید با فرمت png یا jpg یا jpeg باشد!");
                        return View();
                    }
                    if (ImageUpload.ContentLength > 300000)
                    {
                        ModelState.AddModelError("ImageUpload", "سایز تصویر شما نباید بیشتر از 300 کیلوبایت باشد!");
                        return View();
                    }

                    newImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(ImageUpload.FileName);
                    ImageUpload.SaveAs(Server.MapPath("~/Images/" + newImageName));
                }
                userViewModel.ProfileImage = newImageName;

                var user = AutoMapperConfig.mapper.Map<UserViewModel, User>(userViewModel);
                user.RegisterDate = DateTime.Now;
                user.IsActive = true;
                _userService.Add(user);
                _userService.Save();
                return RedirectToAction("UsersList");
            }
            ViewBag.RoleId = new SelectList(db.roles, "RoleId", "Name", userViewModel.RoleId);
            ViewBag.DeparmentId = new SelectList(db.departments, "DepartmentId", "Name", userViewModel.DepartmentId);

            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _userService.GetEntity(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(db.roles, "RoleId", "Name", "Title");
            ViewBag.DepartmentId = new SelectList(db.departments, "DepartmentId", "Name", user.DepartmentId);

            var userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Family,BirthDate,Gender,RegisterDate,PhoneNumber,PersonnelID,StartingDate,Password,ProfileImage,IsActive,RoleId,DepartmentId")] UserViewModel userViewModel, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null)
                {
                    if (userViewModel.ProfileImage != "user.png")
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/") + userViewModel.ProfileImage);
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/") + userViewModel.ProfileImage);
                        userViewModel.ProfileImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    }
                    imageUpload.SaveAs(Server.MapPath("~/Images/" + userViewModel.ProfileImage));
                }
                var user = AutoMapperConfig.mapper.Map<UserViewModel, User>(userViewModel);
                _userService.Update(user);
                _userService.Save();
                return RedirectToAction("UsersList");
            }

            ViewBag.RoleId = new SelectList(db.roles, "RoleId", "Name", "Title");
            ViewBag.DeparmentId = new SelectList(db.departments, "DepartmentId", "Name", userViewModel.DepartmentId);

            return View(userViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetEntity(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(db.roles, "RoleId", "Name", user.Role);
            ViewBag.DepartmentId = new SelectList(db.departments, "DepartmentId", "Name", user.DepartmentId);
            var userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);

            return View(userViewModel);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            User user = _userService.GetEntity(id);
            _userService.Delete(user);
            _userService.Save();

            return RedirectToAction("UsersList");
        }


        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            _roleService.Dispose();
            _departmentService.Dispose();
        }
    }

}