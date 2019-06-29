namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class BookCategory
    {
        [Required]
        public string BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
