namespace Libraary.Web.Models.Publishers
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Publisher;

    public class PublisherBindingModel
    {
        private const string ErrorMessage = "Invalid {0}! Must be between {2} and {1} symbols!";

        [Required]
        [Display(Name = DisplayPublisher)]
        [StringLength(PublisherNameMaxLength, ErrorMessage = ErrorMessage, MinimumLength = PublisherNameMinimumLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = DisplayUrlAddress)]
        public string URLAddress { get; set; }
    }
}
