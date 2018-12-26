using MobileSellingEntities.MobileShop;
using MobileSellingEntities.UserFolder;
using MobileSellingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileSellingProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult ManageProducts()
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if(u == null || u.IsInRole(WebUtil.USER_ROLE))
            {
                return RedirectToAction("Login", "User", new { queryStrng = "Product/ManageProducts" });
            }
            List<Products> products = new MobileShopHandler().GetProducts();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Departments = new MobileShopHandler().GetDepartments().ToSelectItemList();
            ViewBag.sizes = new MobileShopHandler().GetSizes().ToSelectItemList();
            ViewBag.condition = new MobileShopHandler().GetCondition().ToSelectItemList();
            ViewBag.colors = new MobileShopHandler().GetColors().ToSelectItemList();

            return PartialView("~/Views/Product/Create.cshtml");
        }
        [HttpGet]
        public ActionResult GetCategoriesToDropdown(int id)
        {
            DropDownModel model = new DropDownModel();
            model.Id = "Category";
            model.Name = "Category";
            model.Caption = "Select Category";
            model.Values = new MobileShopHandler().GetCategories(id).ToSelectItemList();
            return PartialView("~/Views/Shared/DropDownView.cshtml", model);
        }
        [HttpGet]
        public ActionResult GetSubCategoriesToDropdown(int id)
        {
            DropDownModel model = new DropDownModel();
            model.Id = "SubCategory";
            model.Name = "SubCategory";
            model.Caption = "Select SubCategory";
            model.Values = new MobileShopHandler().GetSubCategories(id).ToSelectItemList();
            return PartialView("~/Views/Shared/DropDownView.cshtml", model);
        }
        [HttpPost]
        public ActionResult Create(FormCollection data)
        {
            Products p = new Products();
            p.SubCategory = new SubCategory { Id = Convert.ToInt32(data["SubCategory"]) };
            p.Name = data["Name"];
            p.Price = Convert.ToInt64(data["Price"]);
            p.LaunchDate = Convert.ToDateTime(data["LaunchDate"]);
            string[] SizesOffered = data["SizesOffered"].Split(',');
            foreach (var s in SizesOffered)
            {
                p.SizesOffered.Add(new Sizes { Id = Convert.ToInt32(s) });
            }
            string[] Colors = data["colors"].Split(',');
            foreach (var c in Colors)
            {
                p.colors.Add(new Colors { Id = Convert.ToInt32(c) });
            }
            p.Condition = new Condition { Id = Convert.ToInt32(data["Condition"]) };
            p.Description = data["Description"];

            long uno = DateTime.Now.Ticks;
            int counter = 0;
            foreach (string f in Request.Files)
            {

                HttpPostedFileBase file = Request.Files[f];
                if (!String.IsNullOrWhiteSpace(file.FileName))
                {
                    string url = $"~/Images/Product Images/{uno}{counter++}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    file.SaveAs(path);
                    p.Images.Add(new ProductImages { Url = url, Priority = counter });
                }
            }
            new MobileShopHandler().AddProduct(p);
            return RedirectToAction("ManageProducts");

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Products p = new MobileShopHandler().GetProducts(id);

            return PartialView("~/Views/Product/Delete.cshtml", p);
        }

        [HttpPost]
        public ActionResult Delete(Products product)
        {
            new MobileShopHandler().DeleteProduct(product);
            return RedirectToAction("ManageProducts");
        }
       

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            ProductModel dep = new MobileShopHandler().GetProducts(id).ToproModel();
            return PartialView("~/Views/Product/UpdateProduct.cshtml", dep);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Products pro,FormCollection data)
        {
             new MobileShopHandler().UpdateProduct(pro);

            return RedirectToAction("ManageProducts");

        }




    }
}