namespace Libraary.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants.Book;

    public class Book : BaseModel<string>
    {
        public Book()
        {
            this.AuthorBooks = new HashSet<AuthorBooks>();
            this.BookCategories = new HashSet<BookCategory>();
            this.LibraryBooks = new List<LibraryBook>();
            this.UserRents = new List<Rent>();
        }

        [Required]
        [MaxLength(ArticleMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(SummaryMaxLength)]
        public string Summary { get; set; }

        [Required]
        public bool IsRented { get; set; }

        [Required]
        public string PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public string RatingId { get; set; }
        public virtual Rating Rating { get; set; }

        [Required]
        public string PictureName { get; set; }

        public bool IsRemoved { get; set; }

        public virtual ICollection<AuthorBooks> AuthorBooks { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }

        public virtual ICollection<Rent> UserRents { get; set; }
    }
}
