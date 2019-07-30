namespace Libraary.Services
{
    using Domain;
    using DTOs.Author;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        Author GetAuthorByFullName(string name);

        bool Add(AddAuthorDTO authorDto);

        IEnumerable<AuthorViewDTO> GetAllByLibraryId(string libraryId);

        IEnumerable<string> GetAllAuthorsByName();
    }
}
