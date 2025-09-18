using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Users
{
    public class User : IdentityUser<int> // Inherits from IdentityUser with int as the key type instead of string
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        public bool? TwoFAEnabled { get; set; } = false; // Indicates if two-factor authentication is enabled -TODO

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Fiscal number is required for tax purposes.")]
        [MaxLength(18, ErrorMessage = "Fiscal number cannot exceed 18 characters.")]
        public string? FiscalNumber { get; set; }

        public int? CondoId { get; set;  }

        public int? ApartmentId { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Many-to-many relationship with Company
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Many-to-many relationship with Role
        public ICollection<Token> Tokens { get; set; } = new List<Token>(); // Tokens for authentication
        public ICollection<Recovery> Recoveries { get; set; } = new List<Recovery>(); // Recovery codes for two-factor authentication

        public ICollection<UserCompanyManager> ManagedCompanies { get; set; } = new List<UserCompanyManager>(); // Companies managed by the user

    }
}
