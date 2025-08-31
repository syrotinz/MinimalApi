using Microsoft.EntityFrameworkCore;
using TestProject.Domain;

namespace TestProject.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        var e = modelBuilder.Entity<Product>();
        e.ToTable("products");

        e.HasKey(p => p.Id);
        e.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        e.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        e.HasIndex(p => p.Name).IsUnique();

        e.Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("numeric")
            .HasPrecision(18, 2)
            .IsRequired();

        e.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("timezone('utc', now())")
            .ValueGeneratedOnAdd();
    }
}
