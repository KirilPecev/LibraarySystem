namespace Libraary.Services
{
    using DTOs.Book;
    using Libraary.Data;
    using Libraary.Domain;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.IO;
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
                var categories = bookDto.Categories;

                book = new Book
                {
                    Name = bookDto.Name,
                    Author = author,
                    Publisher = publisher,
                    Rating = 0,
                    PictureName = bookDto.Picture.FileName,
                    IsRented = false,
                    IsRemoved = false,
                    Summary = bookDto.Summary
                };

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures");

                filePath += $@"\{book.PictureName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    bookDto.Picture.CopyTo(stream);
                }

                publisher.Books.Add(book);

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

            return count != 0;
        }

        public IEnumerable<BookDTO> GetAll(string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId && lb.Book.IsRemoved == false)
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
                    Picture = book.PictureName
                })
                .ToList();
        }

        public IEnumerable<BookDTO> GetAllRented(string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId && lb.Book.IsRented == true && lb.Book.IsRemoved == false)
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
                    Picture = book.PictureName
                })
                .ToList();
        }

        public BookDetailsDTO GetBookDetails(string bookId)
        {
            return this.db
                 .Books
                 .Where(book => book.Id == bookId)
                 .Select(book => new BookDetailsDTO
                 {
                     Name = book.Name,
                     Author = book.Author.ToString(),
                     Publisher = book.Publisher.Name,
                     IsRented = book.IsRented,
                     Rating = book.Rating,
                     Summary = book.Summary,
                     Picture = book.PictureName,
                     Categories = string.Join(", ", book.BookCategories.Select(x=>x.Category.CategoryName))
                 })
                 .FirstOrDefault();
        }

        public IEnumerable<BookDTO> GetAllByAuthor(string authorId, string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId)
                .Include(x => x.Book)
                .Where(b => b.Book.IsRemoved == false && b.Book.AuthorId == authorId)
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
                    Picture = book.PictureName
                })
                .ToList();
        }

        public void RemoveBook(string bookId)
        {
            this.db.Books.SingleOrDefault(b => b.Id == bookId).IsRemoved = true;
            this.db.SaveChanges();
        }

        public EditBookDto GetBookEditDetails(string bookId)
        {
            return this.db
                .Books
                .Where(book => book.Id == bookId)
                .Include(x => x.BookCategories)
                .Select(book => new EditBookDto
                {
                    Name = book.Name,
                    Author = book.Author.ToString(),
                    Publisher = book.Publisher.Name,
                    Summary = book.Summary,
                    Picture = book.PictureName,
                    Categories = book.BookCategories.Select(bc => bc.Category.CategoryName).ToArray()
                })
                .FirstOrDefault();
        }

        public bool EditBookById(string bookId, EditBookDto model, string libraryId)
        {
            var book = this.db.Books.SingleOrDefault(b => b.Id == bookId);

            this.db.Remove(book);

            var author = this.authorService.GetAuthor(model.Author);
            var publisher = this.publisherService.GetPublisher(model.Publisher);
            var categories = model.Categories;

            var pictureName = book.PictureName;
            if (model.NewPicture != null)
            {
                pictureName = model.NewPicture.FileName;
            }

            var editedBook = new Book
            {
                Name = model.Name,
                Author = author,
                Publisher = publisher,
                Rating = book.Rating,
                PictureName = pictureName,
                IsRented = book.IsRented,
                IsRemoved = book.IsRemoved,
                Summary = model.Summary
            };

            if (model.NewPicture != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures");

                filePath += $@"\{editedBook.PictureName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.NewPicture.CopyTo(stream);
                }
            }

            publisher.Books.Add(editedBook);

            foreach (var category in categories)
            {
                editedBook.BookCategories.Add(new BookCategory
                {
                    Category = new Category
                    {
                        CategoryName = category
                    }
                });
            }

            editedBook.LibraryBooks.Add(new LibraryBook
            {
                LibraryId = libraryId
            });

            this.db.Add(editedBook);
            int count = this.db.SaveChanges();
            return count != 0;
        }
    }
}
