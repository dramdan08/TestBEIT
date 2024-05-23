using Microsoft.AspNetCore.Mvc;

namespace TestBEIT.Controllers
{
    public class SiswaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
