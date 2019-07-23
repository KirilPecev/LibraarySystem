namespace Libraary.Web.Models.Books
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class BookInputModel
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

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }

        [Required]
        public string[] Categories { get; set; }
    }
}
