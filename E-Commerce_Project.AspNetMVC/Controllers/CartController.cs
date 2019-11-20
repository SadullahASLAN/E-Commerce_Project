using E_Commerce_Project.AspNetMVC.Models.Cart;
using E_Commerce_Project.BLL.Repositories.Repository;
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
        public ActionResult Index()
        {
            return View();
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
                GetCart().Add(cartItem);
            }
            return RedirectToAction("_CartButton");
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