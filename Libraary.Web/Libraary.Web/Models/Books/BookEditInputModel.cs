namespace Libraary.Web.Models.Books
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using CustomAttributes;

    public class BookEditInputModel
    {
        [Required]
        [StringLength(60, ErrorMessage = "Invalid name! Must be between {2} and {1}", MinimumLength = 5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "Invalid name! Must be between {2} and {1}", MinimumLength = 100)]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Required]
        public string[] Authors { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Image]
        [DataType(DataType.Upload)]
        public IFormFile NewPicture { get; set; }

        public string Picture { get; set; }

        [Required]
        public string[] Categories { get; set; }
    }
}
