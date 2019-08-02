namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Librarians;
    using Services;
    using Services.DTOs.Librarian;

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

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult All()
        {
            var model = this.libraryService.GetAllLibrarians();
            var librarians = this.mapper.Map<LibrarianDetailsViewModel[]>(model);

            return this.View(librarians);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Add()
        {
            LibrarianBindingModel model = new LibrarianBindingModel();
            model.LibraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);

            return this.View(model);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public IActionResult Add(LibrarianBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var dto = this.mapper.Map<LibrarianDTO>(model);
            bool result = this.libraryService.AddLibrarian(dto);

            return this.Redirect("/");
        }
    }
}
