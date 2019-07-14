namespace Libraary.Services
{
    using DTOs.Book;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<BookDTO> GetAll(string libraryId);

        IEnumerable<BookDTO> GetAllRented(string libraryId);

        bool Add(AddBookDTO bookDto, string libraryId);

        BookDetailsDTO GetBookDetails(string bookId);

        IEnumerable<BookDTO> GetAllByAuthor(string authorId, string libraryId);
    }
}
