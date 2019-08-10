namespace Libraary.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Books;
    using Services;

    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBookService bookService;
        private readonly IUserService userService;

        public UsersController(IMapper mapper, IBookService bookService, IUserService userService)
        {
            this.mapper = mapper;
            this.bookService = bookService;
            this.userService = userService;
        }

        [Authorize(Roles = "User")]
        public IActionResult History()
        {
            var user = this.User.Identity.Name;
            var books = this.bookService.GetAllRentedByUserName(user);
            var model = this.mapper.Map<BookViewModel[]>(books);

            return this.View(model);
        }

        [Authorize(Roles = "User")]
        public IActionResult Rent(string bookId)
        {
            var userName = this.User.Identity.Name;
            var result = this.bookService.RentBook(userName, bookId);

            return this.View();
        }
    }
}
