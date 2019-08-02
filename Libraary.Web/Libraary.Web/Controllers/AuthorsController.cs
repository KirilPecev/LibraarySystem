namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Authors;
    using Models.Books;
    using Services;
    using Services.DTOs.Author;

    public class AuthorsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAuthorService authorService;
        private readonly IBookService bookService;
        private readonly IUserService userService;

        public AuthorsController(IMapper mapper, IAuthorService authorService, IBookService bookService, IUserService userService)
        {
            this.mapper = mapper;
            this.authorService = authorService;
            this.bookService = bookService;
            this.userService = userService;
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult All()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var authors = this.authorService.GetAllByLibraryId(libraryId);
            var model = mapper.Map<AuthorViewModel[]>(authors);

            return this.View(model);
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Owner, Librarian")]
        [HttpPost]
        public IActionResult Add(AuthorBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var mappedModel = this.mapper.Map<AddAuthorDTO>(model);

            var result = this.authorService.Add(mappedModel);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Details(string authorId)
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var books = this.bookService.GetAllByAuthor(authorId, libraryId);

            var model = this.mapper.Map<BookViewModel[]>(books);

            return this.View(model);
        }
    }
}
