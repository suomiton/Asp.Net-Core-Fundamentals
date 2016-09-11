using Microsoft.AspNetCore.Http;
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
            int? count;
            count = HttpContext.Session.GetInt32("count");
            count = count ?? 0;
            count++;
            HttpContext.Session.SetInt32("count", count.Value);
            return this.Content($"Here we are, greetings {this._options.Greeting}. Count: {count}");
        }
    }
}