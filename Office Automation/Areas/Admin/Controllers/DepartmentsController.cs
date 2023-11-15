using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using Office_Automation.ServiceLayer;
using Office_Automation.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using WebApplication1.App_Start;

namespace Office_Automation.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        OfficeAutomationContext db = new OfficeAutomationContext();
        DepartmentService _departmentService;

        public DepartmentsController()
        {
            _departmentService = new DepartmentService(db);
        }

        public ActionResult DepartmentList()
        {
            var department = _departmentService.GetAll().ToList();

            var departmentViewModel = AutoMapperConfig.mapper.Map<List<Department>, List<DepartmentViewModel>>(department);

            return View(departmentViewModel);
        }

        public ActionResult DepartmentInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departmet = _departmentService.GetEntity(id.Value);
            if (departmet == null)
            {
                return HttpNotFound();
            }

            var departmentViewModel = AutoMapperConfig.mapper.Map<Department, DepartmentViewModel>(departmet);
            return View(departmentViewModel);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,Info")] DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var department = AutoMapperConfig.mapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                department.IsActive = true;

                _departmentService.Add(department);
                _departmentService.Save();
                return Redirect("/Admin/Departments/DepartmentList");
            }
            return View(departmentViewModel);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = _departmentService.GetEntity(id.Value);

            if (department == null)
            {
                return HttpNotFound();
            }

            var departmentViewModel = AutoMapperConfig.mapper.Map<Department, DepartmentViewModel>(department);

            return View(departmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,Name,Info,IsActive")] DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var department = AutoMapperConfig.mapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                _departmentService.Update(department);
                _departmentService.Save();
                return Redirect("/Admin/Departments/DepartmentList");
            }

            return View(departmentViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentService.GetEntity(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            var departmentViewModel = AutoMapperConfig.mapper.Map<Department, DepartmentViewModel>(department);

            return View(departmentViewModel);
        }


        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Department department = _departmentService.GetEntity(id);
            _departmentService.Delete(department);
            _departmentService.Save();
            return RedirectToAction("DepartmentList");
        }

        protected override void Dispose(bool disposing)
        {
            _departmentService.Dispose();
        }
    }
}