namespace Libraary.Services.DTOs.Book
{
    public class AddBookDTO
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public byte[] Picture { get; set; }

        public string Categories { get; set; }
    }
}
