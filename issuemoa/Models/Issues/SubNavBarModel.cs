using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;

namespace issuemoa.Models.Issues
{
    public class SubNavBarModel
    {
        public int IssueId { get; set; }
        public int ArticleBoardId { get; set; }
        public int DiscussionBoardId { get; set; }
        public string IssueName { get; set; }
        public string ImageUrl { get; set; }

        public SubNavBarModel(int issueId)
        {
            using (var db = new IssueMoaDB())
            {
                var Issue = (from i in db.Issues
                             where i.IssueId == issueId
                             select i).FirstOrDefault();
                if(Issue != null)
                {
                    IssueId = Issue.IssueId;
                    IssueName = Issue.IssueTitle;
                    ImageUrl = Issue.ImageURL;
                }
                var Boards = from b in db.Boards
                             where b.IssueId == IssueId
                             select b;
                foreach (Board ThisBoard in Boards)
                {
                    if (ThisBoard.BoardTypeId == 1)
                    {
                        DiscussionBoardId = ThisBoard.BoardId;
                    }
                    else if (ThisBoard.BoardTypeId == 2)
                    {
                        ArticleBoardId = ThisBoard.BoardId;
                    }
                }
            }
        }
    }
}