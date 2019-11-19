using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.VM
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public Comment Comment { get; set; }
        public bool commentTab { get; set; }
    }
}