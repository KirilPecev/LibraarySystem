﻿namespace Libraary.Services
{
    using DTOs.Library;
    using System.Collections.Generic;

    public interface ILibraryService
    {
        string Add(AddLibraryDTO library);

        bool AddOwner(OwnerDTO dto);

        bool AddLibrarian(LibrarianDTO dto);

        IEnumerable<LibraryDTO> GetAll();

        LibraryDetailsDTO GetLibraryDetails(string libraryId);
    }
}
