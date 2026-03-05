using System;

namespace BodegaHogar.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty; // Usaremos esto para el login básico
        public string PasswordHash { get; set; } = string.Empty; // Para desarrollo local
        public string IdentificationNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}