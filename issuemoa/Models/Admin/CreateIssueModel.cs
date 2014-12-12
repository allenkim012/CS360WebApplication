using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using issuemoa.Models.Global;

namespace issuemoa.Models.Admin
{
    public class CreateIssueModel
    {
        public string NewTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string SubTitle { get; set; }
        public string Summary { get; set; }
        public string DiscussionBoardTitle { get; set; }
        public string ArticleBoardTitle { get; set; }

        public CreateIssueModel() { }

        public void Save()
        {
            using (var db = new IssueMoaDB())
            {
                Issue NewIssue = new Issue();
                IssueSummary NewSummary = new IssueSummary();
                Board NewDiscussionBoard = new Board();
                Board NewArticleBoard = new Board();

                NewIssue.IssueTitle = NewTitle;
                NewIssue.StartDate = DateTime.Now;
                NewIssue.IsTopIssue = false;
                db.Issues.Add(NewIssue);

                NewSummary.SummaryTitle = SubTitle;
                NewSummary.Summary = Summary;
                NewSummary.IssueId = NewIssue.IssueId;
                db.IssueSummaries.Add(NewSummary);

                NewDiscussionBoard.IssueId = NewIssue.IssueId;
                NewDiscussionBoard.Title = DiscussionBoardTitle;
                NewDiscussionBoard.BoardTypeId = 1;

                NewArticleBoard.IssueId = NewIssue.IssueId;
                NewArticleBoard.Title = ArticleBoardTitle;
                NewArticleBoard.BoardTypeId = 2;
                db.Boards.Add(NewDiscussionBoard);
                db.Boards.Add(NewArticleBoard);

                db.SaveChanges();
            }
        }
        
    }
}