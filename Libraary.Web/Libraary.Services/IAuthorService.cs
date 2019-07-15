namespace Libraary.Services
{
    using Domain;
    using DTOs.Author;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        Author GetAuthor(string name);

        bool Add(AddAuthorDTO authorDto, string libraryId);

        IEnumerable<AuthorViewDTO> GetAll(string libraryId);
    }
}
