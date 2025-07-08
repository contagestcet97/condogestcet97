using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = default!; // The name of the role

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Collection of user roles associated with this role
        public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>(); // Collection of role claims associated with this role
    }
}
