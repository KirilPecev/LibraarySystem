namespace Libraary.Tests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Services;
    using Services.DTOs.Book;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Controllers;
    using Web.Models.Books;
    using Web.Profiles;
    using Xunit;

    public class BooksControllerTests
    {
        [Fact]
        public void All_ReturnsAViewResult_WithAListOfAllBooks()
        {
            // Arrange
            var mockBookServices = new Mock<IBookService>();
            var mockUserServices = new Mock<IUserService>();

            var automapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });

            IMapper mapper = automapper.CreateMapper();

            mockBookServices.Setup(repo => repo.GetAll())
                .Returns(GetTestSessions());

            var controller = new BooksController(mapper, mockUserServices.Object, mockBookServices.Object);

            // Act
            var result = controller.All();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BookViewModel>>(
                viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        private IEnumerable<BookDTO> GetTestSessions()
        {
            var sessions = new List<BookDTO>();
            sessions.Add(new BookDTO()
            {
                Name = "Test",
                Id = "askldjlaksjdlkasdj",
                Picture = "pic"
            });
            sessions.Add(new BookDTO()
            {
                Name = "Test2",
                Id = "askldjlaksjdlkasd2j",
                Picture = "picd"
            });

            return sessions;
        }
    }
}
