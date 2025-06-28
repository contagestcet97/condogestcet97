namespace condogestcet97.web.Data.Entities.Users
{
    public class UserRole
    {
        public int UserId { get; set; } // The user identifier
        public User User { get; set; } = default!; // The user associated with this role
        public int RoleId { get; set; } // The role identifier
        public Role Role { get; set; } = default!; // The role associated with this user
    }
}
