namespace condogestcet97.web.Data.Entities.Users
{
    public class Recovery
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to the User entity
        public bool EmailSent { get; set; } // Indicates if the recovery email has been sent
        public string Token { get; set; } = default!; // Unique token for the recovery process

        public User User { get; set; } = default!; // Navigation property to the User entity
    }
}
