namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Libraary.Services;
    using Libraary.Web.Models.Books;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBookService bookService;

        public UsersController(IMapper mapper, IBookService bookService)
        {
            this.mapper = mapper;
            this.bookService = bookService;
        }

        public IActionResult History()
        {
            var user = this.User.Identity.Name;
            var books = this.bookService.GetAllRentedByUserName(user);
            var model = this.mapper.Map<BookViewModel[]>(books);

            return this.View(model);
        }

        public IActionResult Rent(string bookId)
        {
            var userName = this.User.Identity.Name;
            var result = this.bookService.RentBook(userName, bookId);

            if (result == false)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View();
        }
    }
}
