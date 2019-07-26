namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class AuthorBooks
    {
        [Required]
        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [Required]
        public string BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
