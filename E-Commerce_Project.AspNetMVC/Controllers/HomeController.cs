using E_Commerce_Project.AspNetMVC.Models.VM;
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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(string category, string subCategory, string brand)
        {
            ViewBag.SubCategory = subCategory;
            var mcr = new MainCategoryRepository();
            if(category == null) return RedirectToAction("Index");
            var mainCategory = mcr.SelectByName(category);
            if(subCategory != null)
            {
                var subCat = mainCategory.SubCategories.Where(i => i.IsDeleted == false && i.Name == subCategory).FirstOrDefault();
                ViewBag.SubCat = subCat;
                if(brand != null)
                {
                    ViewBag.BrandProduct = subCat.Products.Where(i => i.IsDeleted == false && i.InSales == true && i.Brand == brand).ToList();
                }
            }
            else
            {
                if(brand != null)
                {
                    var subCat = mainCategory.SubCategories.Where(i => i.IsDeleted == false).ToList();
                    var listBrandProduct = new List<Product>();
                    foreach(var sub in subCat)
                    {
                        var products = sub.Products.Where(i => i.IsDeleted == false && i.InSales == true && i.Brand == brand).ToList();
                        foreach(var pro in products)
                        {
                            listBrandProduct.Add(pro);
                        }
                    }
                    ViewBag.BrandProduct = listBrandProduct;
                }
            }
            return View(mainCategory);
        }

        [HttpGet]
        public ActionResult ProductDetails(string id)
        {
            var pr = new ProductRepository();
            var product = pr.SelectById(id);
            if(product == null) return View("Error");
            var productViewModel = new ProductViewModel();
            productViewModel.Product = product;
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetails(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userManager = new UserManager<User>(new UserStore<User>(DbInstance.Instance));
                var user = userManager.FindByName(User.Identity.Name);
                model.Comment.UserId = user.Id;
                var cr = new CommentRepository();
                cr.AddOrUpdate(model.Comment);
            }
            var pr = new ProductRepository();
            var product = pr.SelectById(model.Comment.ProductId);
            model.Product = product;
            model.commentTab = true;
            return View(model);
        }
    }
}