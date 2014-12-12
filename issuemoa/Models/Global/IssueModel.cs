using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using issuemoa.Models.Issues;

namespace issuemoa.Models.Global
{
    public class IssueModel
    {
        public Issue Issue { get; set; }
        public int IssueId { get; set; }
        public IssueSummary IssueSummary { get; set; }
        public SubNavBarModel SubNavBarModel { get; set; }

        public IssueModel() { }

        public IssueModel(Issue issue)
        {
            Issue = issue;
            IssueId = issue.IssueId;
            SubNavBarModel = new SubNavBarModel(IssueId);
            using (var db = new IssueMoaDB())
            {
                IssueSummary = (from i in db.IssueSummaries
                                where i.IssueId == issue.IssueId
                                select i).FirstOrDefault();
            }
        }

        public IssueModel(int issueId)
        {
            SubNavBarModel = new SubNavBarModel(issueId);
            IssueId = issueId;
            using (var db = new IssueMoaDB())
            {
                IssueSummary = (from i in db.IssueSummaries
                                where i.IssueId == issueId
                                select i).FirstOrDefault();
                Issue = IssueSummary.Issue;
            }
        }

        public void Save()
        {
            using (var db = new IssueMoaDB())
            {
                IssueSummary ThisIssueSummary = (from i in db.IssueSummaries
                                                 where i.IssueId == IssueId
                                                 select i).FirstOrDefault();

                if (ThisIssueSummary != null)
                {
                    Issue ThisIssue = ThisIssueSummary.Issue;
                    ThisIssue.IssueTitle = Issue.IssueTitle;
                    ThisIssue.IsTopIssue = Issue.IsTopIssue;
                    ThisIssueSummary.SummaryTitle = IssueSummary.SummaryTitle;
                    ThisIssueSummary.Summary = IssueSummary.Summary;
                    db.SaveChanges();
                }
            }
        }
    }
}