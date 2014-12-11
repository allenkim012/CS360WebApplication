using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.DAL;
using issuemoa.Models.Database;
using issuemoa.Models.Global;

namespace issuemoa.Models.Admin
{
    public class BoardAdminModel
    {
        public List<BoardModel> Boards { get; set; }

        public BoardAdminModel()
        {
            Boards = new List<BoardModel>();
            using(var db = new IssueMoaDB())
            {
                db.Boards.ToList().ForEach(x => Boards.Add(new BoardModel(x)));
            }
        }

        public void Save()
        {
            foreach(BoardModel ThisModel in Boards)
            {
                ThisModel.Save();
            }
        }
    }
}