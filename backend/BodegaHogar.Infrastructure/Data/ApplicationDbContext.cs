using BodegaHogar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BodegaHogar.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Representan las tablas en Supabase
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Mapeo de Productos
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

                entity.Property(e => e.CategoryId).HasColumnName("category_id");
            });

            // 2. Mapeo de Categorías (ESTE ES EL QUE FALTA)
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories", "public");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Slug).HasColumnName("slug").IsRequired().HasMaxLength(100);
                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
            });

            // 3. Mapeo de Imágenes de Productos
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_images", "public");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").IsRequired().HasMaxLength(500);
                entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
                entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            });
        }
    }
}