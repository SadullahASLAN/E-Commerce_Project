using E_Commerce_Project.AspNetMVC.Models.Cart;
using E_Commerce_Project.AspNetMVC.Tools;
using E_Commerce_Project.BLL.DbTools;
using E_Commerce_Project.BLL.Repositories.Repository;
using E_Commerce_Project.DAL.ORM.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace E_Commerce_Project.AspNetMVC.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult TimeOut()
        {
            return View();
        }

        UserManager<User> userManager = new UserManager<User>(new UserStore<User>(DbInstance.Instance));

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

            var user = userManager.FindByName(User.Identity.Name);
            ViewBag.User = user;
            order.UserId = user.Id;
            Session["Order"] = order;
            return View(order);
        }

        public ActionResult AddToOrderAddress(string id)
        {
            var uar = new UserAddressRepository();
            var address = uar.SelectById(id);
            if(id == null || address == null) return RedirectToAction("OrderAddress");
            if((Order)Session["Order"] == null) return RedirectToAction("TimeOut");
            var order = (Order)Session["Order"];
            order.UserAddress = address;
            Session["Order"] = order;
            return RedirectToAction("Payment");
        }

        [HttpPost]
        public ActionResult NewOrderAddress(UserAddress model)
        {
            if(ModelState.IsValid)
            {
                var user = userManager.FindByName(User.Identity.Name);
                model.UserId = user.Id;
                var uar = new UserAddressRepository();
                uar.AddOrUpdate(model);
            }
            return RedirectToAction("OrderAddress");
        }

        public ActionResult Payment()
        {
            var order = (Order)Session["Order"];
            return View(order);
        }

        public ActionResult OrderIsOk()
        {
            if((Order)Session["Order"] == null) return RedirectToAction("TimeOut");
            var order = (Order)Session["Order"];
            var or = new OrderRepository();
            if(or.AddOrUpdate(order))
            {
                var pr = new ProductRepository();

                foreach(var orderDetails in order.OrderDetails)
                {
                    var product = pr.SelectById(orderDetails.ProductId);
                    product.Stock -= orderDetails.Quantity;
                    pr.AddOrUpdate(product);
                }

                Session["Cart"] = new Cart();
                var userStore = new UserStore<User>(DbInstance.Instance);
                var userManager = new UserManager<User>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                ViewBag.UserEmail = user.Email;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("e-commerce-project@hotmail.com", "project321");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("e-commerce-project@hotmail.com", "E-Commerce Project");
                mail.To.Add(user.Email);
                mail.Subject = "Siparişiniz Hk.";
                mail.IsBodyHtml = true;
                mail.Body = _OrderMailBody().PartialRenderToString(); ;
                smtp.Send(mail);

                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public PartialViewResult _OrderMailBody()
        {
            return PartialView("_OrderMailBody");
        }
    }
}