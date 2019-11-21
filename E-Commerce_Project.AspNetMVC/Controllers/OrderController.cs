using E_Commerce_Project.AspNetMVC.Models.Cart;
using E_Commerce_Project.BLL.Repositories.Repository;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Project.AspNetMVC.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult TimeOut()
        {
            return View();
        }

        // GET: Order
        public ActionResult OrderAddress()
        {
            var cart = (Cart)Session["Cart"];

            if(cart == null) return RedirectToAction("TimeOut");

            var order = new Order();
            foreach(var cartItem in cart.UserCart)
            {
                var orderDetails = new OrderDetail();
                var pr = new ProductRepository();
                var product = pr.SelectById(cartItem.ProductId);
                orderDetails.Product = product;
                orderDetails.UnitPrice = cartItem.SubDiscountedPrice;
                orderDetails.Quantity = cartItem.Amount;
                order.OrderDetails.Add(orderDetails);
            }

            Session["Order"] = order;

            return View(order);
        }
    }
}