using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;

namespace issuemoa.Models.Home
{
    using Models.Database;
    public class HomeModel
    {
        public List<IssueSummaryModel> IssueSummaryList = new List<IssueSummaryModel>();

        public HomeModel()
        {
            using (var db = new IssueMoaDB())
            {
                var Result = db.IssueSummaries.ToArray();
                foreach(var ThisSummary in Result)
                {
                    IssueSummaryModel ThisModel = new IssueSummaryModel(ThisSummary);
                    IssueSummaryList.Add(ThisModel);
                }
            }
        }
        public int getTotalDonatedPoint()
        {
            using (var db = new IssueMoaDB())
            {
                var potints = from i in db.Issues
                             select i.DonatedPoints;
                return potints.Sum();
            }            
        }

    }

    public class IssueSummaryModel
    {
        public int IssueSummaryId { get; set; }
        public string SummaryTitle { get; set; }
        public string Summary { get; set; }
        public Issue Issue { get; set; }

        public IssueSummaryModel(IssueSummary thisIssue)
        {
            IssueSummaryId = thisIssue.IssueSummaryId;
            SummaryTitle = thisIssue.SummaryTitle;
            Summary = thisIssue.Summary;
            Issue = thisIssue.Issue;
        }
    }

}