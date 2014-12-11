using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;

namespace issuemoa.Models.Issues
{
    public class NewForumModel
    {
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime UploadDate { get; set; }
        public string ImageUrl { get; set; }
        public int WriterId { get; set; }
        public User Writer { get; set; }
        public int LikeCount { get; set; }
        public int HateCount { get; set; }
        public int ViewCount { get; set; }
        public int BoardId { get; set; }
        public SubNavBarModel SubNavBarModel { get; set; }
        public int BoardTypeId { get; set; }

        public NewForumModel() { }
        public NewForumModel(int itemId)
        {
            using (var db = new IssueMoaDB())
            {
                Board ThisBoard = (from b in db.Boards
                                   where b.BoardId == itemId
                                   select b).FirstOrDefault();
                if(ThisBoard != null)
                {
                    LikeCount = 0;
                    HateCount = 0;
                    ViewCount = 0;
                    BoardId = ThisBoard.BoardId;
                    BoardTypeId = ThisBoard.BoardType.BoardTypeId;
                    SubNavBarModel = new SubNavBarModel(ThisBoard.IssueId);
                }
            }
        }

        public void Save()
        {
            using (var db = new IssueMoaDB())
            {
                int UserId = (int)HttpContext.Current.Session["UserId"];
                Forum ThisForum = new Forum();
                ThisForum.LikeCount = 0;
                ThisForum.HateCount = 0;
                ThisForum.ViewCount = 0;
                ThisForum.BoardId = BoardId;
                ThisForum.Text = Text;
                ThisForum.Title = Title;
                ThisForum.WriterId = UserId;
                db.Forums.Add(ThisForum);

                
                User ThisUser = (from u in db.Users
                                 where u.UserId == UserId
                                 select u).FirstOrDefault();
                if(ThisUser != null)
                {
                    ThisUser.PointsGained += 100;
                    HttpContext.Current.Session["PointsGained"] = ThisUser.PointsGained;
                }
                db.SaveChanges();
            }
        }
    }
}