namespace condogestcet97.web.Data.Entities.Users
{
    public class Token
    {
        public int Id { get; set; } // Unique identifier for the token, cannot be null
        public int UserId { get; set; } // Foreign key to User entity, cannot be null
        public User User { get; set; } = default!; // Navigation property to User entity nullable
    }
}
