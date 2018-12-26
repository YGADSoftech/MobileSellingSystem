using MobileSellingEntities.AddressFolder;
using MobileSellingEntities.MobileShop;
using MobileSellingEntities.UserFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileSellingEntities
{
   public class ContextClass:DbContext
    {
        public ContextClass() : base("ConString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>()
                .HasMany<Colors>(p => p.colors)
                .WithMany();

            modelBuilder.Entity<Products>()
                .HasMany<Sizes>(p => p.SizesOffered)
                .WithMany();

          
        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<SubCategory> SubCategory { get; set; }

        public DbSet<Colors> Colors { get; set; }

        public DbSet<Sizes> Sizes { get; set; }

        public DbSet<Condition> conditions { get; set; }

        public DbSet<Products> Products { get; set; }

        //Location Tables
        public DbSet<Country> countries { get; set; }
        public DbSet<Province> provinces { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
