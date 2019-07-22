namespace Libraary.Services
{
    using DTOs.Book;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<BookDTO> GetAllByLibraryId(string libraryId);

        IEnumerable<BookDTO> GetAllRented(string libraryId);

        bool Add(AddBookDTO bookDto, string libraryId);

        BookDetailsDTO GetBookDetails(string bookId);

        IEnumerable<BookDTO> GetAllByAuthor(string authorId, string libraryId);

        void RemoveBook(string bookId);

        IEnumerable<BookDTO> GetAllRentedByUserName(string user);

        EditBookDto GetBookEditDetails(string bookId);

        bool EditBookById(string bookId, EditBookDto mappedModel, string libraryId);

        IEnumerable<BookDTO> GetAll();

        bool RentBook(string userName, string bookId);

        bool ReturnBook(string bookId);
    }
}
