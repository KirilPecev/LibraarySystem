namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Libraary.Services;
    using Libraary.Services.DTOs.Library;
    using Libraary.Web.Models;
    using Libraary.Web.Models.Librarians;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class LibrariansController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ILibraryService libraryService;

        public LibrariansController(IMapper mapper, IUserService userService, ILibraryService libraryService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.libraryService = libraryService;
        }

        public IActionResult All()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);

            return this.View(libraryId);
        }

        [HttpGet]
        public IActionResult Add()
        {
            LibrarianBindingModel model = new LibrarianBindingModel();
            model.LibraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(LibrarianBindingModel model)
        {
            var dto = this.mapper.Map<LibrarianDTO>(model);
            bool result = this.libraryService.AddLibrarian(dto);

            if (result == false)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return this.Redirect("/");
        }
    }
}
