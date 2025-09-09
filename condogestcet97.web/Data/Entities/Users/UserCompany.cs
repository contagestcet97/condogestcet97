namespace condogestcet97.web.Data.Entities.Users
{
    public class UserCompany
    {
        public int UserId { get; set; } // foreign key to the User entity
        public int CompanyId { get; set; } // foreign key to the Company entity

        public User User { get; set; } = default!; // navigation property to the User entity
        public Company Company { get; set; } = default!; // navigation property to the Company entity
    }
}
