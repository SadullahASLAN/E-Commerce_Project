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
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        ProductRepository pr = new ProductRepository();
        MainCategoryRepository mcr = new MainCategoryRepository();
        SubCategoryRepository scr = new SubCategoryRepository();
        ImageRepository ir = new ImageRepository();

        // GET: Admin/Product
        public ActionResult ProductList(string filter)
        {
            ViewBag.Header = "Tüm Ürünler";
            ViewBag.Filter = filter;
            var products = pr.SelectAllAdmin();

            if(filter == "stocklow")
            {
                ViewBag.Header = "Stokta Az Olan Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.Stock < 6).ToList();
            }
            else if(filter == "onsale")
            {
                ViewBag.Header = "Satışta Olan Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.InSales == true).ToList();
            }
            else if(filter == "notonsale")
            {
                ViewBag.Header = "Satışta Olmayan Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.InSales == false || i.Stock == 0).ToList();
            }
            else if(filter == "campaign")
            {
                ViewBag.Header = "Kampanyalı Ürünler";
                products = pr.SelectAllAdmin().Where(i => i.DiscountDescription != null).ToList();
            }
            products = products.OrderByDescending(i => i.CreatedDate).ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.MainCat = new SelectList(mcr.SelectAll(), "Id", "Name", "00000000-3e66-4dba-99e3-d255f90080cd");
            var product = new Product();
            var addProductModel = new AddOrUpdateProductModel();
            addProductModel.Product = product;
            return View(addProductModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AddOrUpdateProductModel addOrUpdateProductModel)
        {
            if(ModelState.IsValid)
            {
                if(addOrUpdateProductModel.Images.FirstOrDefault() != null)
                {
                    string folderPath = Server.MapPath($"~/Content/image/product/{addOrUpdateProductModel.Product.Id}");
                    Directory.CreateDirectory(folderPath);
                    foreach(var image in addOrUpdateProductModel.Images)
                    {
                        string imagePaht = Server.MapPath($"~/Content/image/product/{addOrUpdateProductModel.Product.Id}/{image.FileName}");
                        image.SaveAs(imagePaht);
                        addOrUpdateProductModel.Product.Images.Add(new Image() { Paht = $"/Content/image/product/{addOrUpdateProductModel.Product.Id}/{image.FileName}" });
                    }
                }
                pr.AddOrUpdate(addOrUpdateProductModel.Product);
                return RedirectToAction("ProductList");
            }
            ViewBag.MainCat = new SelectList(mcr.SelectAll(), "Id", "Name", "00000000-3e66-4dba-99e3-d255f90080cd");
            return View(addOrUpdateProductModel);
        }

        [HttpGet]
        public ActionResult UpdateProduct(string id, string filter)
        {
            var product = pr.SelectById(id);
            if(product != null)
            {
                ViewBag.MainCat = new SelectList(mcr.SelectAll(), "Id", "Name", product.SubCategory.MainCategoryId);
                ViewBag.SubCat = new SelectList(mcr.SelectById(product.SubCategory.MainCategoryId).SubCategories, "Id", "Name", product.SubCategoryId);
                ViewBag.Filter = filter;
                var addProductModel = new AddOrUpdateProductModel();
                addProductModel.Product = product;
                return View(addProductModel);
            }
            return Redirect("/Admin/Product/ProductList?filter=" + filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(AddOrUpdateProductModel addOrUpdateProductModel, string filter)
        {
            if(ModelState.IsValid)
            {
                if(addOrUpdateProductModel.Images.FirstOrDefault() != null)
                {
                    string folderPath = Server.MapPath($"~/Content/image/product/{addOrUpdateProductModel.Product.Id}");
                    Directory.CreateDirectory(folderPath);
                    foreach(var image in addOrUpdateProductModel.Images)
                    {
                        string imagePaht = Server.MapPath($"~/Content/image/product/{addOrUpdateProductModel.Product.Id}/{image.FileName}");
                        image.SaveAs(imagePaht);
                        ir.AddOrUpdate(new Image() { ProductId = addOrUpdateProductModel.Product.Id, Paht = $"/Content/image/product/{addOrUpdateProductModel.Product.Id}/{image.FileName}" });
                    }
                }
                pr.AddOrUpdate(addOrUpdateProductModel.Product);
                return RedirectToAction("ProductList", new { filter = filter });
            }
            ViewBag.MainCat = new SelectList(mcr.SelectAll(), "Id", "Name", scr.SelectById(addOrUpdateProductModel.Product.SubCategoryId).MainCategoryId);
            ViewBag.SubCat = new SelectList(mcr.SelectById(scr.SelectById(addOrUpdateProductModel.Product.SubCategoryId).MainCategoryId).SubCategories, "Id", "Name", ViewBag.Filter = filter);
            return View(addOrUpdateProductModel);
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

        public void DeleteProductImage(string id)
        {
            var image = ir.SelectById(id);
            if(image != null)
            {
                ir.Delete(image);
            }
        }
    }
}