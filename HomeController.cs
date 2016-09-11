using Microsoft.AspNetCore.Mvc;

namespace HelloWorld
{
    public class HomeController : Controller
    {
        public readonly MyOptions _options;

        public HomeController(MyOptions options)
        {
            this._options = options;
        }

        public IActionResult Index()
        {
            return this.Content($"Here we are, greetings {this._options.Greeting}");
        }
    }
}