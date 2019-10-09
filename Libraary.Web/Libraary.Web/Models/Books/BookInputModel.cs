namespace Libraary.Web.Models.Books
{
    using CustomAttributes;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Book;

    public class BookInputModel
    {
        private const string ErrorMessage = "Invalid {0}! Must be between {2} and {1} symbols.";

        [Required]
        [Display(Name = DisplayArticle)]
        [StringLength(ArticleMaxLength, ErrorMessage = ErrorMessage, MinimumLength = ArticleMinimumLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = DisplaySummary)]
        [StringLength(SummaryMaxLength, ErrorMessage = ErrorMessage, MinimumLength = SummaryMinimumLength)]
        public string Summary { get; set; }

        [Required]
        public string[] Authors { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Image]
        public IFormFile Picture { get; set; }

        [Required]
        public string[] Categories { get; set; }
    }
}
