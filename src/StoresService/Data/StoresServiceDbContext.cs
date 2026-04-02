using StoresService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace StoresService.Data;

public class StoresServiceDbContext(DbContextOptions<StoresServiceDbContext> options) : DbContext(options)
{
    public DbSet<StoreEntity> Stores => Set<StoreEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoreEntity>(entity =>
        {
            entity.ToTable("stores");
            entity.HasKey(store => store.Id);

            entity.Property(store => store.Id)
                .HasColumnName("id")
                .HasColumnType("varchar");

            entity.Property(store => store.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .IsRequired();

            entity.HasIndex(store => store.Name)
                .HasDatabaseName("IX_stores_name");
        });
    }
}
