namespace condogestcet97.web.Data.Entities.Users
{
    public class Company
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? FiscalNumber { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Navigation property for many-to-many relationship with User
    }
}
