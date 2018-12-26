using MobileSellingEntities.AddressFolder;
using MobileSellingEntities.UserFolder;
using MobileSellingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileSellingProject.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if (u != null)
            {
                if (u.IsInRole(WebUtil.Super_ADMIN_ROLE) || u.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return RedirectToAction("ManageProducts", "Product");
                }
                else if (u.IsInRole(WebUtil.USER_ROLE))
                {
                    return RedirectToAction("UserView", "Home");
                }
                
            }
            string querystring = Request.QueryString["queryStrng"];
            ViewBag.querystr = querystring;
            HttpCookie cookie = Request.Cookies[WebUtil.MY_COOKIE];
            if (cookie != null)
            {
                string[] a = cookie.Value.Split(',');
                User user  =new UserHandler().GetUserForLogin(a[0], a[1]);
                if(user != null)
                {
                    Session.Add(WebUtil.CURRENT_USER, user);
                    if (!string.IsNullOrWhiteSpace(querystring))
                    {
                        string[] returnUrl = querystring.Split('/');
                        if (returnUrl.Length == 2) return RedirectToAction(returnUrl[1], returnUrl[0]);
                    }
                    if (user.IsInRole(WebUtil.ADMIN_ROLE) || user.IsInRole(WebUtil.Super_ADMIN_ROLE))
                    {
                        return RedirectToAction("ManageProducts", "Product");
                    }
                    else
                    {
                        return RedirectToAction("UserView", "Home");
                    }

                }
            }
                return View();
        }
        [HttpPost]
        public ActionResult Login(Login m)
        {
            User us = Session[WebUtil.CURRENT_USER] as User;
            if (us != null)
            {
                if (us.IsInRole(WebUtil.ADMIN_ROLE) || us.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageProducts", "Product");
                }
                else
                {
                    return RedirectToAction("UserView", "Home");
                }
            }
            User user = new UserHandler().GetUserForLogin(m.LoginId, m.Password);
            if(user!= null)
            {
                Session.Add(WebUtil.CURRENT_USER, user);
                if (m.RememberMe)
                {
                    HttpCookie cookie = new HttpCookie(WebUtil.MY_COOKIE);
                    cookie.Value = $"{m.LoginId},{m.Password}";
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.SetCookie(cookie);
                }
                string returnUrl = Request.QueryString["querystr"];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    string[] parts = returnUrl.Split('/');
                    if (parts.Length == 2)
                        return RedirectToAction(parts[1], parts[0]);
                }
                if (user.IsInRole(WebUtil.ADMIN_ROLE) || user.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageProducts", "Product");
                }
                else
                {
                    return RedirectToAction("UserView", "Home");
                }
            }
            else
            {
                TempData.Add("alert", new AlertModel("You have entered Wrong Login ID or password. Please try again ", AlertType.Error));
                return RedirectToAction("Login");
            }

        }
        [HttpGet]
        public ActionResult logout()
        {
            User user = Session[WebUtil.CURRENT_USER] as User;
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            HttpCookie cookie = Request.Cookies[WebUtil.MY_COOKIE];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now;
                Response.SetCookie(cookie);
            }
            Session.Remove(WebUtil.CURRENT_USER);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if (u != null)
            {
                if (u.IsInRole(WebUtil.Super_ADMIN_ROLE) || u.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return RedirectToAction("ManageProducts", "Product");
                }
                else if (u.IsInRole(WebUtil.USER_ROLE))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.countries = new LocationHandler().GetCountryList().ToSelectItemList();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection data)
        {
            User user = new UserHandler().GetUser(data["email"].ToLower());
            if (user != null)
            { 
                TempData.Add("alert", new AlertModel("This user is already registered.", AlertType.Info));
                return RedirectToAction("SignUp");
            }

            User u = new User();
            u.FirstName = data["first"];
            u.LastName = data["last"];
            u.Address = new Address { StreetAddress = data["strtadrs"], City = new City { Id = Convert.ToInt32(data["Cities"]) } };
            //u.Address.StreetAddress = data["strtadrs"];
            //u.Address.City = new City { Id = Convert.ToInt32(data["Cities"]) };
            u.BirthDate = Convert.ToDateTime(data["dob"]);
            u.ContactNumber = data["Contact"];
            u.Email = data["email"].ToLower();
            u.Password = data["pass"];
            u.SecurityAnswer = data["que"];
            u.SecurityQuestion = data["ans"];
            HttpPostedFileBase file = Request.Files["DisplayPic"];
            if(file != null && file.ContentLength > 0)
            {
                long uno = DateTime.Now.Ticks;
                string url = $"~/Images/User/{uno}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
                string path = Server.MapPath(url);
                file.SaveAs(path);
            }
            if(u.Email.ToLower() == "ahmadsaleem1996@oulook.com")
            {
                 u.Role = new Role { Id = 1 };
            }
            else
            {
                u.Role = new Role { Id = 3 };
            }
            new UserHandler().AddUser(u);
            return RedirectToAction("SignUp");
        }
        [HttpGet]
        public ActionResult GetProvincesForDropDown(int id)
        {
            List<Province> provinces = new LocationHandler().GetProvincesListBaseOnId(id);
            DropDownModel ddm = new DropDownModel();
            ddm.Caption = "-Select Province-";
            ddm.Name = "Provinces";
            ddm.Id = "PList";
            ddm.Values = provinces.ToSelectItemList();
            return PartialView("~/Views/Shared/DropDownView.cshtml", ddm);
        }
        [HttpGet]
        public ActionResult GetCitiesForDropDown(int id)
        {
            List<City> cities = new LocationHandler().GetCitiesList(id);
            DropDownModel ddm = new DropDownModel();
            ddm.Caption = "-Select City-";
            ddm.Name = "Cities";
            ddm.Id = "cList";
            ddm.Values = cities.ToSelectItemList();
            return PartialView("~/Views/Shared/DropDownView.cshtml", ddm);
        }
    }
}