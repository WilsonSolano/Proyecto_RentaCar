using Microsoft.AspNetCore.Mvc;

namespace SistemaRentaCarAppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
