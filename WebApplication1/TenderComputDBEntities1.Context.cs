namespace TenderComp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class TenderComputDBEntities1 : DbContext
    {
        public TenderComputDBEntities1()
            : base("name=TenderComputDBEntities1")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblTender> tblTenders { get; set; }
        public DbSet<tblCompony> tblComponies { get; set; }
    }
}