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

        public ActionResult Index()
        {
            return View();
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
                var admin = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == adminLoginViewModel.PersonnelID && t.Password == adminLoginViewModel.Password && t.RoleId == 1);
                if (admin != null)
                {
                    FormsAuthentication.SetAuthCookie(adminLoginViewModel.PersonnelID, false);

                    if (adminLoginViewModel.ReturnUrl == null)
                    {
                        adminLoginViewModel.ReturnUrl = "/";
                    }

                    return Redirect(adminLoginViewModel.ReturnUrl);

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


        [HttpGet]
        public ActionResult PersonnelLogin(string returnUrl = "/")
        {
            PersonnelLoginViewModel personnelLoginViewModel = new PersonnelLoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(personnelLoginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonnelLogin([Bind(Include = "PersonnelId,Password,ReturnUrl")] PersonnelLoginViewModel personnelLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var personnel = _userService.GetAll().FirstOrDefault(t => t.PersonnelID == personnelLoginViewModel.PersonnelID && t.Password == personnelLoginViewModel.Password);

                if (personnel != null)
                {
                    if (personnel.RoleId == 2)
                    {
                        FormsAuthentication.SetAuthCookie(personnelLoginViewModel.PersonnelID, false);

                        if (personnelLoginViewModel.ReturnUrl == null)
                        {
                            personnelLoginViewModel.ReturnUrl = "/";
                        }

                        return Redirect(personnelLoginViewModel.ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "شما مجاز به ورود به این حساب نیستید");
                    }
                   
                }
                ModelState.AddModelError("Password", "شماره پرسنلی و یا رمز عبور شما صحیح نیست");
                return View(personnelLoginViewModel);
            }

            return View(personnelLoginViewModel);
        }

    }
}