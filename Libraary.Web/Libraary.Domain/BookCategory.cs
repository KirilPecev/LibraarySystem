namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class BookCategory
    {
        [Required]
        public string BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
