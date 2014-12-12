using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using issuemoa.Models.Global;

namespace issuemoa.Models.Admin
{
    public class IssueAdminModel
    {
        public List<IssueModel> Issues { get; set; }
        public IssueAdminModel() 
        {
            Issues = new List<IssueModel>();
            using (var db = new IssueMoaDB())
            {
                db.Issues.ToList().ForEach(x => Issues.Add(new IssueModel(x)));
            }
        }

        public void Save()
        {
            foreach(IssueModel ThisModel in Issues)
            {
                ThisModel.Save();
            }
        }

        public void Delete(int issueId)
        {
            using (var db = new IssueMoaDB())
            {
                Issue ThisIssue = (from i in db.Issues
                                   where i.IssueId == issueId
                                   select i).FirstOrDefault();
                if (ThisIssue != null)
                {
                    var PointHistories = (from p in db.PointHistories
                                        where p.IssueId == ThisIssue.IssueId
                                        select p).ToList();

                    PointHistories.ForEach(x => db.PointHistories.Remove(x));
                    db.SaveChanges();
                    db.Issues.Remove(ThisIssue);
                    db.SaveChanges();
                }
            }
        }
    }
}