using MobileSellingEntities.MobileShop;
using MobileSellingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileSellingProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ProductModel> products = new MobileShopHandler().GetProducts().ToProductList();
            
            return View(products);
        }
        public ActionResult Features()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShoppingCart(int id)
        {
            Products p = new MobileShopHandler().GetProduct(id);
            string color = p.colors.ToArray()[0].Name;
            if(Session[WebUtil.cart]== null)
            {
                List<CartItems> items = new List<CartItems>();
                items.Add(new CartItems(p, 1, color));
                Session[WebUtil.cart] = items;
            }
            else
            {
                List<CartItems> items = (List<CartItems>)Session[WebUtil.cart];
                int index = IsExisting(id);
                if (index == -1)
                {
                    items.Add(new CartItems(p, 1, color));
                }
                else
                {
                    items[index].Quantity++;
                }
            }
            return PartialView("~/Views/Home/ShoppingCart.cshtml");
        }
        //Checking product in cart
        public int IsExisting(int id)
        {
            List<CartItems> items = (List<CartItems>)Session[WebUtil.cart];
            int index = -1;
            for(int i = 0; i<items.Count; i++)
            {
                if(items[i].Products.Id == id)
                {
                    index = i;
                }
            }
            return index;
        }

        public ActionResult UserView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin()
        {
            

            return View();
        }
        public ActionResult Contact()
        {
            return PartialView("~/Views/Home/Contact.cshtml");
        }
        public ActionResult About()
        {
            return PartialView("~/Views/Home/About.cshtml");
        }

    }
}