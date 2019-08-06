namespace Libraary.Web.Models.Books
{
    public class BookViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Authors { get; set; }

        public string Publisher { get; set; }

        public int Rating { get; set; }

        public string Picture { get; set; }

        public bool IsRented { get; set; }

        public string RentedOn { get; set; }

        public string ReturnedOn { get; set; }
    }
}
