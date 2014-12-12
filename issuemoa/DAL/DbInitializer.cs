using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.Models.Database;
using issuemoa.Models.Global;

namespace issuemoa.DAL
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<IssueMoaDB>
    {
        protected override void Seed(IssueMoaDB db)
        {
            var Users = new List<User>
            {
                new User{UserName = "admin", Name = "유호준", Password = PasswordHash.CreateHash("'k`uV(ZQn9dQ7D<"), Email = "soy0803@gmail.com", PointsGained = 1000, PointsDonated=300},
                new User{UserName = "test1", Name = "test1", Password = PasswordHash.CreateHash("12345678"), Email = "test1@gmail.com", PointsGained = 1000, PointsDonated=300},
                new User{UserName = "test2", Name = "test2", Password = PasswordHash.CreateHash("12345678"), Email = "test2@gmail.com", PointsGained = 1000, PointsDonated=300}
            };

            Users.ForEach(u => db.Users.Add(u));
            db.SaveChanges();

            var Role = new List<Role>
            {
                new Role{Description = "Administrator"},
                new Role{Description = "Manager"},
                new Role{Description = "General User"}
            };

            Role.ForEach(r => db.Roles.Add(r));
            db.SaveChanges();

            var UserRoles = new List<UserRole>
            {
                new UserRole{UserId = 1, RoleId = 1},
                new UserRole{UserId = 1, RoleId = 2},
                new UserRole{UserId = 1, RoleId = 3}
            };
            UserRoles.ForEach(u => db.UserRoles.Add(u));
            db.SaveChanges();

            var Issues = new List<Issue>
            {
                new Issue{IssueTitle = "세월호", StartDate = DateTime.Parse("2014 - 04 - 16"), ImageURL="home/issues/세월호.jpg", DonatedPoints = 500, IsTopIssue = true},
                new Issue{IssueTitle = "부정선거", StartDate = DateTime.Parse("2014 - 04 - 16"), ImageURL="home/issues/부정선거.jpg", DonatedPoints = 500, IsTopIssue = true},
                new Issue{IssueTitle = "개인정보", StartDate = DateTime.Parse("2014 - 04 - 16"), ImageURL="home/issues/개인정보.jpg", DonatedPoints = 500, IsTopIssue = true}
            };

            Issues.ForEach(i => db.Issues.Add(i));
            db.SaveChanges();

            var IssueSummaries = new List<IssueSummary>
            {
                new IssueSummary{SummaryTitle = "세월호 침몰사고", Summary = "세월호 침몰 사고(한자: 世越號沈沒事故)는 2014년 4월 16일 오전 8시 48분경 대한민국 전라남도 진도군 조도면 부근 해상에서 청해진해운 소속의 인천발 제주행 연안 여객선 세월호가 전복되어 침몰한 사고이다. 2014년 4월 18일에 세월호는 완전히 침몰하였다. 이 사고로 탑승인원 476명 중 295명이 사망하고 9명이 실종되었다.", IssueId = 1},
                new IssueSummary{SummaryTitle = "2012 대선 부정선거", Summary = "여기는 뭘 써야될지 모르겠다", IssueId = 2},
                new IssueSummary{SummaryTitle = "카카오톡 감청사건", Summary = "그러하다...", IssueId = 3}
            };
            IssueSummaries.ForEach(i => db.IssueSummaries.Add(i));
            db.SaveChanges();

            var BoardType = new List<BoardType>
            {
                new BoardType{Description = "discussion"},
                new BoardType{Description = "article"}
            };
            BoardType.ForEach(b => db.BoardTypes.Add(b));
            db.SaveChanges();

            var Board = new List<Board>
            {
                new Board{Title = "세월호 토론", IssueId = 1, BoardTypeId = 1},
                new Board{Title = "세월호 기사", IssueId = 1, BoardTypeId = 2},
                new Board{Title = "부정선거 토론", IssueId = 2, BoardTypeId = 1},
                new Board{Title = "부정선거 기사", IssueId = 2, BoardTypeId = 2},
                new Board{Title = "개인정보 토론", IssueId = 3, BoardTypeId = 1},
                new Board{Title = "개인정보 기사", IssueId = 3, BoardTypeId = 2}
            };

            Board.ForEach(b => db.Boards.Add(b));
            db.SaveChanges();
            var Forum = new List<Forum>
            {
                new Forum{Title = "세월호 침몰 사고 발생", Text = "16일 오전 8시 30분께 진도 관매도 부근 해상에서 여객선 세월호가 침몰하고 있다는 신고가 해경에 들어와 긴급 구조에 나선 가운데 지금까지 2명의 사망자가 발생했다.", WriterId = 1, LikeCount = 100, HateCount = 10, ViewCount = 1856, BoardId = 1},
                new Forum{Title = "다이빙벨", Text = "다이빙벨 투입", WriterId = 1, LikeCount = 1255, HateCount = 2, ViewCount = 1254, BoardId = 1},
                new Forum{Title = "세월호 침몰 사고 발생", Text = "16일 오전 8시 30분께 진도 관매도 부근 해상에서 여객선 세월호가 침몰하고 있다는 신고가 해경에 들어와 긴급 구조에 나선 가운데 지금까지 2명의 사망자가 발생했다.", WriterId = 1, LikeCount = 100, HateCount = 10, ViewCount = 1856, BoardId = 2},
                new Forum{Title = "다이빙벨", Text = "다이빙벨 투입", WriterId = 1, LikeCount = 1255, HateCount = 2, ViewCount = 1254, BoardId = 2},
            };

            Forum.ForEach(f => db.Forums.Add(f));
            db.SaveChanges();

            var Comment = new List<Comment>
            {
                new Comment{ForumId = 1, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 1, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 1, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 2, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 2, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 3, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 3, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 4, Text="테스트입니다", WriterId = 1},
                new Comment{ForumId = 4, Text="테스트입니다", WriterId = 1},
            };

            Comment.ForEach(c => db.Comments.Add(c));
            db.SaveChanges();

            var PointType = new List<PointType>
            {
                new PointType{Type = "출석"},
                new PointType{Type = "글쓰기"},
                new PointType{Type = "댓글"},
                new PointType{Type = "광고"},
                new PointType{Type = "기부"},
                new PointType{Type = "회원가입"}
            };
            PointType.ForEach(p => db.PointTypes.Add(p));
            db.SaveChanges();

            var PointHistory = new List<PointHistory>
            {
                new PointHistory{UserId = 1, PointTypeId=6, ChangeAmount=1000},
                new PointHistory{UserId = 2, PointTypeId=6, ChangeAmount=1000},
                new PointHistory{UserId = 3, PointTypeId=6, ChangeAmount=1000},
                new PointHistory{UserId = 1, PointTypeId=1, ChangeAmount=500},
                new PointHistory{UserId = 1, PointTypeId=2, ChangeAmount=500},
                new PointHistory{UserId = 1, PointTypeId=3, ChangeAmount=500},
                new PointHistory{UserId = 1, PointTypeId=4, ChangeAmount=500},
                new PointHistory{UserId = 1, PointTypeId=4, ChangeAmount=500},
                new PointHistory{UserId = 1, PointTypeId=5, ChangeAmount=-500, IssueId=1},
                new PointHistory{UserId = 1, PointTypeId=5, ChangeAmount=-500, IssueId=2},
                new PointHistory{UserId = 1, PointTypeId=5, ChangeAmount=-500, IssueId=3}
            };
            PointHistory.ForEach(p => db.PointHistories.Add(p));
            db.SaveChanges();
        }
    }
}