using E_Commerce_Project.AspNetMVC.Models.DTO;
using E_Commerce_Project.DAL.ORM.Context;
using E_Commerce_Project.DAL.ORM.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce_Project.BLL.DbTools;
using Microsoft.Owin.Security;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace E_Commerce_Project.AspNetMVC.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        public AccountController()
        {
            userManager = new UserManager<User>(new UserStore<User>(DbInstance.Instance));

            userManager.UserValidator = new UserValidator<User>(userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false
            };
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)

        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDTO model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = userManager.FindByEmail(model.Email);
                if(user != null)
                {
                    user = userManager.Find(user.UserName, model.Password);

                    if(user != null)
                    {
                        if(!user.EmailConfirmed)
                        {
                            ViewBag.LoginError = "Mail adresinizi onaylamadınız. <a href='/Account/ConfirmMail?email=" + user.Email + "&returnUrl=" + returnUrl + "' class='alert-link'>Link gönder.</a>";
                            return View(model);
                        }

                        var authManager = HttpContext.GetOwinContext().Authentication;
                        var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        var authProperties = new AuthenticationProperties()
                        {
                            IsPersistent = true
                        };

                        authManager.SignOut();
                        authManager.SignIn(authProperties, identity);
                        return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                    }
                    else
                    {
                        ViewBag.LoginError = "Email veya şifre hatalı.";
                    }
                }
                else
                {
                    ViewBag.LoginError = "Böyle bir kullanıcı bulunmamaktadır.";
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterDTO model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = new User();
                user.UserName = model.UserName;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;

                var result = userManager.Create(user, model.Password);

                if(result.Succeeded)
                {
                    EmailConfirm(user.Email);
                    TempData["returnUrl"] = returnUrl;
                    return RedirectToAction("ConfirmMail");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ViewBag.ErrorResister += error + " ";
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmMail(string email, string returnUrl)
        {
            if(email != null)
            {
                EmailConfirm(email);
            }
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        public ActionResult ConfirmMailOk(string code)
        {
            var user = userManager.FindById(code);
            if(user != null)
            {
                user.EmailConfirmed = true;
                userManager.Update(user);
                ViewBag.UserName = user.UserName.ToString();
                ViewBag.Name = user.Name.ToString();
                ViewBag.Surname = user.Surname.ToString();
                return View();
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            var user = userManager.FindById(code);
            if(user != null)
            {
                ViewBag.Code = code;
                ViewBag.UserName = user.UserName;
                return View();
            }

            return View("Error");
        }

        [HttpPost]
        public ActionResult ResetPassword(string code, ResetPasswordDTO model)
        {
            if(ModelState.IsValid)
            {
                var user = userManager.FindById(code);
                if(user != null)
                {
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, model.Password);
                    ViewBag.PasswordResetSuccess = "Şifreniz sıfırlanmıştır tekrar <a href='/Account/Login' class='alert-link'>giriş</a> yapabilirsiniz.";
                    return View();
                }
                return View("Error");
            }
            return View(model);
        }

        public void EmailConfirm(string email)
        {
            var user = userManager.FindByEmail(email);
            if(user != null)
            {
                TempData["email"] = email;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("e-commerce-project@hotmail.com", "project321");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("e-commerce-project@hotmail.com", "E-Commerce Project");
                mail.To.Add(email);
                mail.Subject = "Email Onay Hk.";
                mail.IsBodyHtml = true;
                mail.Body = "Mail adresinizi onaylamak için <a href='http://localhost:50420/Account/ConfirmMailOk?code=" + user.Id + "'>linke</a> tıklayınız.";

                smtp.Send(mail);
            }
        }

        public void PasswordReset(string email)
        {
            var user = userManager.FindByEmail(email);
            if(user != null)
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("e-commerce-project@hotmail.com", "project321");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("e-commerce-project@hotmail.com", "E-Commerce Project");
                mail.To.Add(email);
                mail.Subject = "Parola Sıfırlama Hk.";
                mail.IsBodyHtml = true;
                mail.Body = "Parolanızı sıfırlamak için <a href='http://localhost:50420/Account/ResetPassword?code=" + user.Id + "'>linke</a> tıklayınız.";

                smtp.Send(mail);
            }
        }
    }
}