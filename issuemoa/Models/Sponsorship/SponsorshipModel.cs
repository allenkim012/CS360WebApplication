using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using System.Web.Mvc;
using issuemoa.Models.Global;
using System.Numerics;

namespace issuemoa.Models.Sponsorship
{
    public class SponsorshipModel
    {
        public List<Issue> IssueList{get;set;}
        public string SelectedIssue { get; set; }
        public int DoantedPoint { get; set; }
        //public BigInteger DoantedPoint { get; set; }
        
        public string message { get; set; }
        public IEnumerable<SelectListItem> IssueSelectList 
        { 
            get
            {
                List<SelectListItem> IssueSelectList = new List<SelectListItem>();
                foreach(Issue ThisIssue in IssueList)
                {
                    SelectListItem NewItem = new SelectListItem();
                    string stringId = Convert.ToString(ThisIssue.IssueId);
                    NewItem.Value = stringId;
                    NewItem.Text = ThisIssue.IssueTitle;
                    if (NewItem.Value == SelectedIssue)
                        NewItem.Selected = true;
                    IssueSelectList.Add(NewItem);
                }

                return IssueSelectList;
            }
        }
        public SponsorshipModel()
        {
            using (var db = new IssueMoaDB())
            {
                IssueList = db.Issues.ToList();
            }
            this.message = "";
        }

        public SponsorshipModel(int issueId)
        {
            using (var db = new IssueMoaDB())
            {
                SelectedIssue = Convert.ToString(issueId);
                IssueList = db.Issues.ToList();
            }
            this.message = "";
        }

        public bool Donate()
        {
            
            int IssueId = Convert.ToInt32(SelectedIssue);
            using (var db = new IssueMoaDB())
            {
                //decrease users' points
                if (HttpContext.Current.Session["UserId"] == null)   //------------Not login------------//
                    return false;
                int userId = (int)HttpContext.Current.Session["UserId"];
                User user = (from i in db.Users
                             where i.UserId == userId
                             select i).FirstOrDefault();

                if (user.PointsGained < DoantedPoint)
                {   //---------Not enough points---------------//
                    this.message = "**현재 충분한 포인트가 없습니다!! \"" + 
                        user.Name + "\"님 께서 현재 기부 가능한 포인트는 " + user.PointsGained + "포인트 입니다.";
                    return false;
                }
                if (DoantedPoint <= 0)
                {
                    this.message = "**최소 기부 가능한 포인트는 1포인트 입니다!!";
                    return false;
                }

                Issue ThisIssue = (from i in db.Issues
                                   where i.IssueId == IssueId
                                   select i).FirstOrDefault();
                Forum ThisForums = db.Forums.First();
                if (ThisIssue != null)
                {
                    ThisIssue.DonatedPoints += DoantedPoint;
                }
                

                
                user.PointsGained -= DoantedPoint;
                user.PointsDonated += DoantedPoint;
                HttpContext.Current.Session["PointsGained"] = user.PointsGained;
                HttpContext.Current.Session["PointsDonated"] = user.PointsDonated;
                
                //update PointHistory db
                PointHistory ph = new PointHistory();
                ph.ChangeAmount = -DoantedPoint;
                PointType pt = (from i in db.PointTypes
                                where i.Type == "기부"
                                select i).FirstOrDefault();
                ph.PointTypeId = pt.PointTypeId;
                ph.UserId = userId;
                ph.IssueId = IssueId;
                db.PointHistories.Add(ph);
                db.SaveChanges();
            }
            return true;
        }
    }
}