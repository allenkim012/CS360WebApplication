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
        public ActionResult Forum(ForumModel postedValues)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Issue()
        {
            return View(new IssueAdminModel());
        }

        [HttpPost]
        public ActionResult Issue(string Delete, IssueAdminModel postedValues)
        {
            if (Delete == null)
            {
                postedValues.Save();
            }

            else
            {
                int DeleteId = Convert.ToInt32(Delete);
                postedValues.Delete(DeleteId);
            }
            
            return View(new IssueAdminModel());
        }

        [HttpGet]
        public ActionResult CreateIssue()
        {
            return View(new CreateIssueModel());
        }
        
        [HttpPost]
        public ActionResult CreateIssue(CreateIssueModel postedValues)
        {
            postedValues.Save();
            return RedirectToAction("Issue");
        }
    }
}