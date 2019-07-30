namespace Libraary.Services
{
    using Domain;
    using DTOs.Librarian;
    using DTOs.Library;
    using System.Collections.Generic;

    public interface ILibraryService
    {
        bool Add(AddLibraryDTO libraryDTO);

        bool AddOwner(Library library, string userEmail);

        bool AddLibrarian(LibrarianDTO dto);

        IEnumerable<LibraryDTO> GetAll();

        LibraryDetailsDTO GetLibraryDetails(string libraryId);

        IEnumerable<LibrarianDetailsDTO> GetAllLibrarians();

        int GetCountOfAllLibraries();
    }
}
