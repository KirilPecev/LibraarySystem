namespace Libraary.Domain
{
    using System.Collections.Generic;

    public class Book : BaseModel<string>
    {
        public Book()
        {
            this.Rents = new List<Rent>();
            this.BookCategories = new HashSet<BookCategory>();
        }

        public string Name { get; set; }

        public string Summary { get; set; }

        public decimal Fee { get; set; }

        public string AuthorId { get; set; }
        public Author Author { get; set; }

        public bool IsRented { get; set; }

        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public int Rating { get; set; }

        public ICollection<Rent> Rents { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
