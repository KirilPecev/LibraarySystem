namespace Libraary.Web.Models.Authors
{
    using System.ComponentModel.DataAnnotations;

    public class AuthorBindingModel
    {
        [Required]
        [MaxLength(25)]
        [StringLength(25, ErrorMessage = "Invalid name! Must be between {2} and {1}", MinimumLength = 1)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        [StringLength(25, ErrorMessage = "Invalid name! Must be between {2} and {1}", MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
    }
}
