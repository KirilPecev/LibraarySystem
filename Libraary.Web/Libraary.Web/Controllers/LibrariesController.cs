﻿namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Libraary.Services.DTOs.Library;
    using Libraary.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Libraries;
    using Services;
    using System.Diagnostics;

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
            var libraries = this.libraryService.GetAll();
            var model = this.mapper.Map<LibraryViewModel[]>(libraries);

            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddLibraryInputModel model)
        {
            var library = this.mapper.Map<AddLibraryDTO>(model);

            var id = this.libraryService.Add(library);

            if (id == null)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return this.RedirectToAction("Add","Owners", new { LibraryId = id });
        }


        public IActionResult Details(string libraryId)
        {
            var library = this.libraryService.GetLibraryDetails(libraryId);
            var model = this.mapper.Map<LibraryDetailsViewModel>(library);

            return this.View(model);
        }
    }
}