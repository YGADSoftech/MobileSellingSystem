using MobileSellingEntities.MobileShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileSellingProject.Model
{
    public static class ProductHelper
    {
        public static ProductModel ToproModel(this Products entity)
        {
            ProductModel model = new ProductModel();
            model.id = entity.Id;
            model.name = entity.Name;
            model.imageUrl = entity.Images.ToArray()[0].Url;
            model.price = entity.Price;
            model.date = entity.LaunchDate;
            return model;
        }
        public static List<ProductModel> ToProductList(this List<Products> entities)
        {
            List<ProductModel> model = new List<ProductModel>();
            foreach(var e in entities)
            {
                model.Add(e.ToproModel());
            }
            return model;
        }
    }
}