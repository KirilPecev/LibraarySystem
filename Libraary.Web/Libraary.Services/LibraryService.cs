namespace Libraary.Services
{
    using DTOs.Library;
    using Libraary.Data;
    using Libraary.Domain;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;

    public class LibraryService : ILibraryService
    {
        private readonly LibraaryDbContext db;
        private readonly UserManager<LibraaryUser> userManager;

        public LibraryService(LibraaryDbContext db, UserManager<LibraaryUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public string Add(AddLibraryDTO libraryDTO)
        {
            Address address = new Address()
            {
                Country = libraryDTO.Address.Country,
                Street = libraryDTO.Address.Street,
                Town = libraryDTO.Address.Town,
                Zip = libraryDTO.Address.Zip
            };

            Library library = new Library()
            {
                Name = libraryDTO.Name,
                PhoneNumber = libraryDTO.PhoneNumber,
                Address = address
            };

            this.db.Libraries.Add(library);
            this.db.SaveChanges();

            return library.Id ?? null;
        }

        public bool AddOwner(OwnerDTO dto)
        {
            var library = this.db.Libraries.Find(dto.LibraryId);
            var user = this.db.Users.SingleOrDefault(u => u.Email == dto.Email);

            this.userManager.AddToRoleAsync(user, "Owner");
            this.userManager.RemoveFromRoleAsync(user, "User");

            library.LibraryUsers.Add(user);

            this.db.SaveChanges();

            return true;
        }
    }
}
