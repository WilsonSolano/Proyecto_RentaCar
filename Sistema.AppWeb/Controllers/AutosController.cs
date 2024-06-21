using Microsoft.AspNetCore.Mvc;

namespace SistemaRentaCarAppWeb.Controllers
{
    public class AutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
