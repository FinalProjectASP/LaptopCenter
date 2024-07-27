using Microsoft.AspNetCore.Mvc;

namespace LaptopCenter.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
