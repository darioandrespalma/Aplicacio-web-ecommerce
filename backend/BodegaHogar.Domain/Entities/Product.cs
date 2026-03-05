using System;

namespace BodegaHogar.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool HasIva { get; set; }
        public bool IsImported { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? VolumeM3 { get; set; }
        public DateTime CreatedAt { get; set; }

        // Propiedad calculada: Precio con IVA si aplica (regla de negocio de Ecuador)
        public decimal PriceWithTax => HasIva ? BasePrice * 1.12m : BasePrice;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; } // Propiedad de navegación a la Categoría
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>(); // Lista de fotos
    }
}