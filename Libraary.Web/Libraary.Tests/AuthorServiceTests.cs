namespace Libraary.Tests
{
    using Common;
    using Services;
    using Services.DTOs.Author;
    using System.Linq;
    using Xunit;

    public class AuthorServiceTests
    {
        private IAuthorService authorService;

        [Fact]
        public void AddAuthor_WithCorrectData_ShouldSuccessfullyAddAuthor()
        {
            string errorMessagePrefix = "AuthorService Add() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            AddAuthorDTO author = new AddAuthorDTO()
            {
                FirstName = "Test1",
                LastName = "Testov",
                Nationality = "Test"
            };

            bool actualResult = this.authorService.Add(author);
            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void AddAuthor_WithAuthorWhichIsAlreadyInDb_ShouldNotAddAuthor()
        {
            string errorMessagePrefix = "AuthorService Add() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            AddAuthorDTO author = new AddAuthorDTO()
            {
                FirstName = "Test",
                LastName = "Testov",
                Nationality = "Test"
            };

            bool actualResult = this.authorService.Add(author);
            Assert.True(!actualResult, errorMessagePrefix);
        }

        [Fact]
        public void GetAllAuthorsByName_ShouldReturnAllAuthors()
        {
            string errorMessagePrefix = "AuthorService GetAllAuthorsByName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var author = this.authorService.GetAllAuthorsByName();

            Assert.True(author.Count() != 0, errorMessagePrefix);
        }

        [Fact]
        public void GetAuthorByFullName_WithCorrectData_ShouldReturnAuthor()
        {
            string errorMessagePrefix = "AuthorService GetAuthorByFullName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var author = this.authorService.GetAuthorByFullName("Test Testov");

            Assert.True(author != null, errorMessagePrefix);
        }

        [Fact]
        public void GetAuthorByFullName_WithIncorrectData_ShouldReturnNull()
        {
            string errorMessagePrefix = "AuthorService GetAuthorByFullName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var author = this.authorService.GetAuthorByFullName("Test Testo");

            Assert.True(author == null, errorMessagePrefix);
        }

        [Fact]
        public void GetAllByLibraryId_WithCorrectData_ShouldReturnAllAuthors()
        {
            string errorMessagePrefix = "AuthorService GetAllByLibraryId() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var id = LibraaryDbContextInMemoryFactory.GetLibraryId(context);

            var author = this.authorService.GetAllByLibraryId(id);

            Assert.True(author != null, errorMessagePrefix);
        }


        [Fact]
        public void GetAllByLibraryId_WithInCorrectData_ShouldReturnNull()
        {
            string errorMessagePrefix = "AuthorService GetAllByLibraryId() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.authorService = new AuthorService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var author = this.authorService.GetAllByLibraryId("teest");

            Assert.True(author == null, errorMessagePrefix);
        }
    }
}