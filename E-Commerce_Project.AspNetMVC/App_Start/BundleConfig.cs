using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace E_Commerce_Project.AspNetMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Contents/CSS").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/all.min.css",
                "~/Content/css/site.css"
                ));
            bundles.Add(new ScriptBundle("~/Scripts").Include(
                "~/Scripts/js/jquery-3.3.1.slim.min.js",
                "~/Scripts/js/popper.min.js",
                "~/Scripts/js/bootstrap.min.js",
                "~/Scripts/js/site.js"
                ));
        }
    }
}