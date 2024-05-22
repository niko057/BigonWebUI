using Microsoft.AspNetCore.Mvc;

namespace BigonWebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
