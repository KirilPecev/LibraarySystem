namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Publishers;
    using Services;
    using Services.DTOs.Publisher;

    public class PublishersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPublisherService publisherService;
        private readonly IUserService userService;

        public PublishersController(IMapper mapper, IPublisherService publisherService, IUserService userService)
        {
            this.mapper = mapper;
            this.publisherService = publisherService;
            this.userService = userService;
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Owner, Librarian")]
        [HttpPost]
        public IActionResult Add(PublisherBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var mappedModel = this.mapper.Map<AddPublisherDTO>(model);
            this.publisherService.Add(mappedModel);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult All()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var model = this.publisherService.GetAllByLibraryId(libraryId);
            var mappedModel = this.mapper.Map<PublisherViewModel[]>(model);

            return this.View(mappedModel);
        }
    }
}
