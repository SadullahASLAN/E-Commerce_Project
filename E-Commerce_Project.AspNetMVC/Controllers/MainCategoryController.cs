using E_Commerce_Project.BLL.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Project.AspNetMVC.Controllers
{
    public class MainCategoryController : Controller
    {
        // GET: MainCategory
        public ActionResult _GetMainCategories()
        {
            MainCategoryRepository mcc = new MainCategoryRepository();
            var categories = mcc.SelectAll();
            return PartialView(categories);
        }
    }
}