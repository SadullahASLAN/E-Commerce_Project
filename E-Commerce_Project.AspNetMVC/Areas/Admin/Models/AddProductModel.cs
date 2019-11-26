using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Areas.Admin.Models
{
    public class AddProductModel
    {
        public Product Product { get; set; }
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }
}