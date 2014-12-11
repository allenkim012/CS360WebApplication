using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issuemoa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<string> issues = new List<string>(); //TODO: 모델에서 불러와야 함.
            issues.Add("세월호.jpg");
            issues.Add("부정선거.jpg");
            issues.Add("개인정보.jpg");

            ViewBag.issues = issues;
            return View();
        }
    }
}