namespace Libraary.Web.Models.Address
{
    using System.ComponentModel.DataAnnotations;

    public class AddressInputModel
    {
        [Required]
        [Display(Name = "Country")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Town")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Town { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Street")]

        public string Street { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"(^\d{5}(?:[-\s]\d{4})?$)|(^\d{4})", ErrorMessage = "Invalid {0} code!")]
        [Display(Name = "Zip")]

        public string Zip { get; set; }
    }
}
