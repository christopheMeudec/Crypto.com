using Crypto.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Crypto.Core.Persistence;

public class DataContext(IConfiguration configuration) : DbContext
{
    public DbSet<TokenEntity> Tokens { get; set; }
    public DbSet<TokenExchangeHistoryEntity> TokensExchangeHistory { get; set; }
    public DbSet<TokenValueHistoryEntity> TokensValueHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(configuration.GetConnectionString("CryptoDatabase"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TokenEntity>(entity =>
            {
                entity.HasKey(e => e.TokenCode);
                entity.Property(e => e.TokenCode).HasColumnType("VARCHAR").HasMaxLength(25);
            });

        modelBuilder.Entity<TokenEntity>()
            .HasMany(c => c.ValueHistory)
            .WithOne(e => e.Token)
            .HasForeignKey(e => e.TokenCode)
            .IsRequired(false);

        modelBuilder.Entity<TokenEntity>()
            .HasMany(c => c.ExchangeHistory)
            .WithOne(e => e.Token)
            .HasForeignKey(e => e.TokenCode)
            .IsRequired(false);

        modelBuilder.Entity<TokenValueHistoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TokenCode).HasColumnType("VARCHAR").HasMaxLength(25);
            entity.Property(e => e.RecordedDate).HasColumnType("DATETIME");
            entity.Property(e => e.Value).HasConversion<double>();
        });

        modelBuilder.Entity<TokenExchangeHistoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TokenCode).HasColumnType("VARCHAR").HasMaxLength(25);
            entity.Property(e => e.RecordedDate).HasColumnType("DATETIME");
            entity.Property(e => e.Value).HasConversion<double>();
            entity.Property(e => e.ExchangeType).HasConversion<int>();
        });

    }
}