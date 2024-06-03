using Crypto.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Crypto.Core.Persistence;

public class DataContext(IConfiguration configuration) : DbContext
{
    public DbSet<TokenEntity> Tokens { get; set; }
    public DbSet<TokenHistoryEntity> TokensHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(configuration.GetConnectionString("CryptDatabase"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TokenEntity>(entity =>
        {
            entity.HasKey(e => e.TokenCode);
            entity.Property(e => e.TokenCode).HasColumnType("VARCHAR").HasMaxLength(25);
            entity.Property(e => e.PurchaseValue);
        })
            .Ignore("CurrentValue");

        modelBuilder.Entity<TokenHistoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TokenCode).HasColumnType("VARCHAR").HasMaxLength(25);
            entity.Property(e => e.RecordedDate).HasColumnType("DATETIME");
            entity.Property(e => e.Value).HasConversion<double>();
        });

    }
}