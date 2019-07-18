namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book : BaseModel<string>
    {
        public Book()
        {
            this.Rents = new List<Rent>();
            this.BookCategories = new HashSet<BookCategory>();
            this.LibraryBooks = new List<LibraryBook>();
        }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Summary { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public Author Author { get; set; }

        [Required]
        public bool IsRented { get; set; }

        [Required]
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Rating { get; set; }

        [Required]
        public string PictureName { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
