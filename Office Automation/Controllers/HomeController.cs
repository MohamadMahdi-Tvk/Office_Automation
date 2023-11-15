using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Office_Automation.Controllers
{
    public class HomeController : Controller
    {
        OfficeAutomationContext db = new OfficeAutomationContext();

        private UserService _userService;
        public HomeController()
        {
            _userService = new UserService(db);
        }



        [HttpGet]
        public ActionResult Login(string returnUrl = "/")
        {
            AdminLoginViewModel adminLoginViewModel = new AdminLoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(adminLoginViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "PersonnelID,Password,ReturnUrl")] AdminLoginViewModel adminLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var admin = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == adminLoginViewModel.PersonnelID && t.Password == adminLoginViewModel.Password);
                if (admin != null)
                {
                    if (admin.RoleId == 1)
                    {
                        FormsAuthentication.SetAuthCookie(adminLoginViewModel.PersonnelID, false);
                        return Redirect("/Admin/Dashboard");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(adminLoginViewModel.PersonnelID, false);
                        return Redirect("/Personnel/Dashboard");
                    }

                }
                ModelState.AddModelError("Password", "شماره پرسنلی و یا رمز عبور شما صحیح نیست");
                return View(adminLoginViewModel);
            }
            return View(adminLoginViewModel);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }



    }
}