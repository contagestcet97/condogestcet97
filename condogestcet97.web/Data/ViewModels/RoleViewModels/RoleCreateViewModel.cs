using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.ViewModels.RoleViewModels
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; }
    }
}
