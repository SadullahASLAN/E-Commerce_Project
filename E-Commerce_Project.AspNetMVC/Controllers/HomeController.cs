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
                search = search.ToLower();
                var products = pr.SelectAll().Where(i => i.Name.ToLower().Contains(search) || i.Brand.ToLower().Contains(search) || i.Model.ToLower().Contains(search) || i.Code.ToLower() == search).ToList();
                return View(products);
            }
            return View("Index");
        }

        public ActionResult Category(string category, string subCategory, string brand)
        {
            ViewBag.SubCategory = subCategory;
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

        [OutputCache(Duration = 86400)]
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

            return PartialView(products);
        }

        public PartialViewResult _CampaignProducts()
        {
            var products = pr.SelectAll().Where(i => i.DiscountDescription != null).ToList();

            var product = products[0];

            return PartialView(products);
        }
        public bool urunekle()
        {
            var urun = new Product()
            {
                Name = "HP EliteOne 800 G3 All-in-One PC (1KA85EA)",
                Price = 10000m,
                Description = "Açıklama",
                Stock = 5,
                Brand = "Hp",
                Model = "EliteOne 800",
                SubCategoryId = scr.SelectAll().Where(i => i.Name == "All In One").FirstOrDefault().Id,
                InSales = true,
                Images = new List<Image>()
                    {
                        new Image(){Paht="https://webdenal.s3.amazonaws.com/catalog2/948237.jpg"},
                    }
            };
            return pr.AddOrUpdate(urun);
        }
    }
}