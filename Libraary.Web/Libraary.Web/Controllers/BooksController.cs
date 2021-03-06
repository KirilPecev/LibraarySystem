﻿namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Books;
    using Newtonsoft.Json;
    using Services;
    using Services.DTOs.Book;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BooksController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public BooksController(
            IMapper mapper,
            IDistributedCache cache,
            IUserService userService,
            IBookService bookService,
            IServiceProvider provider,
            IServiceScopeFactory factory) : base(provider, factory)
        {
            this.mapper = mapper;
            this.cache = cache;
            this.userService = userService;
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            IEnumerable<BookDTO> modelDto;

            if (this.User != null && this.User.IsInRole("Librarian"))
            {
                var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
                modelDto = this.bookService.GetAllByLibraryId(libraryId);
            }
            else
            {
                modelDto = this.GetAllBooksFromCache();
            }

            var model = this.mapper.Map<BookViewModel[]>(modelDto);

            return this.View(model);
        }

        private IEnumerable<BookDTO> GetAllBooksFromCache()
        {
            var books = this.cache.GetString("books");

            var result = JsonConvert.DeserializeObject<BookDTO[]>(books).ToList();

            return result;
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Rented()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var booksDTO = this.bookService.GetAllRented(libraryId);
            var model = this.mapper.Map<BookViewModel[]>(booksDTO);

            return this.View("All", model);
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Owner, Librarian")]
        [HttpPost]
        public IActionResult Add(BookInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);

            var isNameBusy = this.bookService.GetAllByLibraryId(libraryId).Any(book => book.Name == model.Name);
            if (isNameBusy)
            {
                ModelState.AddModelError("Name", "Already has book with this name!");
                return this.View(model);
            }

            var mappedModel = this.mapper.Map<AddBookDTO>(model);
            this.bookService.Add(mappedModel, libraryId);

            base.UpdateBooksInCache().Wait();

            return this.Redirect("/");
        }

        public IActionResult Details(string bookId)
        {
            var booksDto = this.bookService.GetBookDetails(bookId);
            var mappedModel = this.mapper.Map<BookDetailsViewModel>(booksDto);

            return this.View(mappedModel);
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Remove(string bookId)
        {
            this.bookService.RemoveBook(bookId);

            base.UpdateBooksInCache().Wait();

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult Edit(string bookId)
        {
            var book = this.bookService.GetBookEditDetails(bookId);
            var mappedModel = this.mapper.Map<BookEditInputModel>(book);

            return this.View(mappedModel);
        }

        [Authorize(Roles = "Owner, Librarian")]
        [HttpPost]
        public IActionResult Edit(BookEditInputModel model, string bookId)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var mappedModel = this.mapper.Map<EditBookDto>(model);

            this.bookService.EditBookById(bookId, mappedModel, libraryId);
            base.UpdateBooksInCache().Wait();

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Owner, Librarian")]
        public IActionResult ReturnBook(string bookId)
        {
            var result = this.bookService.ReturnBook(bookId);

            if (result == false)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Rented");
        }

        [Authorize(Roles = "Owner, Librarian, User")]
        [HttpPost]
        public IActionResult Rating(BookDetailsViewModel model)
        {
            this.bookService.SaveRatingFromUser(model.Id, model.Rating);

            return this.RedirectToAction("Details", new { bookId = model.Id });
        }

        public IActionResult SearchedBooks(string search)
        {
            if (search == null)
            {
                return this.RedirectToAction("All");
            }

            IEnumerable<BookDTO> modelDto;

            if (this.User.IsInRole("Librarian"))
            {
                var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
                modelDto = this.bookService.GetAllByLibraryId(libraryId)
                    .Where(book => book.Name.ToLower().Contains(search.ToLower()));
            }
            else
            {
                modelDto = this.bookService.GetAll()
                    .Where(book => book.Name.ToLower().Contains(search.ToLower()));
            }

            if (!modelDto.Any())
            {
                return this.RedirectToAction("All");
            }

            var model = this.mapper.Map<BookViewModel[]>(modelDto);

            return this.View("All", model);
        }
    }
}
