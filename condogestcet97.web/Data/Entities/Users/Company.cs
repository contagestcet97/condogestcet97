using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Users
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Name { get; set; } // Name of the company, cannot be null

        [MaxLength(250)]
        public string? Address { get; set; } // Address of the company

        [MaxLength(9)]
        public string? Phone { get; set; } // Phone number for the company

        [MaxLength(9)]
        public string? FiscalNumber { get; set; } // Fiscal number for the company

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Navigation property for many-to-many relationship with User

    }
}
