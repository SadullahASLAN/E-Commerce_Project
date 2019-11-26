using E_Commerce_Project.AspNetMVC.Areas.Admin.Models;
using E_Commerce_Project.BLL.Repositories.Repository;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Project.AspNetMVC.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository pr = new ProductRepository();
        MainCategoryRepository mcr = new MainCategoryRepository();
        SubCategoryRepository scr = new SubCategoryRepository();

        // GET: Admin/Product
        public ActionResult ProductList(string filter)
        {
            ViewBag.Header = "Tüm Ürünler";
            var products = pr.SelectAllAdmin();

            if(filter == "stocklow")
            {
                ViewBag.Header = "Stokta Az Olan Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.Stock < 5).ToList();
            }
            else if(filter == "onsale")
            {
                ViewBag.Header = "Satışta Olan Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.InSales == true).ToList();
            }
            else if(filter == "notonsale")
            {
                ViewBag.Header = "Satışta Olmayan Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.InSales == false).ToList();
            }
            products = products.OrderByDescending(i => i.CreatedDate).ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.MainCat = new SelectList(mcr.SelectAll(), "Id", "Name", "00000000-3e66-4dba-99e3-d255f90080cd");
            var product = new Product();
            var addProductModel = new AddProductModel();
            addProductModel.Product = product;
            return View(addProductModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AddProductModel addProductModel)
        {
            if(ModelState.IsValid)
            {
                string folderPath = Server.MapPath($"~/Content/image/product/{addProductModel.Product.Id}");
                Directory.CreateDirectory(folderPath);
                foreach(var image in addProductModel.Images)
                {
                    string imagePaht = Server.MapPath($"~/Content/image/product/{addProductModel.Product.Id}/{image.FileName}");
                    image.SaveAs(imagePaht);
                    addProductModel.Product.Images.Add(new Image() { Paht = $"/Content/image/product/{addProductModel.Product.Id}/{image.FileName}" });
                }
                pr.AddOrUpdate(addProductModel.Product);
                return RedirectToAction("ProductList");
            }
            ViewBag.MainCat = new SelectList(mcr.SelectAll(), "Id", "Name", "00000000-3e66-4dba-99e3-d255f90080cd");
            return View(addProductModel);
        }

        public ActionResult DeleteProduct(string id)
        {
            pr.Delete(id);
            return RedirectToAction("ProductList");
        }

        public JsonResult SubCategory(string id)
        {
            var sub = scr.MainCategoryId(id);
            var list = new Dictionary<string, string>();
            foreach(var subcat in sub)
            {
                list.Add(subcat.Id, subcat.Name);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllBrand()
        {
            var products = pr.SelectAll();
            var list = new List<string>();
            foreach(var product in products)
            {
                list.Add(product.Brand);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}