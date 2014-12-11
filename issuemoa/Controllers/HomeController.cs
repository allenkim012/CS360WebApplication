using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using issuemoa.Models.Home;
using issuemoa.DAL;
using issuemoa.Models.Global;
using issuemoa.Models.Database;

namespace issuemoa.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //using(var db = new IssueMoaDB())
            //{
            //    List<User> users = db.Users.ToList();
            //    ViewBag.p = users.First().Password;
            //}
            
            HomeModel hm = new HomeModel();

            return View(hm);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            HomeModel hm = new HomeModel();

            return RedirectToAction("Index");
        }
    }
}