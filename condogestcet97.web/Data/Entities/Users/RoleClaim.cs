namespace condogestcet97.web.Data.Entities.Users
{
    public class RoleClaim
    {
        public int Id { get; set; }
        public int RoleId { get; set; } // The role identifier foreign key to which this claim belongs

        public Role Role { get; set; } = default!; // The role associated with this claim
    }
}
