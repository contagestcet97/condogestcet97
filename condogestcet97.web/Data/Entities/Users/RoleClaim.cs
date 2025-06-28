namespace condogestcet97.web.Data.Entities.Users
{
    public class RoleClaim
    {
        public int Id { get; set; } // Unique identifier for the role claim
        public int RoleId { get; set; } // The role identifier to which this claim belongs
        public Role Role { get; set; } = default!; // The role associated with this claim
    }
}
