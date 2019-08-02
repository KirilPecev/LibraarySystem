namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Libraries;
    using Services;
    using Services.DTOs.Library;

    public class LibrariesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILibraryService libraryService;

        public LibrariesController(IMapper mapper, ILibraryService libraryService)
        {
            this.mapper = mapper;
            this.libraryService = libraryService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var libraries = this.libraryService.GetAll();
            var model = this.mapper.Map<LibraryViewModel[]>(libraries);

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AddLibraryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var library = this.mapper.Map<AddLibraryDTO>(model);

            var id = this.libraryService.Add(library);

            return this.Redirect("/");
        }

        [Authorize(Roles = "Admin, Owner")]
        public IActionResult Details(string libraryId)
        {
            var library = this.libraryService.GetLibraryDetails(libraryId);
            var model = this.mapper.Map<LibraryDetailsViewModel>(library);

            return this.View(model);
        }
    }
}
