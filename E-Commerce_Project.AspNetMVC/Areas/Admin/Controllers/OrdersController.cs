using E_Commerce_Project.BLL.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Project.AspNetMVC.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        OrderRepository or = new OrderRepository();
        OrderStatusRepository osr = new OrderStatusRepository();

        [Authorize(Roles = "Admin")]
        public PartialViewResult _OrderNav()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OrderList(string filter)
        {
            or = new OrderRepository();
            var orders = or.SelectAll().Where(i => i.OrderStatus.Status == "Onay Bekleniyor").ToList();
            if(filter == "shipped")
            {
                orders = or.SelectAll().Where(i => i.OrderStatus.Status == "Kargoya Verildi").ToList();
            }
            else if(filter == "ordercancellation")
            {
                orders = or.SelectAll().Where(i => i.OrderStatus.Status == "İptal Edildi").ToList();
            }
            ViewData["filter"] = filter;
            return View(orders.OrderByDescending(i => i.OrderDate).ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OrderDetails(string id)
        {
            var order = or.SelectById(id);
            if(order != null)
            {
                return View(order);
            }
            return RedirectToAction("OrderList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OrderShipped(string id, string trackingNumber)
        {
            var order = or.SelectById(id);
            if(order != null && trackingNumber != null)
            {
                order.TrackingNumber = trackingNumber;
                order.OrderStatus = osr.SelectAll().FirstOrDefault(i => i.Status == "Kargoya Verildi");
                or.AddOrUpdate(order);

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("e-commerce-project@hotmail.com", "project321");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("e-commerce-project@hotmail.com", "E-Commerce Project");
                mail.To.Add(order.User.Email);
                mail.Subject = "Siparişiniz Kargoya Verilmiştir.";
                mail.IsBodyHtml = true;
                mail.Body = order.Id.Replace("-", "").ToUpper() + " Takip numaralı siparişiniz kargoya verilmiştir. <br /> Kargo Firması: " + order.Shipping.Name + "<br /> Takip No: " + order.TrackingNumber;
                smtp.Send(mail);
            }
            return RedirectToAction("OrderList");
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult CancelOrder(string id)
        {
            var order = or.SelectById(id);
            if(order != null)
            {
                order.OrderStatus = osr.SelectAll().FirstOrDefault(i => i.Status == "İptal Edildi");
                or.AddOrUpdate(order);
            }
            if(User.IsInRole("Admin"))
            {
                return RedirectToAction("OrderList");
            }
            return RedirectToAction("Index", "Account", new { area = "" });
        }
    }
}