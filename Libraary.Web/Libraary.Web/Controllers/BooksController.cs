namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models.Books;
    using Services;
    using Services.DTOs.Book;
    using System.Collections.Generic;

    public class BooksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public BooksController(IMapper mapper, IUserService userService, IBookService bookService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            IEnumerable<BookDTO> modelDto;

            if (this.User.IsInRole("Librarian"))
            {
                var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
                modelDto = this.bookService.GetAllByLibraryId(libraryId);
            }
            else
            {
                modelDto = this.bookService.GetAll();
            }

            var model = this.mapper.Map<BookViewModel[]>(modelDto);

            return this.View(model);
        }

        public IActionResult Rented()
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var booksDTO = this.bookService.GetAllRented(libraryId);
            var model = this.mapper.Map<BookViewModel[]>(booksDTO);

            return this.View("All", model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(BookInputModel book)
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var model = this.mapper.Map<AddBookDTO>(book);
            this.bookService.Add(model, libraryId);

            return this.Redirect("/");
        }


        public IActionResult Details(string bookId)
        {
            var booksDto = this.bookService.GetBookDetails(bookId);
            var mappedModel = this.mapper.Map<BookDetailsViewModel>(booksDto);

            return this.View(mappedModel);
        }

        public IActionResult Remove(string bookId)
        {
            this.bookService.RemoveBook(bookId);
            return this.RedirectToAction("All");
        }

        public IActionResult Edit(string bookId)
        {
            var book = this.bookService.GetBookEditDetails(bookId);
            var mappedModel = this.mapper.Map<BookEditInputModel>(book);

            return this.View(mappedModel);
        }

        [HttpPost]
        public IActionResult Edit(BookEditInputModel model, string bookId)
        {
            var libraryId = this.userService.GetUserLibraryId(this.User.Identity.Name);
            var mappedModel = this.mapper.Map<EditBookDto>(model);
            this.bookService.EditBookById(bookId, mappedModel, libraryId);

            return this.RedirectToAction("All");
        }

        public IActionResult ReturnBook(string bookId)
        {
            var result = this.bookService.ReturnBook(bookId);

            if (result == false)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Rented");
        }

        [HttpPost]
        public IActionResult Rating(BookDetailsViewModel model)
        {
            //TODO: Implement functionality
            this.bookService.SaveRatingFromUser(model.Id);

            return this.RedirectToAction("Details", model.Id);
        }
    }
}
