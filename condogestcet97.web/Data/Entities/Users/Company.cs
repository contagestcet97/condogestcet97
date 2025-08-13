using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Users
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(25, ErrorMessage = "Name cannot exceed 25 characters.")]
        public string? Name { get; set; }

        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string? Address { get; set; }

        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string? Phone { get; set; }

        [MaxLength(18, ErrorMessage = "Fiscal number cannot exceed 18 characters.")]
        public string? FiscalNumber { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Navigation property for many-to-many relationship with User

        public ICollection<UserCompanyManager> Managers { get; set; } = new List<UserCompanyManager>(); // Navigation property for managers of the company

    }
}
