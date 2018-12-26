using MobileSellingEntities.MobileShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileSellingProject.Model
{
    public class CartItems
    {
        public CartItems()
        {

        }
        public CartItems(Products product, int quantity, string color)
        {
            this.product = product;
            this.quantity = quantity;
            this.color = color;

        }
        private Products product = new Products();
        private int quantity;
        private string color;
        public Products Products
        {
            get { return product; }
            set { product = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
       
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}