﻿namespace Libraary.Services
{
    using Data;
    using Domain;
    using DTOs.Librarian;
    using DTOs.Library;
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

        public bool Add(AddLibraryDTO libraryDTO)
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

            var result = AddOwner(library, libraryDTO.Email);

            return result;
        }

        public bool AddOwner(Library library, string userEmail)
        {
            this.userService.ChangeRoles(userEmail, "User", "Owner");
            this.userService.AddRole(userEmail, "Librarian");

            var user = this.userService.GetUser(userEmail);

            library.LibraryUsers.Add(user);

            this.db.Libraries.Add(library);
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
            var librarians = this.userService
                .GetUsersInRole("Librarian")
                .Where(user => user.AddressId != null);

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
            var librarians = this.userService.GetUsersInRole("Librarian");

            var currentOwner = owners
                .FirstOrDefault(u => u.LibraryId == libraryId);

            var currentAddress = this.db.Addresses
                .SingleOrDefault(address => address.Id == currentOwner.AddressId);

            var librariansCount = librarians.Count(x => x.LibraryId == libraryId);

            var authorsCount = this.db
                .Libraries
                .Where(lib => lib.Id == libraryId)
                .Select(x => x.Authors)
                .Distinct()
                .Count();

            return this.db.
             Libraries
             .Where(lib => lib.Id == libraryId)
             .Select(lib => new LibraryDetailsDTO
             {
                 Name = lib.Name,
                 Address = lib.Address.ToString(),
                 PhoneNumber = lib.PhoneNumber,
                 BooksCount = lib.LibraryBooks.Count,
                 RentedBooksCount = lib.LibraryBooks.Count(b => b.Book.IsRented == true),
                 Owner = currentOwner.ToString(),
                 OwnerAddress = currentAddress.ToString(),
                 OwnerPhone = currentOwner.PhoneNumber ?? "Not added",
                 LibrariansCount = librariansCount,
                 AuthorsCount = authorsCount
             })
             .SingleOrDefault();
        }

        public int GetCountOfAllLibraries()
        {
            return this.db.Libraries.Count();
        }
    }
}
