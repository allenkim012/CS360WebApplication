using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using issuemoa.Filters;
using issuemoa.Models.Global;
using issuemoa.Models.Admin;

namespace issuemoa.Controllers
{
    //[AdminFilter]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Board()
        {
            return View(new BoardAdminModel());
        }
        [HttpPost]
        public ActionResult Board(BoardAdminModel postedValues)
        {
            postedValues.Save();
            return View(new BoardAdminModel());
        }

        [HttpGet]
        public ActionResult Forum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forum()
        {
            return View();
        }
    }
}