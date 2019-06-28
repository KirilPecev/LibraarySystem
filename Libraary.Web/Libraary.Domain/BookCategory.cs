namespace Libraary.Domain
{
    public class BookCategory
    {
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
