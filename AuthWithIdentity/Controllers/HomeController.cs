using Microsoft.AspNetCore.Mvc;

namespace PaderbornUniversity.SILab.Hip.Auth.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
