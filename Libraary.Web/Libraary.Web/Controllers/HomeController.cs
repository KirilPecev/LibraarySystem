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

        public HomeController(IMapper mapper, ILibraryService libraryService, IUserService userService, IBookService bookService)
        {
            this.mapper = mapper;
            this.libraryService = libraryService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole("User") || !this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("All", "Books");
            }

            if (this.User.IsInRole("Owner") || this.User.IsInRole("Librarian"))
            {
                string libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
                var library = this.libraryService.GetLibraryDetails(libraryId);
                var libraryModel = this.mapper.Map<LibraryDetailsViewModel>(library);

                return this.View(libraryModel);
            }

            return this.RedirectToAction("Index", "Admin", new { Area = "Admin" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
