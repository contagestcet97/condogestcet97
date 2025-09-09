using condogestcet97.web.Data.Entities.Users;

namespace condogestcet97.web.Data.ViewModels.User
{
    public class UserCompanyAssignmentViewModel
    {
        public int UserId { get; set; }
        public List<Company> AllCompanies { get; set; } = new();
        public List<int> SelectedCompanyIds { get; set; } = new();
    }
}
