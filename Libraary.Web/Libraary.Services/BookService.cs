namespace Libraary.Services
{
    using DTOs.Book;
    using Libraary.Data;
    using Libraary.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly LibraaryDbContext db;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;

        public BookService(LibraaryDbContext db, IAuthorService authorService, IPublisherService publisherService)
        {
            this.db = db;
            this.authorService = authorService;
            this.publisherService = publisherService;
        }

        public bool Add(AddBookDTO bookDto, string libraryId)
        {
            bool isNewBook = false;
            Book book = this.db.Books.FirstOrDefault(b => b.Name == bookDto.Name
                           && b.Author.ToString() == bookDto.Author
                           && b.Publisher.Name == bookDto.Publisher);

            if (book == null)
            {
                isNewBook = true;
                var author = this.authorService.GetAuthor(bookDto.Author);
                var publisher = this.publisherService.GetPublisher(bookDto.Publisher);
                var categories = bookDto.Categories.Split(' ').ToArray();

                book = new Book
                {
                    Name = bookDto.Name,
                    Author = author,
                    Publisher = publisher,
                    Picture = bookDto.Picture,
                    Rating = 0,
                    IsRented = false,
                    Fee = bookDto.Fee,
                    Summary = bookDto.Summary
                };

                foreach (var category in categories)
                {
                    book.BookCategories.Add(new BookCategory
                    {
                        Category = new Category
                        {
                            CategoryName = category
                        }
                    });
                }
            }

            book.LibraryBooks.Add(new LibraryBook
            {
                LibraryId = libraryId
            });

            if (isNewBook)
            {
                this.db.Add(book);
            }

            int count = this.db.SaveChanges();

            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<BookDTO> GetAll(string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId)
                .Select(b => b.Book)
                .OrderBy(b => b.Name)
                .ThenBy(b => b.BookCategories
                    .Select(c => c.Category.CategoryName))
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Author = book.Author.ToString(),
                    Publisher = book.Publisher.Name,
                    Rating = book.Rating,
                    Picture = Convert.ToBase64String(book.Picture)
                })
                .ToList();
        }

        public IEnumerable<BookDTO> GetAllRented(string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId && lb.Book.IsRented == true)
                .Select(b => b.Book)
                .OrderBy(b => b.Name)
                .ThenBy(b => b.BookCategories
                    .Select(c => c.Category.CategoryName))
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Author = book.Author.ToString(),
                    Publisher = book.Publisher.Name,
                    Rating = book.Rating,
                    Picture = Convert.ToBase64String(book.Picture)
                })
                .ToList();
        }
    }
}
