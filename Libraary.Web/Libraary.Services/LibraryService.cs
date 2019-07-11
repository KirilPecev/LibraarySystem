namespace Libraary.Services
{
    using DTOs.Library;
    using Libraary.Data;
    using Libraary.Domain;
    using Libraary.Services.DTOs.Librarian;
    using Libraary.Services.DTOs.Owner;
    using System.Collections.Generic;
    using System.Linq;

    public class LibraryService : ILibraryService
    {
        private readonly LibraaryDbContext db;
        private readonly IUserService userService;

        public LibraryService(LibraaryDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
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
            this.userService.ChangeRoles(dto.Email, "User", "Owner");

            var user = this.userService.GetUser(dto.Email);

            library.LibraryUsers.Add(user);

            int resultCount = this.db.SaveChanges();

            if (resultCount == 0)
            {
                return false;
            }

            return true;
        }

        public bool AddLibrarian(LibrarianDTO dto)
        {
            var library = this.db.Libraries.Find(dto.LibraryId);
            this.userService.ChangeRoles(dto.Email, "User", "Librarian");

            var user = this.userService.GetUser(dto.Email);

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
            var owners = this.userService.GetUsersInRole("Owner");

            return this.db
                .Libraries
                .Select(library => new LibraryDTO
                {
                    Id = library.Id,
                    Name = library.Name,
                    Owner = this.userService.GetFirstAndLastNamesOfUser(library, owners)
                })
                .ToList();
        }

        public IEnumerable<LibrarianDetailsDTO> GetAllLibrarians()
        {
            var librarians = this.userService.GetUsersInRole("Librarian");
            var librariansDto = librarians
                .Select(librarian => new LibrarianDetailsDTO()
                {
                    FullName = librarian.ToString(),
                    Address = this.db.Addresses.Find(librarian.AddressId).ToString()
                })
                .ToList();

            return librariansDto;
        }

        public LibraryDetailsDTO GetLibraryDetails(string libraryId)
        {
            var owners = this.userService.GetUsersInRole("Owner");

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
                     RentedBooksCount = lib.LibraryBooks.Where(b => b.Book.IsRented == true).Count(),
                     UsersCount = lib.LibraryUsers.Count,
                     Owner = currentOwner.ToString(),
                     OwnerAddress = ownerAddress,
                     OwnerPhone = currentOwner.PhoneNumber ?? "Not added",
                     Profit = lib.LibraryBooks.Where(book => book.Book.IsRented == true).Sum(book => book.Book.Fee) //TODO: Think about this fee
                 })
                 .SingleOrDefault();
        }
    }
}
