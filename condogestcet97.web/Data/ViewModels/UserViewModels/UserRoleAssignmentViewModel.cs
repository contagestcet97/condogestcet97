using condogestcet97.web.Data.Entities.Users;

namespace condogestcet97.web.Data.ViewModels.User
{
    public class UserRoleAssignmentViewModel
    {
        public int UserId { get; set; }
        public List<Role> AllRoles { get; set; } = new();
        public List<int> SelectedRoleIds { get; set; } = new();
    }
}
