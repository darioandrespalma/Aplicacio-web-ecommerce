using BodegaHogar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BodegaHogar.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Representa la tabla 'products' en Supabase
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo explícito usando Fluent API para que coincida con tu script SQL en minúsculas (PostgreSQL convención)
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "public"); // public schema
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Sku).HasColumnName("sku").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.BasePrice).HasColumnName("base_price").HasColumnType("decimal(10,2)");
                entity.Property(e => e.HasIva).HasColumnName("has_iva");
                entity.Property(e => e.IsImported).HasColumnName("is_imported");
                entity.Property(e => e.WeightKg).HasColumnName("weight_kg").HasColumnType("decimal(8,2)");
                entity.Property(e => e.VolumeM3).HasColumnName("volume_m3").HasColumnType("decimal(8,2)");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });
        }
    }
}