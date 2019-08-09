namespace Libraary.Web.Models.Publishers
{
    using System.ComponentModel.DataAnnotations;

    public class PublisherBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Invalid name! Must be between {2} and {1} symbols!", MinimumLength = 5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = "Url address")]
        public string URLAddress { get; set; }
    }
}
