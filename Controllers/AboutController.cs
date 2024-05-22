using Microsoft.AspNetCore.Mvc;

namespace BigonWebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
