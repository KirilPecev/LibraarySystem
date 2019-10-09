namespace Libraary.Web.Models.Address
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Address;

    public class AddressInputModel
    {
        private const string ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        private const string ZipErrorMessage = "Invalid Zip code.";
        private const string ZipRegex = @"(^\d{5}(?:[-\s]\d{4})?$)|(^\d{4})";

        [Required]
        [Display(Name = CountryName)]
        [StringLength(CountryMaxLength, ErrorMessage = ErrorMessage, MinimumLength = CountryMinimumLength)]
        public string Country { get; set; }

        [Required]
        [Display(Name = TownName)]
        [StringLength(TownMaxLength, ErrorMessage = ErrorMessage, MinimumLength = TownMinimumLength)]
        public string Town { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = StreetName)]
        [StringLength(StreetMaxLength, ErrorMessage = ErrorMessage, MinimumLength = StreetMinimumLength)]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = ZipName)]
        [RegularExpression(ZipRegex, ErrorMessage = ZipErrorMessage)]
        public string Zip { get; set; }
    }
}
