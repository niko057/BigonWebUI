using Microsoft.AspNetCore.Mvc;

namespace BigonWebUI.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
