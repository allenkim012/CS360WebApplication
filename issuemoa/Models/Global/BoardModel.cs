using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using issuemoa.Models.Issues;

namespace issuemoa.Models.Global
{
    public class BoardModel
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public List<ForumModel> Forums { get; set; }
        public SubNavBarModel SubNavBarModel { get; set; }
        public string NewTitle { get; set; }
        public Issue Issue { get; set; }

        public BoardModel() { }
        public BoardModel(int itemId) 
        {
            using(var db = new IssueMoaDB())
            {
                Board ThisBoard = (from b in db.Boards
                                 where b.BoardId == itemId
                                 select b).FirstOrDefault();
                if(ThisBoard != null)
                {
                    SetProperty(ThisBoard);
                }
            }
        }
        public BoardModel(Board board)
        {
            SetProperty(board);
        }
        
        private void SetProperty(Board thisBoard)
        {
            BoardId = thisBoard.BoardId;
            Title = thisBoard.Title;
            SubNavBarModel = new SubNavBarModel(thisBoard.Issue.IssueId);
            Forums = new List<ForumModel>();
            thisBoard.ListForum.OrderByDescending(x=>x.UploadDate).ToList().ForEach(f => Forums.Add(new ForumModel(f)));
            Issue = thisBoard.Issue;
        }

        public void Save()
        {
            using (var db = new IssueMoaDB())
            {
                Board ThisBoard = (from b in db.Boards
                                   where b.BoardId == BoardId
                                   select b).FirstOrDefault();
                if(ThisBoard != null)
                {
                    ThisBoard.Title = NewTitle;
                    db.SaveChanges();
                }
            }
        }
    }
}