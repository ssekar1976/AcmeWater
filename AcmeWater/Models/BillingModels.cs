namespace AcmeWater.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BillingModels : DbContext
    {
        public BillingModels()
            : base("name=AcmeWaterDbConnection")
        {
        }

        public virtual DbSet<Billing_Log> Billing_Log { get; set; }
        public virtual DbSet<Billing_Txns> Billing_Txns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Billing_Log>()
                .Property(e => e.BillingAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Billing_Log>()
                .HasMany(e => e.Billing_Txns)
                .WithRequired(e => e.Billing_Log)
                .HasForeignKey(e => e.BillingLogId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Billing_Txns>()
                .Property(e => e.PastDue)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Billing_Txns>()
                .Property(e => e.CurrentDue)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Billing_Txns>()
                .Property(e => e.EmailedTo)
                .IsUnicode(false);

            modelBuilder.Entity<Billing_Txns>()
                .Property(e => e.EmailText)
                .IsUnicode(false);
        }
    }
}
