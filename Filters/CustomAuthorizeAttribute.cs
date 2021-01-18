using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;
using starteAlkemy.Repository.Implements;
using starteAlkemy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace starteAlkemy.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool allowedroles;
        private  IAdminRepository adminRepository = new AdminRepository(new StartContext());

        public CustomAuthorizeAttribute(bool roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var email = Convert.ToString(httpContext.Session["Email"]);
            var password = Convert.ToString(httpContext.Session["Password"]);
            if ( !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {

                Admin admin = adminRepository.Get(password, email);
                if (admin != null)
                {
                    AdminViewModel adminView = new AdminViewModel(admin);
                    if (adminView.Active == allowedroles) return true;
                }

                
            }

            //using (var context = new SqlDbContext())
            //{
            //    var userRole = (from u in context.Users
            //                    join r in context.Roles on u.RoleId equals r.Id
            //                    where u.UserId == userId
            //                    select new
            //                    {
            //                        r.Name
            //                    }).FirstOrDefault();
            //    foreach (var role in allowedroles)
            //    {
            //        if (role == userRole.Name) return true;
            //    }



            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Admin" },
                    { "action", "Login" }
               });
        }
    }
}