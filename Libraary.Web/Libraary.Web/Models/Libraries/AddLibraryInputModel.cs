namespace Libraary.Web.Models.Libraries
{
    using Address;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Library;

    public class AddLibraryInputModel
    {
        private const string ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        private const string PhoneErrorMessage = "Invalid phone number!";
        private const string PhoneRegex = @"08[789]\d{7}";

        [Required]
        [Display(Name = DisplayLibrary)]
        [DataType(DataType.Text)]
        [StringLength(LibraryNameMaxLength, ErrorMessage = ErrorMessage, MinimumLength = LibraryNameMinimumLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = DisplayAddress)]
        public AddressInputModel Address { get; set; }

        [Required]
        [Display(Name = DisplayPhone)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(PhoneRegex, ErrorMessage = PhoneErrorMessage)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = DisplayEmail)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
