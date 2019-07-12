namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Services;
    using Services.DTOs.Book;
    using Microsoft.AspNetCore.Mvc;
    using Models.Books;

    public class BooksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public string LibraryId { get; private set; }

        public BooksController(IMapper mapper, IUserService userService, IBookService bookService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.bookService = bookService;
            this.LibraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
        }

        public IActionResult All()
        {
            var booksDTO = this.bookService.GetAll(this.LibraryId);
            var model = this.mapper.Map<BookViewModel[]>(booksDTO);

            return this.View(model);
        }

        public IActionResult Rented()
        {
            var booksDTO = this.bookService.GetAllRented(this.LibraryId);
            var model = this.mapper.Map<BookViewModel[]>(booksDTO);

            return this.View("All", model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(BookInputModel book)
        {
            var model = this.mapper.Map<AddBookDTO>(book);
            this.bookService.Add(model, this.LibraryId);

            return this.View("/");
        }


        public IActionResult Details()
        {
            return this.View();
        }
    }
}
