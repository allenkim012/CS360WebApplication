namespace issuemoa
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class IssueMoaDB : DbContext
    {
        public IssueMoaDB()
            : base("name=IssueMoaDB")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
