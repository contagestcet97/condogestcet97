using Microsoft.AspNetCore.Identity;

namespace condogestcet97.web.Data.Entities.Users
{
    public class User : IdentityUser<int> // Inherits from IdentityUser with int as the key type instead of string
    {
        public string? Name { get; set; }

        public bool? TwoFAEnabled { get; set; } = false; // Indicates if two-factor authentication is enabled -TODO

        public string? Address { get; set; }

        public string? FiscalNumber { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Many-to-many relationship with Company
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Many-to-many relationship with Role
        public ICollection<Token> Tokens { get; set; } = new List<Token>(); // Tokens for authentication
        public ICollection<Recovery> Recoveries { get; set; } = new List<Recovery>(); // Recovery codes for two-factor authentication
    }
}
