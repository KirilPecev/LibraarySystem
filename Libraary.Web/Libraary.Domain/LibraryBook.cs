namespace Libraary.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class LibraryBook
    {
        [Required]
        public string LibraryId { get; set; }
        public virtual Library Library { get; set; }

        [Required]
        public string BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
