using MobileSellingEntities.MobileShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileSellingProject.Model
{
    public static class DepartmentandCategoriesHelper
    {
        public static DepartmentModel ToDepModel(this Department entity)
        {
            DepartmentModel model = new DepartmentModel();
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.imageUrl = entity.ImageUrl;
            return model;
        }
        public static Department ToDepEntity(this DepartmentModel model)
        {
            Department entity = new Department();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.ImageUrl = model.imageUrl;
            return entity;
        }
        public static List<DepartmentModel> ToDepModelList(this List<Department> entities)
        {
            List<DepartmentModel> models = new List<DepartmentModel>();
            foreach (var e in entities)
            {
                models.Add(ToDepModel(e));
            }
            models.TrimExcess();
            return models;
        }



    }
}