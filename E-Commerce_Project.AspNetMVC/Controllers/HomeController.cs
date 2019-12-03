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
        MainCategoryRepository mcr = new MainCategoryRepository();
        CommentRepository cr = new CommentRepository();
        ProductRepository pr = new ProductRepository();
        SubCategoryRepository scr = new SubCategoryRepository();
        OrderDetailRepository odr = new OrderDetailRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string search)
        {
            if(search != null)
            {
                ViewBag.Search = search;
                search = search.ToLower().Trim();
                var products = pr.SelectAll().Where(i => i.Name.ToLower().Contains(search) || i.Brand.ToLower().Contains(search) || i.Model.ToLower().Contains(search) || i.Code.ToLower() == search).ToList();
                foreach(var mainCategory in mcr.SelectAll().Where(i => i.Name.ToLower().Contains(search)).ToList())
                {
                    foreach(var subCategory in mainCategory.SubCategories)
                    {
                        products.AddRange(subCategory.Products);
                    }
                }
                foreach(var subCategory1 in scr.SelectAll().Where(i => i.Name.ToLower().Contains(search)).ToList())
                {
                    products.AddRange(subCategory1.Products);
                }
                products = products.Distinct().ToList();
                return View(products);
            }
            return View("Index");
        }

        public ActionResult Category(string category, string subCategory, string brand)
        {
            ViewBag.SubCategory = subCategory;
            var mainCategory = mcr.SelectByName(category);
            if(mainCategory == null) return RedirectToAction("Index");
            if(subCategory != null)
            {
                var subCat = mainCategory.SubCategories.Where(i => i.IsDeleted == false && i.Name == subCategory).FirstOrDefault();
                if(subCat == null) return RedirectToAction("Index");
                ViewBag.SubCat = subCat;
                if(brand != null)
                {
                    ViewBag.BrandProduct = subCat.Products.Where(i => i.IsDeleted == false && i.InSales == true && i.Brand == brand && i.Stock > 0).ToList();
                    if((ViewBag.BrandProduct as List<Product>).Count() == 0) return RedirectToAction("Index");
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
                        var products = sub.Products.Where(i => i.IsDeleted == false && i.InSales == true && i.Brand == brand && i.Stock > 0).ToList();
                        foreach(var pro in products)
                        {
                            listBrandProduct.Add(pro);
                        }
                    }
                    if(listBrandProduct.Count() == 0) return RedirectToAction("Index");
                    ViewBag.BrandProduct = listBrandProduct;
                }
            }
            return View(mainCategory);
        }

        [HttpGet]
        public ActionResult ProductDetails(string id, bool campanyComment = false)
        {
            var product = pr.SelectById(id);
            if(product == null) return View("Error");
            var productViewModel = new ProductViewModel();
            productViewModel.Product = product;
            if(campanyComment)
            {
                productViewModel.commentTab = true;
            }
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
                cr.AddOrUpdate(model.Comment);
            }
            var product = pr.SelectById(model.Comment.ProductId);
            model.Product = product;
            model.commentTab = true;
            return View(model);
        }

        public PartialViewResult _NewPRoducts()
        {
            var products = pr.SelectAll();
            var newProducts = products.OrderByDescending(i => i.CreatedDate).Take(12).ToList();
            return PartialView(newProducts);
        }

        public PartialViewResult _BestSellingProducts()
        {
            var bestSellingProducts = odr.SelectAll().GroupBy(i => i.ProductId).Select(i => new
            {
                productId = i.Key,
                quantity = i.Sum(x => x.Quantity)
            }).OrderByDescending(i => i.quantity).Take(12).ToList();

            var products = new List<Product>();
            foreach(var item in bestSellingProducts)
            {
                products.Add(pr.SelectById(item.productId));
            }

            return PartialView(products.Where(i => i.Stock > 0).ToList());
        }

        public PartialViewResult _CampaignProducts()
        {
            var products = pr.SelectAll().Where(i => i.DiscountDescription != null).ToList();

            var product = products.FirstOrDefault();

            return PartialView(products);
        }
    }
}