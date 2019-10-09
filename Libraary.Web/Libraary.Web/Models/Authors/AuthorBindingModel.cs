namespace Libraary.Web.Models.Authors
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Author;

    public class AuthorBindingModel
    {
        private const string ErrorMessage = "Invalid {0}! Must be between {2} and {1} symbols.";

        [Required]
        [Display(Name = DisplayFirstName)]
        [StringLength(FirstNameMaxLength, ErrorMessage = ErrorMessage, MinimumLength = FirstNameMinimumLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = DisplayLastName)]
        [StringLength(LastNameMaxLength, ErrorMessage = ErrorMessage, MinimumLength = LastNameMinimumLength)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = NationalityName)]
        [StringLength(NationalityMaxLength, ErrorMessage = ErrorMessage, MinimumLength = NationalityMinimumLength)]
        public string Nationality { get; set; }
    }
}
