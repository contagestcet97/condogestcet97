namespace condogestcet97.web.Data.Entities.Users
{
    public class Company
    {
        public int Id { get; set; } // Unique identifier for the company, cannot be null
        public string? Name { get; set; } = default!; // Name of the company, cannot be null
        public string? Address { get; set; } // Address of the company
        public string? Phone { get; set; } // Phone number for the company
        public string? FiscalNumber { get; set; } // Fiscal number for the company
        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Navigation property for many-to-many relationship with User

    }
}
