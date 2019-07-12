namespace Libraary.Web.Models.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookInputModel
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Summary { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000")]
        public decimal Fee { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        [Required]
        public string Categories { get; set; }
    }
}
