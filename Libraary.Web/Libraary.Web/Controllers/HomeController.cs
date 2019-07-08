namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Libraary.Services;
    using Libraary.Web.Models;
    using Libraary.Web.Models.Libraries;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILibraryService libraryService;
        private readonly IUserService userService;

        public HomeController(IMapper mapper, ILibraryService libraryService, IUserService userService)
        {
            this.mapper = mapper;
            this.libraryService = libraryService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            if (this.User.IsInRole("Owner"))
            {
                string id = this.userService.GetUserLibraryId(this.User.Identity.Name);

                return this.RedirectToAction("Home", new { LibraryId = id });
            }

            return this.RedirectToAction("Home");
        }

        public IActionResult Home(string libraryId = null)
        {
            LibraryDetailsViewModel model = new LibraryDetailsViewModel();
            model.UsersCount = this.userService.GetUsersCount();

            if (libraryId != null)
            {
                var library = this.libraryService.GetLibraryDetails(libraryId);
                model = this.mapper.Map<LibraryDetailsViewModel>(library);
            }

            return this.View(model);
        }

        public IActionResult Choose(string libraryId)
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
