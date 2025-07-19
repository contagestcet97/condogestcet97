using condogestcet97.web.Data.Entities.Users;

namespace condogestcet97.web.Data.ViewModels.UserViewModels
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? TwoFAEnabled { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FiscalNumber { get; set; }
        public bool EmailConfirmed { get; set; }

        public List<Company> AllCompanies { get; set; } = new();
        public List<int> SelectedCompanyIds { get; set; } = new();

    }

}
