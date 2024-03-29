﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace E_Commerce_Project.AspNetMVC.Tools
{
    public static class MailBody
    {
        public static string PartialRenderToString(this PartialViewResult partialView)
        {
            var httpContext = HttpContext.Current;

            if(httpContext == null)
            {
                throw new NotSupportedException("Hata Meydana Geldi ! Partialview'ı string'e çevirmek için HTML Context'e ihtiyacınız var.");
            }

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var sb = new StringBuilder();

            using(var sw = new StringWriter(sb))
            {
                using(var tw = new HtmlTextWriter(sw))
                {
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
                }
            }

            return sb.ToString();
        }
    }
}