using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.ViewModels.User
{
    public class UserCreateViewModel
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool TwoFAEnabled { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FiscalNumber { get; set; }
    }

}
