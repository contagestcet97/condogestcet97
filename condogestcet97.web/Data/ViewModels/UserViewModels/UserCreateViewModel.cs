using condogestcet97.web.Data.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.ViewModels.User
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool TwoFAEnabled { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "phone number is required.")]
        [MaxLength(13, ErrorMessage = "Phone number cannot exceed 13 characters.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Fiscal number is required for tax purposes.")]
        [MaxLength(10, ErrorMessage = "Fiscal number cannot exceed 10 characters.")]
        public string? FiscalNumber { get; set; }

        public List<Company> AllCompanies { get; set; } = new();
        public List<int> SelectedCompanyIds { get; set; } = new();
    }

}
