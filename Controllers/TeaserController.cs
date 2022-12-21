using Microsoft.AspNetCore.Mvc;

namespace PromoDH.Controllers
{
    public class TeaserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
