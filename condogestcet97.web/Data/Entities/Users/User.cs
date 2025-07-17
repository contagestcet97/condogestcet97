using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Users
{
    public class User : IdentityUser<int> // Inherits from IdentityUser with int as the key type
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; } // user name

        public bool? TwoFAEnabled { get; set; } = false; // Indicates if two-factor authentication is enabled -TODO

        [MaxLength(250)]
        public string? Address { get; set; } // User's address

        [MaxLength(9)]
        public string? FiscalNumber { get; set; } // Fiscal number for tax purposes

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Many-to-many relationship with Company
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Many-to-many relationship with Role
        public ICollection<Token> Tokens { get; set; } = new List<Token>(); // Tokens for authentication
        public ICollection<Recovery> Recoveries { get; set; } = new List<Recovery>(); // Recovery codes for two-factor authentication
    }
}
