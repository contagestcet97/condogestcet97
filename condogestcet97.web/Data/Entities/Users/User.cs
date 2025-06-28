namespace condogestcet97.web.Data.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; } = default!; // User/Building name
        public string? Email { get; set; } = default!;  // User's email address, should be unique and validated
        public string? Password { get; set; } = default!; // User's password, should be hashed and salted - TODO
        public bool? TwoFAEnabled { get; set; } = false; // Indicates if two-factor authentication is enabled
        public string? Address { get; set; } // User's address
        public string? Phone { get; set; } // User's phone number
        public string? FiscalNumber { get; set; } // Fiscal number for tax purposes
        public bool EmailConfirmed { get; set; } = false; // Indicates if the user's email is confirmed

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>(); // Many-to-many relationship with Company
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Many-to-many relationship with Role
        public ICollection<Token> Tokens { get; set; } = new List<Token>(); // Tokens for authentication
        public ICollection<Recovery> Recoveries { get; set; } = new List<Recovery>(); // Recovery codes for two-factor authentication
    }
}
