namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models.Publishers;
    using Services;
    using Services.DTOs.Publisher;

    public class PublishersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPublisherService publisherService;

        public PublishersController(IMapper mapper, IPublisherService publisherService)
        {
            this.mapper = mapper;
            this.publisherService = publisherService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(PublisherBindingModel model)
        {
            var mappedModel = this.mapper.Map<AddPublisherDTO>(model);
            this.publisherService.Add(mappedModel);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var model = this.publisherService.GetAll();
            var mappedModel = this.mapper.Map<PublisherViewModel[]>(model);

            return this.View(mappedModel);
        }
    }
}
