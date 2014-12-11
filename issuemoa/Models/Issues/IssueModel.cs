using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;

namespace issuemoa.Models.Issues
{
    public class IssueModel
    {
        public Issue Issue { get; set; }
        public int DiscussionBoardId { get; set; }
        public int ArticleBoardId { get; set; }
        public IssueSummary IssueSummary { get; set; }
        public SubNavBarModel SubNavBarModel { get; set; }

        public IssueModel() { }

        public IssueModel(int IssueId)
        {
            SubNavBarModel = new SubNavBarModel(IssueId);
            using (var db = new IssueMoaDB())
            {
                IssueSummary = (from i in db.IssueSummaries
                                where i.IssueId == IssueId
                                select i).FirstOrDefault();
                Issue = IssueSummary.Issue;
            }
        }
    }
}