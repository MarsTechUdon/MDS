using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MDS.Models;

namespace MDS.Fillter
{
    public class MenuFillter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                var route = new RouteValueDictionary();
                route["controller"] = "Login";
                route["action"] = "Index";
                filterContext.Result = new RedirectToRouteResult(route);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"]
                .ToString();
            string controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"]
                .ToString();
            var menuList = (List<UserMenuModel>)httpContext.Session["UserMenu"];
            var checkAction = menuList.FirstOrDefault(x => x.Action == actionName);
            var checkController = menuList.FirstOrDefault(x => x.Controller == controllerName);
            return (checkAction != null && checkController != null);
        }
    }
}