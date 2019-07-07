namespace Libraary.Services
{
    using DTOs.Library;
    using Libraary.Data;
    using Libraary.Domain;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

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
            var users = this.userManager.GetUsersInRoleAsync("Owner").Result;

            return this.db
                .Libraries
                .Select(library => new LibraryDTO
                {
                    Id = library.Id,
                    Name = library.Name,
                    Owner = GetFirstAndLastNamesOfUser(library, users)
                })
                .ToList();
        }

        private string GetFirstAndLastNamesOfUser(Library library, IList<LibraaryUser> users)
        {
            return users.Where(l => l.LibraryId == library.Id)
                .Select(user => $"{user.FirstName} {user.LastName}").FirstOrDefault();
        }
    }
}
