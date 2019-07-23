namespace Libraary.Services.DTOs.Library
{
    public class LibraryDetailsDTO
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int BooksCount { get; set; }

        public int RentedBooksCount { get; set; }

        public int UsersCount { get; set; }

        public string Owner { get; set; }

        public string OwnerAddress { get; set; }

        public string OwnerPhone { get; set; }

        public int LibrariesCount { get; set; }

        public int BooksCountForAllLibraries { get; set; }
    }
}
