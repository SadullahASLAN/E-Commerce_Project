using E_Commerce_Project.AspNetMVC.Models.Cart;
using E_Commerce_Project.BLL.DbTools;
using E_Commerce_Project.BLL.Repositories.Repository;
using E_Commerce_Project.DAL.ORM.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Project.AspNetMVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Card
        public ActionResult CartList()
        {
            return View(GetCart());
        }

        public ActionResult _CartButton()
        {
            return PartialView(GetCart());
        }

        public ActionResult AddToCart(string id)
        {
            var pr = new ProductRepository();
            var product = pr.SelectById(id);
            if(product != null)
            {
                var cartItem = new CartItem();
                cartItem.ProductId = product.Id;
                cartItem.ProductName = product.Name;
                cartItem.Amount = 1;
                cartItem.Price = product.Price;
                cartItem.Image = product.Images.FirstOrDefault().Paht;
                cartItem.DiscountPercentage = product.DiscountPercentage;
                GetCart().Add(cartItem);
            }
            return RedirectToAction("_CartButton");
        }

        public ActionResult RemoveToItemCart(string id)
        {
            if(GetCart() != null)
            {
                GetCart().Remove(id);
            }
            return RedirectToAction("CartList");
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}