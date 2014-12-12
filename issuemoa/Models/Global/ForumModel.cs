using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using issuemoa.Models.Issues;

namespace issuemoa.Models.Global
{
    public class ForumModel
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
        public List<CommentModel> Comments { get; set; }
        public string NewComment { get; set; }
        public int BoardTypeId { get; set; }

        public ForumModel() {}
        public ForumModel(Forum thisForum)
        {
            SetProperty(thisForum);
        }
        public ForumModel(int itemId)
        {
            using (var db = new IssueMoaDB())
            {
                Forum ThisForum = (from f in db.Forums
                                   where f.ForumId == itemId
                                   select f).FirstOrDefault();
                if (ThisForum != null)
                {
                    SetProperty(ThisForum);
                }
            }
        }
        private void SetProperty(Forum thisForum)
        {
            ForumId = thisForum.ForumId;
            Title = thisForum.Title;
            Text = thisForum.Text;
            UploadDate = thisForum.UploadDate;
            ImageUrl = thisForum.ImageUrl;
            WriterId = thisForum.WriterId;
            LikeCount = thisForum.LikeCount;
            HateCount = thisForum.HateCount;
            ViewCount = thisForum.ViewCount;
            BoardId = thisForum.BoardId;
            Writer = thisForum.Writer;
            BoardTypeId = thisForum.Board.BoardTypeId;
            Comments = new List<CommentModel>();
            foreach (Comment ThisComment in thisForum.Comments)
            {
                CommentModel ThisCommentModel = new CommentModel(ThisComment);
                Comments.Add(ThisCommentModel);
            }
            SubNavBarModel = new SubNavBarModel(thisForum.Board.Issue.IssueId);
        }
        
        public void SaveComment()
        {
            int UserId = (int)HttpContext.Current.Session["UserId"];
            Comment ThisComment = new Comment();
            ThisComment.ForumId = ForumId;
            ThisComment.Text = NewComment;
            ThisComment.WriterId = UserId;

            using (var db = new IssueMoaDB())
            {
                db.Comments.Add(ThisComment);
                User ThisUser = (from u in db.Users
                                 where u.UserId == UserId
                                 select u).FirstOrDefault();
                if (ThisUser != null)
                {
                    ThisUser.PointsGained += 10;
                    HttpContext.Current.Session["PointsGained"] = ThisUser.PointsGained;
                }
                db.SaveChanges();
            }
        }

        public void DeleteComment(int commentId)
        {
            int UserId = (int)HttpContext.Current.Session["UserId"];
            using (var db = new IssueMoaDB())
            {
                Comment ThisComment = (from c in db.Comments
                                       where c.CommentId == commentId
                                       select c).FirstOrDefault();
                if(ThisComment != null && UserId == ThisComment.WriterId || UserModel.IsInRole("Administrator"))
                {
                    db.Comments.Remove(ThisComment);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteForum()
        {
            int UserId = (int)HttpContext.Current.Session["UserId"];
            using(var db = new IssueMoaDB())
            {
                Forum ThisForum = (from f in db.Forums
                                   where f.ForumId == ForumId
                                   select f).FirstOrDefault();
                if(ThisForum != null && (UserId == ThisForum.WriterId || UserModel.IsInRole("Administrator")))
                {
                    db.Forums.Remove(ThisForum);
                    db.SaveChanges();
                }                
            }
        }

        public void ViewCountUp()
        {
            using (var db = new IssueMoaDB())
            {
                Forum ThisForum = (from f in db.Forums
                                    where f.ForumId == ForumId
                                    select f).FirstOrDefault();
                if (ThisForum != null)
                {
                    ThisForum.ViewCount++;
                    db.SaveChanges();
                    ViewCount = ThisForum.ViewCount;
                }
            }
        }
    }
}