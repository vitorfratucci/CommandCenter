using Microsoft.AspNetCore.Mvc;

namespace CommandCenter.Controllers
{
    public class InformativosController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Retorna a View "Index.cshtml" dentro de "Views/Informativos/"
        }
    }
}