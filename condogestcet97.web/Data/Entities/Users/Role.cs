using Microsoft.AspNetCore.Identity;

namespace condogestcet97.web.Data.Entities.Users
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Collection of user roles associated with this role
        public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>(); // Collection of role claims associated with this role
    }
}
