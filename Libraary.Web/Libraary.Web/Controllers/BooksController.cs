namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models.Books;
    using Services;
    using Services.DTOs.Book;

    public class BooksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        private readonly string libraryId;

        public BooksController(IMapper mapper, IUserService userService, IBookService bookService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.bookService = bookService;

            this.libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
        }

        public IActionResult All()
        {
            var booksDTO = this.bookService.GetAll(this.libraryId);
            var model = this.mapper.Map<BookViewModel[]>(booksDTO);

            return this.View(model);
        }

        public IActionResult Rented()
        {
            var booksDTO = this.bookService.GetAllRented(this.libraryId);
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
            this.bookService.Add(model, this.libraryId);

            return this.View("/");
        }


        public IActionResult Details(string bookId)
        {
            var booksDto = this.bookService.GetBookDetails(bookId);
            this.mapper.Map<BookDetailsViewModel>(booksDto);

            return this.View(booksDto);
        }
    }
}
