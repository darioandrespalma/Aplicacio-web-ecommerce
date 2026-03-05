namespace BodegaHogar.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty; // Para URLs amigables (ej: "muebles-de-sala")
        public int? ParentCategoryId { get; set; } // Nullable porque las categorías principales no tienen padre
    }
}