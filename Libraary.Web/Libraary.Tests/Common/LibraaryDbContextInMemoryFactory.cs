namespace Libraary.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public static class LibraaryDbContextInMemoryFactory
    {
        public static LibraaryDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<LibraaryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LibraaryDbContext(options);
        }

        public static void SeedDb(LibraaryDbContext context)
        {
            var library = new Library()
            {
                Name = "Test"
            };

            var author = new Author()
            {
                FirstName = "Test",
                LastName = "Testov",
                Nationality = "Test"
            };
            context.Authors.Add(author);

            var publisher = new Publisher()
            {
                Name = "Test",
                URLAddress = "https//facebook.com/"
            };
            context.Publishers.Add(publisher);

            context.Books.Add(new Book()
            {
                Name = "TestBook",
                Rating = new Rating()
                {
                    CountOfScoresFive = 2,
                    CountOfScoresFour = 3,
                    CountOfScoresOne = 1,
                    CountOfScoresThree = 2,
                    CountOfScoresTwo = 3
                },
                BookCategories = new List<BookCategory>()
                {
                    new BookCategory()
                    {
                        Category = new Category()
                        {
                            CategoryName = "Fantasy"
                        }
                    },
                },
                IsRented = false,
                IsRemoved = false,
                PictureName = "url",
                Publisher = publisher,
                Summary = "summary",
                AuthorBooks = new List<AuthorBooks>()
                {
                    new AuthorBooks()
                    {
                        Author = author,
                    }
                },
                LibraryBooks = new List<LibraryBook>()
                {
                    new LibraryBook()
                    {
                        Library = library
                    }
                }
            });

            context.SaveChanges();
        }

        public static string GetLibraryId(LibraaryDbContext context)
        {
            return context.Libraries.SingleOrDefault(x => x.Name == "Test").Id;
        }
    }
}

