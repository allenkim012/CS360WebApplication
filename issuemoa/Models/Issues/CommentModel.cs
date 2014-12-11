using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;

namespace issuemoa.Models.Issues
{
    public class CommentModel
    {
        public CommentModel() { }
        public int CommentId { get; set; }
        public DateTime UploadDate { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public int HateCount { get; set; }
        public int WriterId { get; set; }
        public string WriterName { get; set; }
        public int ForumId { get; set; }

        public CommentModel(Comment thisComment)
        {
            SetProperty(thisComment);
        }
        private void SetProperty(Comment thisComment)
        {
            CommentId = thisComment.CommentId;
            UploadDate = thisComment.UploadDate;
            Text = thisComment.Text;
            LikeCount = thisComment.LikeCount;
            HateCount = thisComment.HateCount;
            WriterId = thisComment.WriterId;
            ForumId = thisComment.ForumId;
            WriterName = thisComment.Writer.UserName;
        }
    }
}