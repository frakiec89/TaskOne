using Microsoft.EntityFrameworkCore;
using TaskTwo_Waybill.Model;

namespace TaskTwo_Waybill
{
    public class MsSqlContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;" +
                "Database=TaskTwo_Waybill_DB;trustservercertificate=true;MultipleActiveResultSets=True");
        }

        public DbSet<Model.Invoice> Invoices { get; set; }
        public DbSet<Model.InvoiceProducts>   InvoiceProducts { get; set; }
        public DbSet<Model.Product>  Products { get; set; }
        public DbSet<Model.ProductPriceHistory>  ProductPriceHistories { get; set; }
        public DbSet<Model.Supplier>  Suppliers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.From)
                .WithMany()
                .HasForeignKey("FromId")
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.To)
                .WithMany()
                .HasForeignKey("ToId")
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }

    }
}
