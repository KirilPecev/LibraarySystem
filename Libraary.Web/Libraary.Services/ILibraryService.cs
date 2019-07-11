namespace Libraary.Services
{
    using DTOs.Library;
    using Libraary.Services.DTOs.Librarian;
    using Libraary.Services.DTOs.Owner;
    using System.Collections.Generic;

    public interface ILibraryService
    {
        string Add(AddLibraryDTO library);

        bool AddOwner(OwnerDTO dto);

        bool AddLibrarian(LibrarianDTO dto);

        IEnumerable<LibraryDTO> GetAll();

        LibraryDetailsDTO GetLibraryDetails(string libraryId);

        IEnumerable<LibrarianDetailsDTO> GetAllLibrarians();
    }
}
