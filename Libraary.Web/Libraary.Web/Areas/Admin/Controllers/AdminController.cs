namespace Libraary.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly ILibraryService libraryService;
        private readonly IBookService bookService;

        public AdminController(IUserService userService, ILibraryService libraryService, IBookService bookService)
        {
            this.userService = userService;
            this.libraryService = libraryService;
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                UsersCount = this.userService.GetUsersCount(),
                BooksCountForAllLibraries = this.bookService.GetCountOfAllBooks(),
                LibrariesCount = this.libraryService.GetCountOfAllLibraries()
            };

            return this.View(model);
        }
    }
}
