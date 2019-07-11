namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Libraary.Services;
    using Libraary.Services.DTOs.Library;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Owners;
    using System.Diagnostics;

    public class OwnersController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILibraryService libraryService;

        public OwnersController(IMapper mapper, ILibraryService libraryService)
        {
            this.mapper = mapper;
            this.libraryService = libraryService;
        }

        [HttpGet]
        public IActionResult Add(OwnerBindingModel model)
        {
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(OwnerBindingModel model, int a) //TODO: Refactor this method!
        {
            var dto = this.mapper.Map<OwnerDTO>(model);
            bool result = this.libraryService.AddOwner(dto);

            if (result == false)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return this.Redirect("/");
        }
    }
}
