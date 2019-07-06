namespace Libraary.Web.Models.Libraries
{
    using Address;
    using System.ComponentModel.DataAnnotations;

    public class AddLibraryInputModel
    {
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public AddressInputModel Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"08[789]\d{7}", ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; }
    }
}
