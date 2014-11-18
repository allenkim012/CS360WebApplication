namespace OneADayDating
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Dating : DbContext
    {
        public Dating()
            : base("name=Dating")
        {
        }

        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .Property(e => e.phone)
                .IsFixedLength();
        }
    }
}
