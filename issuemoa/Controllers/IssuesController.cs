using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using issuemoa.Models.Issues;
using issuemoa.Models.Global;

namespace issuemoa.Controllers
{
    public class IssuesController : Controller
    {
        [HttpGet]
        public ActionResult Index(int itemId)
        {
            IssueModel ThisModel = new IssueModel(itemId);
            return View(ThisModel);
        }
        [HttpGet]
        public ActionResult Articles(int itemId)
        {
            BoardModel ThisBoard = new BoardModel(itemId);
            return View(ThisBoard);
        }
        [HttpGet]
        public ActionResult Discussion(int itemId)
        {
            BoardModel ThisBoard = new BoardModel(itemId);
            return View(ThisBoard);
        }
        [HttpGet]
        public ActionResult Forum(int itemId)
        {
            ForumModel ThisForum = new ForumModel(itemId);
            ThisForum.ViewCountUp();
            return View(ThisForum);
        }
        [HttpGet]
        public ActionResult Write(int itemId)
        {
            NewForumModel ThisNewForum = new NewForumModel(itemId);
            return View(ThisNewForum);
        }
        [HttpPost]
        public ActionResult Write(string postAction, NewForumModel postedValues)
        {
            postedValues.Save();
            if (postedValues.BoardTypeId == 1)
                return RedirectToAction("Discussion", new { itemId = postedValues.BoardId });
            else
                return RedirectToAction("Articles", new { itemId = postedValues.BoardId });
        }
        [HttpPost]
        public ActionResult WriteComment(string postAction, ForumModel postedValues)
        {
            postedValues.SaveComment();
            return RedirectToAction("Forum", new { itemId = postedValues.ForumId });
        }
        [HttpPost]
        public ActionResult DeleteComment(string action, ForumModel postedValues)
        {
            int CommentId = Convert.ToInt32(action);
            postedValues.DeleteComment(CommentId);
            return RedirectToAction("Forum", new { itemId = postedValues.ForumId });
        }
        [HttpPost]
        public ActionResult DeleteForum(ForumModel postedValues)
        {
            postedValues.DeleteForum();
            if (postedValues.BoardTypeId == 1)
                return RedirectToAction("Discussion", new { itemId = postedValues.BoardId });
            else
                return RedirectToAction("Articles", new { itemId = postedValues.BoardId });
        }
    }
}