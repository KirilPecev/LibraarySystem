namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Libraries;
    using Services;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;

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

            if (this.User.IsInRole("Owner") || this.User.IsInRole("Librarian"))
            {
                string id = this.userService.GetUserLibraryId(this.User.Identity.Name);

                return this.RedirectToAction("Home", new { LibraryId = id });
            }

            if (this.User.IsInRole("User"))
            {
                return this.RedirectToAction("All", "Books");
            }

            return this.RedirectToAction("Home");
        }

        [Authorize(Roles = "Owner, Librarian, Admin")]
        public IActionResult Home(string libraryId = null)
        {
            LibraryDetailsViewModel model = new LibraryDetailsViewModel
            {
                UsersCount = this.userService.GetUsersCount(),
                BooksCountForAllLibraries = this.bookService.GetCountOfAllBooks(),
                LibrariesCount = this.libraryService.GetCountOfAllLibraries()
            };

            if (libraryId != null)
            {
                var library = this.libraryService.GetLibraryDetails(libraryId);
                model = this.mapper.Map<LibraryDetailsViewModel>(library);
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
