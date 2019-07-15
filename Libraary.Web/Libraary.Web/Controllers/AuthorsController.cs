namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Libraary.Services.DTOs.Author;
    using Libraary.Web.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    using Models.Authors;
    using Services;

    public class AuthorsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAuthorService authorService;
        private readonly IBookService bookService;
        private readonly IUserService userService;
        private readonly string libraryId;

        public AuthorsController(IMapper mapper, IAuthorService authorService, IBookService bookService, IUserService userService)
        {
            this.mapper = mapper;
            this.authorService = authorService;
            this.bookService = bookService;
            this.userService = userService;
            this.libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
        }

        public IActionResult All()
        {
            var authors = this.authorService.GetAll(this.libraryId);
            var model = mapper.Map<AuthorViewModel[]>(authors);

            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AuthorBindingModel model)
        {
            var mappedModel = this.mapper.Map<AddAuthorDTO>(model);

            this.authorService.Add(mappedModel, libraryId);

            return this.RedirectToAction("All");
        }

        public IActionResult Details(string authorId)
        {
            var books = this.bookService.GetAllByAuthor(authorId, libraryId);

            var model = this.mapper.Map<BookViewModel[]>(books);

            return this.View(model);
        }
    }
}
