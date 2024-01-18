using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
