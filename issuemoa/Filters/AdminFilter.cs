using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using issuemoa.DAL;

namespace issuemoa.Filters
{
    public class AdminFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["UserId"] == null)
            {
                return false;
            }
            int UserId = (int)httpContext.Session["UserId"];
            using (var db = new IssueMoaDB())
            {
                var result = from u in db.UserRoles
                             where u.UserId == UserId && u.RoleId == 1
                             select u;
                if(result.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}