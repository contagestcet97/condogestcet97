namespace condogestcet97.web.Data.Entities.Users
{
    public class UserRole
    {
        public int UserId { get; set; } // The user identifier foreign key, cannot be null
        public int RoleId { get; set; } // The role identifier foreign key, cannot be null

        public Role Role { get; set; } = default!; // The role associated with this user
        public User User { get; set; } = default!; // The user associated with this role
    }
}
