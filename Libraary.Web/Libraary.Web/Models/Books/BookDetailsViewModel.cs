namespace Libraary.Web.Models.Books
{
    public class BookDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Authors { get; set; }

        public bool IsRented { get; set; }

        public string Publisher { get; set; }

        public int Rating { get; set; }

        public string Picture { get; set; }

        public string Categories { get; set; }
    }
}
