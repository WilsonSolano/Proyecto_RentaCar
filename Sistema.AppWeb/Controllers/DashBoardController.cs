using Microsoft.AspNetCore.Mvc;

namespace SistemaRentaCarAppWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
