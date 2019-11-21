using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.Cart
{
    public class Cart
    {
        private List<CartItem> _cart = new List<CartItem>();

        public List<CartItem> UserCart
        {
            get { return _cart; }
        }

        public void Add(CartItem item)
        {
            if(_cart.Any(i => i.ProductId == item.ProductId))
            {
                _cart.First(i => i.ProductId == item.ProductId).Amount += 1;
                return;
            }
            _cart.Add(item);
        }

        public void Remove(string id)
        {
            if(_cart.Any(i => i.ProductId == id))
            {
                var item = _cart.FirstOrDefault(i => i.ProductId == id);
                _cart.Remove(item);
            }
        }

        public decimal SubTotal()
        {
            return _cart.Sum(i => i.SubTotal);
        }

        public decimal SubDiscountedPrice()
        {
            return _cart.Sum(i => i.SubDiscountedPrice);
        }
    }
}