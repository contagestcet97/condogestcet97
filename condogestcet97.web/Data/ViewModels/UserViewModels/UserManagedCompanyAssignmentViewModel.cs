using condogestcet97.web.Data.Entities.Users;

namespace condogestcet97.web.Data.ViewModels.UserViewModels
{
    public class UserManagedCompanyAssignmentViewModel
    {
        public int UserId { get; set; }
        public List<Company> AllCompanies { get; set; } = new();
        public List<int> SelectedManagedCompanyIds { get; set; } = new();
    }
}
