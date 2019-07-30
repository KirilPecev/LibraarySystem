namespace Libraary.Services
{
    using DTOs.Book;
    using Libraary.Data;
    using Libraary.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly LibraaryDbContext db;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;

        public BookService(LibraaryDbContext db, IAuthorService authorService, IPublisherService publisherService, IUserService userService, ICategoryService categoryService)
        {
            this.db = db;
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.userService = userService;
            this.categoryService = categoryService;
        }

        public bool Add(AddBookDTO bookDto, string libraryId)
        {
            bool isNewBook = false;
            var book = this.db.Books.FirstOrDefault(b => b.Name == bookDto.Name);

            if (book == null)
            {
                isNewBook = true;
                var authors = bookDto.Authors;
                var publisher = this.publisherService.GetPublisher(bookDto.Publisher);
                var categories = bookDto.Categories;
                var rating = new Rating();

                book = new Book
                {
                    Name = bookDto.Name,
                    Publisher = publisher,
                    Rating = rating,
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
                    var currentCategory = this.categoryService.GetCategory(category);

                    book.BookCategories.Add(new BookCategory()
                    {
                        Category = currentCategory
                    });
                }

                foreach (var author in authors)
                {
                    var currentAuthor = this.authorService.GetAuthorByFullName(author);
                    book.AuthorBooks.Add(new AuthorBooks()
                    {
                        Author = currentAuthor
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

            var count = db.SaveChanges();
            return count != 0;
        }

        public IEnumerable<BookDTO> GetAllByLibraryId(string libraryId)
        {
            var a = this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId && lb.Book.IsRemoved == false)
                .Select(b => b.Book)
                .Include(x => x.Rating)
                .OrderBy(b => b.Name)
                .ThenBy(b => b.BookCategories
                    .Select(c => c.Category.CategoryName))
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Authors = string.Join(", ", book.AuthorBooks.Select(ab => ab.Author.ToString())),
                    Publisher = book.Publisher.Name,
                    Rating = this.CalculateRating(book),
                    Picture = book.PictureName,
                    IsRented = book.IsRented
                })
                .ToList();

            return a;
        }

        public IEnumerable<BookDTO> GetAllRented(string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId && lb.Book.IsRented == true && lb.Book.IsRemoved == false)
                .Select(b => b.Book)
                .Include(x => x.Rating)
                .OrderBy(b => b.Name)
                .ThenBy(b => b.BookCategories
                    .Select(c => c.Category.CategoryName))
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Authors = string.Join(", ", book.AuthorBooks.Select(ab => ab.Author.ToString())),
                    Publisher = book.Publisher.Name,
                    Rating = this.CalculateRating(book),
                    Picture = book.PictureName,
                    IsRented = book.IsRented
                })
                .ToList();
        }

        public BookDetailsDTO GetBookDetails(string bookId)
        {
            var currentBook = this.db
                .Books
                .Where(book => book.Id == bookId)
                .Include(x => x.Rating)
                .Select(book => new BookDetailsDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Authors = string.Join(", ", book.AuthorBooks.Select(ab => ab.Author.ToString())),
                    Publisher = book.Publisher.Name,
                    IsRented = book.IsRented,
                    Rating = this.CalculateRating(book),
                    Summary = book.Summary,
                    Picture = book.PictureName,
                    Categories = string.Join(", ", book.BookCategories.Select(x => x.Category.CategoryName)),
                })
                .FirstOrDefault();

            if (currentBook != null && currentBook.IsRented)
            {
                currentBook.User = this.db.Rents.SingleOrDefault(rent => rent.BookId == currentBook.Id)?.User.ToString();
                currentBook.RentDate = this.db.Rents.SingleOrDefault(rent => rent.BookId == currentBook.Id)
                    ?.IssuedOn
                    .ToString(CultureInfo.InvariantCulture);
            }

            return currentBook;
        }

        public IEnumerable<BookDTO> GetAllByAuthor(string authorId, string libraryId)
        {
            return this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId)
                .Where(b => b.Book.IsRemoved == false && b.Book.AuthorBooks.Any(author => author.Author.Id == authorId))
                .Select(b => b.Book)
                .OrderBy(b => b.Name)
                .ThenBy(b => b.BookCategories
                    .Select(c => c.Category.CategoryName))
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Authors = string.Join(", ", book.AuthorBooks.Select(ab => ab.Author.ToString())),
                    Publisher = book.Publisher.Name,
                    Rating = this.CalculateRating(book),
                    Picture = book.PictureName,
                    IsRented = book.IsRented
                })
                .ToList();
        }

        public void RemoveBook(string bookId)
        {
            var book = this.GetBookById(bookId);

            if (!book.IsRented)
            {
                book.IsRemoved = true;
            }

            this.db.SaveChanges();
        }

        public EditBookDto GetBookEditDetails(string bookId)
        {
            return this.db
                .Books
                .Where(book => book.Id == bookId)
                .Select(book => new EditBookDto
                {
                    Name = book.Name,
                    Authors = book.AuthorBooks.Select(ab => ab.Author.ToString()).ToArray(),
                    Publisher = book.Publisher.Name,
                    Summary = book.Summary,
                    Picture = book.PictureName,
                    Categories = book.BookCategories.Select(bc => bc.Category.CategoryName).ToArray()
                })
                .FirstOrDefault();
        }

        public bool EditBookById(string bookId, EditBookDto model, string libraryId)
        {
            var book = this.GetBookById(bookId);

            if (model.NewPicture != null)
            {
                var fileForDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", book.PictureName);
                File.Delete(fileForDeletePath);
            }

            this.db.Remove(book);

            var authors = model.Authors;
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
                var currentCategory = this.categoryService.GetCategory(category);

                editedBook.BookCategories.Add(new BookCategory()
                {
                    Category = currentCategory
                });
            }

            foreach (var author in authors)
            {
                var currentAuthor = this.authorService.GetAuthorByFullName(author);
                editedBook.AuthorBooks.Add(new AuthorBooks()
                {
                    Author = currentAuthor
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

        public IEnumerable<BookDTO> GetAll()
        {
            return this.db
                .Books
                .Where(book => book.IsRemoved == false && book.IsRented == false)
                .Include(x => x.Rating)
                .OrderBy(b => b.Name)
                .ThenBy(b => b.BookCategories
                    .Select(c => c.Category.CategoryName))
                .Select(book => new BookDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Authors = string.Join(", ", book.AuthorBooks.Select(ab => ab.Author.ToString())),
                    Publisher = book.Publisher.Name,
                    Rating = this.CalculateRating(book),
                    Picture = book.PictureName,
                    IsRented = book.IsRented
                })
                .ToList();
        }

        public IEnumerable<BookDTO> GetAllRentedByUserName(string user)
        {
            return this.userService.GetUser(user)
                 .RentedBooks
                 .Select(rent => new BookDTO
                 {
                     Id = rent.Book.Id,
                     Name = rent.Book.Name,
                     Authors = string.Join(", ", rent.Book.AuthorBooks.Select(ab => ab.Author.ToString())),
                     Publisher = rent.Book.Publisher.Name,
                     Rating = this.CalculateRating(rent.Book),
                     Picture = rent.Book.PictureName,
                     IsRented = rent.Book.IsRented,
                 })
                 .ToList();
        }

        public bool RentBook(string userName, string bookId)
        {
            var user = this.userService.GetUser(userName);

            user.RentedBooks.Add(new Rent
            {
                BookId = bookId,
                IssuedOn = DateTime.UtcNow
            });

            var currentBook = this.GetBookById(bookId)
                .IsRented = true;

            int count = this.db.SaveChanges();
            return count != 0;
        }

        public bool ReturnBook(string bookId)
        {
            this.GetBookById(bookId)
                 .IsRented = false;

            int count = this.db.SaveChanges();
            return count != 0;
        }

        public int GetCountOfAllBooks()
        {
            return this.db.Books.Count();
        }

        public void SaveRatingFromUser(string bookId, int modelRating)
        {
            var book = this.GetBookById(bookId);
            switch (modelRating)
            {
                case 1:
                    book.Rating.CountOfScoresOne++;
                    break;
                case 2:
                    book.Rating.CountOfScoresTwo++;
                    break;
                case 3:
                    book.Rating.CountOfScoresThree++;
                    break;
                case 4:
                    book.Rating.CountOfScoresFour++;
                    break;
                case 5:
                    book.Rating.CountOfScoresFive++;
                    break;
            }

            this.db.SaveChanges();
        }

        private Book GetBookById(string bookId)
        {
            return this.db
                .Books
                .SingleOrDefault(book => book.Id == bookId);
        }

        private int CalculateRating(Book book)
        {
            var dividerPoints = book.Rating.CountOfScoresOne + book.Rating.CountOfScoresTwo + book.Rating.CountOfScoresThree +
                                 book.Rating.CountOfScoresFour + book.Rating.CountOfScoresFive;

            if (dividerPoints == 0)
            {
                return 0;
            }

            var points =
                (decimal)(book.Rating.CountOfScoresOne + book.Rating.CountOfScoresTwo * 2 +
                           book.Rating.CountOfScoresThree * 3 +
                           book.Rating.CountOfScoresFour * 4 + book.Rating.CountOfScoresFive * 5) / dividerPoints;

            return (int)Math.Ceiling(points);
        }
    }
}
