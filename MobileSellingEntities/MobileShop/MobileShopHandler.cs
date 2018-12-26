using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace MobileSellingEntities.MobileShop
{
   public class MobileShopHandler
    {
        public List<Department> GetDepartments()
        {
            using (ContextClass context = new ContextClass())
            {
                List<Department> dept = (from d in context.Departments select d).ToList();
                return dept;
            }
        }
        public void AddDep(Department d)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Departments.Add(d);
                context.SaveChanges();
            }
        }
        public Department GetDep(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Department d = context.Departments.Find(id);
                return d;
            }
        }
        public void updateDep(Department dep)
        {
            using (ContextClass context = new ContextClass())
            {
                Department d = context.Departments.Find(dep.Id);
                d.Name = dep.Name;
                d.ImageUrl = dep.ImageUrl;
                context.SaveChanges();
            }
        }
        public void DeleteDepartment(Department department)
        {
            using (ContextClass context = new ContextClass())
            {
                Department found = context.Departments.Find(department.Id);
                context.Departments.Remove(found);
                context.SaveChanges();
            }
        }
        //End of Departments crud operation

        //starting product handler

        public List<Products> GetProducts()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                        .Include(p => p.Condition)
                        .Include(p => p.colors)
                        .Include(p => p.Images)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.SubCategory.Category.Department)
                        select p).ToList();
            }
        }
        public List<Sizes> GetSizes()
        {
            using (ContextClass con = new ContextClass())
            {
                return (from s in con.Sizes select s).ToList();
            }
        }
        public List<Colors> GetColors()
        {
            using (ContextClass con = new ContextClass())
            {
                return (from s in con.Colors select s).ToList();
            }
        }
        public List<Condition> GetCondition()
        {
            using (ContextClass con = new ContextClass())
            {
                return (from s in con.conditions select s).ToList();
            }
        }
        public List<Category> GetCategories(int id)
        {
            using (ContextClass con = new ContextClass())
            {
                return (from c in con.Category where c.Department.Id == id select c).ToList();
            }
        }
        public List<SubCategory> GetSubCategories(int id)
        {
            using (ContextClass con = new ContextClass())
            {
                return (from s in con.SubCategory where s.Category.Id == id select s).ToList();
            }
        }

        public void AddProduct(Products p)
        {
            using (ContextClass context = new ContextClass())
            {
                foreach (var c in p.colors)
                {
                    context.Entry(c).State = EntityState.Unchanged;
                }
                foreach (var s in p.SizesOffered)
                {
                    context.Entry(s).State = EntityState.Unchanged;
                }
                context.Entry(p.SubCategory).State = EntityState.Unchanged;
                context.Entry(p.Condition).State = EntityState.Unchanged;

                context.Products.Add(p);
                context.SaveChanges();
            }

        }
        public Products GetProducts(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Products d = context.Products.Find(id);
                return d;
            }
        }
        public void DeleteProduct(Products d)
        {
            using (ContextClass context = new ContextClass())
            {

                Products find = (from p in context.Products
                                         .Include(p => p.Condition)
                                         .Include(p => p.colors)
                                         .Include(p => p.Images)
                                         .Include(p => p.SizesOffered)
                                         .Include(p => p.SubCategory.Category.Department)
                                where p.Id == d.Id
                                select p).FirstOrDefault();


                context.Products.Remove(find);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Products products)
        {
            using (ContextClass context = new ContextClass())
            {
                Products d = context.Products.Find(products.Id);
                d.Name = products.Name;
                d.Price = products.Price;
                d.Images = products.Images;
                context.SaveChanges();
            }
        }
        public Products GetProduct(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                        .Include(p => p.Condition)
                        .Include(p => p.colors)
                        .Include(p => p.Images)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.SubCategory.Category.Department)
                        select p).FirstOrDefault();
            }
        }
      
       
    }
}

