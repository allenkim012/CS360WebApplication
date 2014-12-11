using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issuemoa.Controllers
{
    public class IssuesController : Controller
    {
        [HttpGet]
        public ActionResult Issues(int? itemId)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Articles(int? itemId)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Discussion(int? itemId)
        {
            return View();
        }
    }
}