namespace Libraary.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Rented()
        {
            return this.View();
        }

        public IActionResult Add()
        {
            return this.View();
        }
    }
}
