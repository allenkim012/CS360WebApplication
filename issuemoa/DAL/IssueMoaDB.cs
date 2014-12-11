namespace issuemoa.DAL
{
    using Models.Database;
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public partial class IssueMoaDB : DbContext
    {
        public IssueMoaDB()
            : base("IssueMoaDB")
        {
            Database.SetInitializer<IssueMoaDB>(new DbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueSummary> IssueSummaries { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<PointType> PointTypes { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<BoardType> BoardTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
