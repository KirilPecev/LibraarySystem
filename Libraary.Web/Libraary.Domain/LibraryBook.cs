namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class LibraryBook
    {
        [Required]
        public string LibraryId { get; set; }
        public Library Library { get; set; }

        [Required]
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
