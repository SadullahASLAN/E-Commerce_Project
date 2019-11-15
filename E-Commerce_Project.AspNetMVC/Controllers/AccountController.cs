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

namespace E_Commerce_Project.AspNetMVC.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        public AccountController()
        {
            userManager = new UserManager<User>(new UserStore<User>(DbInstance.Instance));
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
                user.UserName = model.UserName.Trim(' ');
                user.Name = model.Name.Trim();
                user.Surname = model.Surname.Trim();
                user.Email = model.Email.Trim();

                var result = userManager.Create(user, model.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Login", returnUrl);
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
    }
}