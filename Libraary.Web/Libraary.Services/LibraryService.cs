namespace Libraary.Services
{
    using DTOs.Library;
    using Libraary.Data;
    using Libraary.Domain;

    public class LibraryService : ILibraryService
    {
        private readonly LibraaryDbContext db;

        public LibraryService(LibraaryDbContext db)
        {
            this.db = db;
        }

        public string Add(AddLibraryDTO libraryDTO)
        {
            Library library = new Library()
            {
                Name = libraryDTO.Name,
                PhoneNumber = libraryDTO.PhoneNumber,
                Address = new Address()
                {
                    Country = libraryDTO.Address.Country,
                    Street = libraryDTO.Address.Street,
                    Town = libraryDTO.Address.Town,
                    Zip = libraryDTO.Address.Zip
                }
            };

            this.db.Libraries.Add(library);
            this.db.SaveChanges();

            return library.Id ?? null;
        }
    }
}
