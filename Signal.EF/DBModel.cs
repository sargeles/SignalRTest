namespace Signal.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    
    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DBModel>());
        }

        public virtual DbSet<cus_visit> cus_visit { get; set; }
        public virtual DbSet<WIFI_MAC_DATA> wifi_mac_data { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cus_visit>()
                .Property(e => e.VISIT_MAC)
                .IsUnicode(false);

            modelBuilder.Entity<cus_visit>()
                .Property(e => e.CUS_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<cus_visit>()
                .Property(e => e.CUS_MOBILE)
                .IsUnicode(false);

            modelBuilder.Entity<cus_visit>()
                .Property(e => e.CUS_SEX)
                .IsUnicode(false);

            modelBuilder.Entity<cus_visit>()
                .Property(e => e.CUS_AGE_RANGE)
                .IsUnicode(false);

            modelBuilder.Entity<cus_visit>()
                .Property(e => e.CUS_INFO_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<cus_visit>()
                .Property(e => e.Guid)
                .IsUnicode(false);
        }
    }
}
