using System;
using MobileSellingEntities.AddressFolder;
using MobileSellingProject.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MobileSellingProject.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        [HttpGet]
        public ActionResult Countries()
        {
           List<Country> c = new LocationHandler().GetCountryList();
            return View(c);
        }
        [HttpPost]
        public ActionResult Countries(FormCollection data)
        {
       

            try
            {
                Country c = new Country();
                c.Name = data["name"];
                c.Code = Convert.ToInt16(data["code"]);
                new LocationHandler().AddCountry(c);
                TempData.Add("alert", new AlertModel("Country is Added successfully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to Add the Country", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }
        [HttpGet]
        public ActionResult Provinces(int id)
        {
          
            List<Province> p = new LocationHandler().GetProvincesListBaseOnId(id);
            return PartialView("~/Views/Location/Provinces.cshtml", p);
        }

        [HttpPost]
        public ActionResult AddProvinces(FormCollection data)
        {
           try
            {
                int id = Convert.ToInt16(data["id"]);

                Province p = new Province();
                p.Name = data["name"];

                p.Country = new Country { Id = id };

                new LocationHandler().AddProvince(p);
                TempData.Add("alert", new AlertModel("State is Added Successfully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to add the State", AlertType.Error));
            }
            return RedirectToAction("Countries");

        }
        [HttpGet]
        public ActionResult Cities(int id)
        {
           
            List<City> c = new LocationHandler().GetCitiesList(id);
            return PartialView("~/Views/Location/Cities.cshtml", c);
        }

        [HttpPost]
        public ActionResult AddCities(FormCollection data)
        {
           
            try
            {
                int id = Convert.ToInt16(data["id"]);
                City c = new City();
                c.Name = data["name"];
                c.Province = new Province { Id = id };
                new LocationHandler().AddCity(c);
                TempData.Add("alert", new AlertModel("City Added SuccessFully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to Add city", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpPost]
        public ActionResult EditCountries(FormCollection data)
        {
           
            try
            {
                int idtoSearch = Convert.ToInt16(data["id"]);
                Country c = new Country { Code = Convert.ToInt16(data["code"]), Name = data["name"] };
                new LocationHandler().UpdateCountry(c, idtoSearch);
                TempData.Add("alert", new AlertModel("Country Updated Successfully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("failed to update country", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }
        [HttpGet]
        public ActionResult Form()
        {
          
            return PartialView("~/Views/Location/Form.cshtml");
        }
        [HttpGet]
        public ActionResult Form1()
        {
           
            return PartialView("~/Views/Location/Form1.cshtml");
        }
        [HttpGet]
        public ActionResult Form2()
        {
            
            return PartialView("~/Views/Location/Form2.cshtml");
        }

        [HttpGet]
        public ActionResult Form3()
        {
       
            return PartialView("~/Views/Location/Form3.cshtml");
        }

        [HttpGet]
        public ActionResult DelCountry(int id)
        {
           
            try
            {
                new LocationHandler().DelCountry(id);
                TempData.Add("alert", new AlertModel("Successfully deleted country", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("failed to delete country", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpPost]
        public ActionResult EditProvinces(FormCollection data)
        {
           
            try
            {
                int idToSearch = Convert.ToInt16(data["id"]);
                Province p = new Province { Name = data["name"] };
                new LocationHandler().UpdateProvince(p, idToSearch);
                TempData.Add("alert", new AlertModel("State updated successfully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to update state", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpGet]
        public ActionResult DelProvince(int id)
        {
            
            try
            {
                new LocationHandler().DelProvince(id);
                TempData.Add("alert", new AlertModel("State deleted successfully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to delete state", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpPost]
        public ActionResult EditCity(FormCollection data)
        {
           
            try
            {
                int idToSearch = Convert.ToInt16(data["id"]);
                City c = new City { Name = data["name"] };
                new LocationHandler().UpdateCity(c, idToSearch);
                TempData.Add("alert", new AlertModel("City updated Succcessfully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to update city", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }


        [HttpGet]
        public ActionResult DelCity(int id)
        {
       
            try
            {
                new LocationHandler().DelCity(id);
                TempData.Add("alert", new AlertModel("City deleted SuccessFully", AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to delete City", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

    }
}