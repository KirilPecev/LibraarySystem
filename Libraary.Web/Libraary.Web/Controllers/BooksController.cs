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
  
        public BooksController(IMapper mapper, IUserService userService, IBookService bookService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var booksDTO = this.bookService.GetAll(libraryId);
            var model = this.mapper.Map<BookViewModel[]>(booksDTO);

            return this.View(model);
        }

        public IActionResult Rented()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var booksDTO = this.bookService.GetAllRented(libraryId);
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
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var model = this.mapper.Map<AddBookDTO>(book);
            this.bookService.Add(model, libraryId);

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
