using Microsoft.AspNetCore.Mvc;

namespace SistemaRentaCarAppWeb.Controllers
{
    public class RentaController : Controller
    {
        public IActionResult NuevaRenta()
        {
            return View();
        }
        
        public IActionResult HistorialRenta()
        {
            return View();
        }
    }
}
