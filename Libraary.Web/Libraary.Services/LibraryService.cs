namespace Libraary.Services
{
    using DTOs.Library;
    using Libraary.Data;
    using Libraary.Domain;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
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

            int resultCount = this.db.SaveChanges();

            if (resultCount == 0)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<LibraryDTO> GetAll()
        {
            var owners = this.userManager.GetUsersInRoleAsync("Owner").Result;

            return this.db
                .Libraries
                .Select(library => new LibraryDTO
                {
                    Id = library.Id,
                    Name = library.Name,
                    Owner = GetFirstAndLastNamesOfUser(library, owners)
                })
                .ToList();
        }

        public LibraryDetailsDTO GetLibraryDetails(string libraryId)
        {
            var owners = this.userManager.GetUsersInRoleAsync("Owner").Result;
            var currentOwner = owners
                .FirstOrDefault(u => u.LibraryId == libraryId);

            string ownerAddress = "Not added";
            if (currentOwner.Address != null)
            {
                ownerAddress = currentOwner.Address.ToString();
            }

            return this.db.
                 Libraries
                 .Where(lib => lib.Id == libraryId)
                 .Select(lib => new LibraryDetailsDTO
                 {
                     Name = lib.Name,
                     Address = lib.Address.ToString(),
                     PhoneNumber = lib.PhoneNumber,
                     BooksCount = lib.LibraryBooks.Count,
                     UsersCount = lib.LibraryUsers.Count,
                     Owner = currentOwner.ToString(),
                     OwnerAddress = ownerAddress,
                     OwnerPhone = currentOwner.PhoneNumber ?? "Not added"
                 })
                 .SingleOrDefault();
        }

        private string GetFirstAndLastNamesOfUser(Library library, IList<LibraaryUser> owners)
        {
            return owners.Where(l => l.LibraryId == library.Id)
                .FirstOrDefault()
                .ToString();
        }
    }
}
