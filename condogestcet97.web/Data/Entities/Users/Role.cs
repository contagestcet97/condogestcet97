using Microsoft.AspNetCore.Identity;

namespace condogestcet97.web.Data.Entities.Users
{
    public class Role : IdentityRole<int>
    {
        //[Required]
        //[StringLength(50)]
        //public string Name { get; set; } // Name of the role, inherited from IdentityRole

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Collection of user roles associated with this role
        public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>(); // Collection of role claims associated with this role
    }
}
