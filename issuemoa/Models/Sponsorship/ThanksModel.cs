using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Global;
using issuemoa.Models.Database;
using System.IO;
using System.Numerics;

namespace issuemoa.Models.Sponsorship
{
    public class ThanksModel
    {
        public string name { get; set; }
        public int donatedPoint { get; set; }
        //public BigInteger donatedPoint { get; set; }
        public int pointsLeft { get; set; }
        public int issuePoint { get; set; }
        public string issueName { get; set; }
        public ThanksModel(){
            using (var db = new IssueMoaDB())
            {
                int userId = (int)HttpContext.Current.Session["UserId"]; // if user went directly to thanks page, this will be null

                PointHistory ph = (from h in db.PointHistories
                                   where h.UserId == userId
                                   orderby h.ChangeDate descending
                                   select h).FirstOrDefault();

                this.name = ph.User.Name;
                this.donatedPoint = -ph.ChangeAmount;
                this.pointsLeft = ph.User.PointsGained;
                this.issuePoint = ph.Issue.DonatedPoints;
                this.issueName = ph.Issue.IssueTitle;
            }
        }

        public string toString()
        {
            StringWriter thanks = new StringWriter();
            
            thanks.Write(this.name + "님이 기부해주신 " + 
                this.donatedPoint + "포인트는 " +
                this.issueName + " 단체에 기부되었습니다.\n\n\n"+
                this.issueName + " 단체에는 현재까지 총 " + 
                this.issuePoint + "포인트가 기부되었고, "+
                "현재 " + this.name + "님 께서는 " + this.pointsLeft+
                "포인트가 남아 있습니다.");


            return thanks.ToString();
        }
    }
}