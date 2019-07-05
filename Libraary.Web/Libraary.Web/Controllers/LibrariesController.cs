namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models.Libraries;
    using Services;

    public class LibrariesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILibraryService libraryService;

        public LibrariesController(IMapper mapper, ILibraryService libraryService)
        {
            this.mapper = mapper;
            this.libraryService = libraryService;
        }

        public IActionResult All()
        {
            return Ok();
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddLibraryInputModel model)
        {

            return Ok();
        }

        public IActionResult Details()
        {
            return Ok();
        }
    }
}
