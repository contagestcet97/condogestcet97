namespace condogestcet97.web.Data.Entities.Users
{
    public class UserCompanyManager
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public User User { get; set; } = default!;
        public Company Company { get; set; } = default!;
    }
}
