namespace Libraary.Web.Models.Publishers
{
    using Address;
    using System.ComponentModel.DataAnnotations;

    public class PublisherBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Invalid name! Must be between {2} and {1} symbols!", MinimumLength = 5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public AddressInputModel Address { get; set; }

        [Required]
        [Url]
        [Display(Name = "Url address")]
        public string URLAddress { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
