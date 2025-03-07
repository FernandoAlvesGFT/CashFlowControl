using CashFlowControl.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowControl.Core.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ConsolidatedBalance> ConsolidatedBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<ConsolidatedBalance>().ToTable("ConsolidatedBalance");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<ConsolidatedBalance>().HasKey(c => c.Id);
        }
    }
}
