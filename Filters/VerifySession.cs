//using starteAlkemy.Controllers;
//using starteAlkemy.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace starteAlkemy.Filters
//{

//    public class VerifySession : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            var admin = (Admin)HttpContext.Current.Session["Admin"];

//            if (admin == null)
//            {
//                if (filterContext.Controller is AdminController == false)
//                {

//                    filterContext.HttpContext.Response.Redirect("~/Admin/Index");



//                }
//            }
//            else if (admin != null && filterContext.Controller is AdminController == true)
//            {

//                filterContext.HttpContext.Response.Redirect("~/Project/Create");

//            }


//            base.OnActionExecuting(filterContext);
//        }
//    }

//}