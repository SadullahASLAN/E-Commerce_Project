using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.Cart
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public short Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public byte DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get => Price * (100 - DiscountPercentage) / 100; }
        public decimal SubTotal { get => Amount * Price; }
        public decimal SubDiscountedPrice { get => Amount * (Price * (100 - DiscountPercentage) / 100); }
    }
}