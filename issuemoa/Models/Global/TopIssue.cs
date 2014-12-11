using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.Models.Database;
using issuemoa.DAL;

namespace issuemoa.Models.Global
{
    public class TopIssue
    {
        public static List<Issue> TopIssues
        {
            get
            {
                using (var db = new IssueMoaDB())
                {
                    List<Issue> IssueList = new List<Issue>();
                    IssueList = (from i in db.Issues
                                where i.IsTopIssue == true
                                select i).ToList();
                    return IssueList;
                }
            }
        }
    }
}