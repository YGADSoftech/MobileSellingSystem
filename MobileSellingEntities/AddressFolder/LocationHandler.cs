﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace MobileSellingEntities.AddressFolder
{
  public class LocationHandler
    {
        public List<Country> GetCountryList()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from cntry in context.countries orderby cntry.Name ascending select cntry).ToList();
            }
        }
        public Country GetCountry(string name)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from c in context.countries where c.Name == name select c).FirstOrDefault();
            }
        }
        public Country GetCountry(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from c in context.countries where c.Id == id select c).First();
            }
        }

        public void AddCountry(Country country)
        {
            using (ContextClass context = new ContextClass())
            {
                context.countries.Add(country);
                context.SaveChanges();
            }
        }
        public void UpdateCountry(Country c, int IdToSearcCountry)
        {
            using (ContextClass context = new ContextClass())
            {
                Country found = context.countries.Find(IdToSearcCountry);
                found.Code = c.Code;
                found.Name = c.Name;
                context.SaveChanges();
            }
        }

        public void DelCountry(int id)
        {
            using (ContextClass context = new ContextClass())
            {
              
                Country found = context.countries.Find(id);
                context.countries.Remove(found);

                context.SaveChanges();
            }
        }

        //Provinces Entities Handling

        public List<Province> GetProvincesList(Country c)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.provinces where p.Country.Id == c.Id select p).ToList();
            }
        }
        public Province GetProvince(string name, int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.provinces where p.Country.Id == id && p.Name == name select p).FirstOrDefault();
            }
        }

        public List<Province> GetProvincesListBaseOnId(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.provinces where p.Country.Id == id orderby p.Name ascending select p).ToList();
            }
        }


        //City data handling
        public List<City> GetCitiesList(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                List<City> cities = (from c in context.cities.Include(c => c.Province.Country)
                                     where c.Province.Id == id
                                     orderby c.Name ascending
                                     select c).ToList();
                return cities;
            }
        }
        public List<City> GetCitiesList(Country c)
        {
            using (ContextClass context = new ContextClass())
            {
                List<City> Cities = (from city in context.cities.Include(City => City.Province.Country)
                                     where city.Province.Country.Id == c.Id
                                     select city).ToList();
                return Cities;
            }
        }
        public List<City> GetCitiesListOnbaseOfID(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                List<City> cities = (from c in context.cities.Include(c => c.Province.Country)
                                     where c.Province.Id == id
                                     select c).ToList();
                return cities;
            }
        }

        public void AddProvince(Province p)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(p.Country).State = EntityState.Unchanged;
                context.provinces.Add(p);
                context.SaveChanges();
            }
        }
        public void UpdateProvince(Province p, int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Province pr = context.provinces.Find(id);
                pr.Name = p.Name;
                context.SaveChanges();
            }
        }
        public void DelProvince(int id)
        {
            using (ContextClass context = new ContextClass())
            {
               Province found = context.provinces.Find(id);
                context.provinces.Remove(found);

                context.SaveChanges();
            }
        }


        public void AddCity(City c)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(c.Province).State = EntityState.Unchanged;
                context.cities.Add(c);
                context.SaveChanges();
            }
        }
        public void UpdateCity(City c, int id)
        {
            using (ContextClass context = new ContextClass())
            {
                City city = context.cities.Find(id);
                city.Name = c.Name;
                context.SaveChanges();
            }
        }
        public void DelCity(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                City c = context.cities.Find(id);
                context.cities.Remove(c);
                context.SaveChanges();

            }
        }

    }
}
