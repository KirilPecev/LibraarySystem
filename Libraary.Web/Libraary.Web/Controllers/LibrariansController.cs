namespace Libraary.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class LibrariansController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(string something)
        {
            return this.View();
        }
    }
}
