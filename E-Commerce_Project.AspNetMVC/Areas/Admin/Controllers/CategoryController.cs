using E_Commerce_Project.BLL.Repositories.Repository;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Project.AspNetMVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        MainCategoryRepository mcr = new MainCategoryRepository();
        SubCategoryRepository scr = new SubCategoryRepository();

        public ActionResult Categories()
        {
            return View(mcr.SelectAll());
        }

        public ActionResult MainCategoryAddOrUpdate(string CategoryName, string id)
        {
            if(id != null && !string.IsNullOrWhiteSpace(CategoryName))
            {
                var mainCat = mcr.SelectById(id);
                mainCat.Name = CategoryName;
                mcr.AddOrUpdate(mainCat);
            }
            else if(!string.IsNullOrWhiteSpace(CategoryName))
            {
                var mainCat = new MainCategory();
                mainCat.Name = CategoryName;
                mcr.AddOrUpdate(mainCat);
            }
            return RedirectToAction("Categories");
        }

        public ActionResult MainCategoryDelete(string id)
        {
            if(mcr.SelectById(id) != null)
            {
                mcr.Delete(mcr.SelectById(id));
            }
            return RedirectToAction("Categories");
        }

        public ActionResult SubCategoryAddOrUpdate(string CategoryName, string MainCatId, string id)
        {
            if(id == null)
            {
                if(!String.IsNullOrWhiteSpace(CategoryName))
                {
                    var subCat = new SubCategory() { Name = CategoryName, MainCategoryId = MainCatId };
                    scr.AddOrUpdate(subCat);
                }
            }
            else
            {
                if(!String.IsNullOrWhiteSpace(CategoryName))
                {
                    var subCat = scr.SelectById(id);
                    subCat.Name = CategoryName;
                    scr.AddOrUpdate(subCat);
                }
            }
            return RedirectToAction("Categories");
        }

        public ActionResult SubCategoryDelete(string id)
        {
            if(scr.SelectById(id) != null)
            {
                scr.Delete(scr.SelectById(id));
            }
            return RedirectToAction("Categories");
        }
    }
}