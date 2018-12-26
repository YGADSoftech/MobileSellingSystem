using MobileSellingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileSellingProject.Model
{
    public  static class ModelHelper
    {
        public static List<SelectListItem> ToSelectItemList(this IEnumerable<Illistables> values)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var v in values)
            {
                items.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id) });
            }
            return items;
        }
    }
}