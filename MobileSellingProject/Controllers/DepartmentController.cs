using MobileSellingEntities.MobileShop;
using MobileSellingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileSellingProject.Controllers
{
    public class DepartmentController : Controller
    {
        [HttpGet]
        public ActionResult ManageDep()
        {
         List<DepartmentModel> depModel = new MobileShopHandler().GetDepartments().ToDepModelList();
            return View(depModel);
        }
        [HttpGet]
        public ActionResult CreateDep()
        {
            return PartialView("~/Views/Department/CreateDep.cshtml");
        }
        [HttpPost]
        public ActionResult CreateDep(FormCollection collection)
        {
            Department d = new Department();
            d.Name = collection["abc"];
            new MobileShopHandler().AddDep(d);

            return RedirectToAction("ManageDep");
        }
        [HttpGet]
        public ActionResult UpdateDep(int id)
        {
            DepartmentModel dep = new MobileShopHandler().GetDep(id).ToDepModel();
            return PartialView("~/Views/Department/UpdateDep.cshtml", dep);
        }
        [HttpPost]
        public ActionResult UpdateDep(Department dep)
        {
            new MobileShopHandler().updateDep(dep);

            return RedirectToAction("ManageDep");

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Department dept = new MobileShopHandler().GetDep(id);

            return PartialView("~/Views/Department/Delete.cshtml", dept);
        }

        [HttpPost]
        public ActionResult Delete(Department dept)
        {
            new MobileShopHandler().DeleteDepartment(dept);
            return RedirectToAction("ManageDep");
        }

    }
}

