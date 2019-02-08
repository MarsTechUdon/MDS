using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MDS.Fillter
{
    public class NeedLoginFirst : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                var route = new RouteValueDictionary();
                route["controller"] = "Login";
                route["action"] = "FirstLogin";
                filterContext.Result = new RedirectToRouteResult(route);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["userSession"] != null;
        }
    }
}