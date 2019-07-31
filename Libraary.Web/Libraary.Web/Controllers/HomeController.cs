namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Libraries;
    using Services;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILibraryService libraryService;
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public HomeController(IMapper mapper, ILibraryService libraryService, IUserService userService, IBookService bookService)
        {
            this.mapper = mapper;
            this.libraryService = libraryService;
            this.userService = userService;
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            LibraryDetailsViewModel model = new LibraryDetailsViewModel
            {
                UsersCount = this.userService.GetUsersCount(),
                BooksCountForAllLibraries = this.bookService.GetCountOfAllBooks(),
                LibrariesCount = this.libraryService.GetCountOfAllLibraries()
            };

            if (this.User.IsInRole("Owner") || this.User.IsInRole("Librarian"))
            {
                string libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
                var library = this.libraryService.GetLibraryDetails(libraryId);
                model = this.mapper.Map<LibraryDetailsViewModel>(library);

                return this.View(model);
            }

            if (this.User.IsInRole("User"))
            {
                return this.RedirectToAction("All", "Books");
            }

            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
