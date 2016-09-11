using Microsoft.AspNetCore.Mvc;

namespace HelloWorld
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return this.Content("Here we are");
        }
    }
}